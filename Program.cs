
using Microsoft.EntityFrameworkCore;
using Npgsql;
using wt_lab7_7postgres_efcore_talashkevich.Models;
using wt_lab7_7postgres_efcore_talashkevich.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Подключение к PostgreSQL
var databaseUrl = builder.Configuration["DATABASE_URL"];

string connectionString;

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);

    if (userInfo.Length != 2)
    {
        throw new InvalidOperationException(
            "Некорректная переменная DATABASE_URL.");
    }

    connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = Uri.UnescapeDataString(userInfo[0]),
        Password = Uri.UnescapeDataString(userInfo[1]),
        SslMode = SslMode.Disable
    }.ConnectionString;
}
else
{
    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "Строка подключения к базе данных не найдена.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Автоматическое применение миграций
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Главная страница
app.MapGet("/", () => "Minimal API работает!");

// Информация о приложении
app.MapGet("/api/info", () =>
{
    return Results.Ok(new
    {
        Application = "FITNESS CLUB",
        Technology = "ASP.NET Core Minimal API + EF Core",
        Database = "PostgreSQL",
        Version = "1.0"
    });
});

// Получение активных участников
app.MapGet("/api/members/active", async (AppDbContext db) =>
{
    var members = await db.Members
        .Where(m => m.IsActive)
        .ToListAsync();

    return Results.Ok(members);
});

// Сортировка участников по возрасту
app.MapGet("/api/members/sorted", async (AppDbContext db) =>
{
    var members = await db.Members
        .OrderBy(m => m.Age)
        .ToListAsync();

    return Results.Ok(members);
});

// Получение участников старше указанного возраста
app.MapGet("/api/members/age", async (
    AppDbContext db,
    int minAge) =>
{
    var members = await db.Members
        .Where(m => m.Age >= minAge)
        .OrderBy(m => m.Name)
        .ToListAsync();

    return Results.Ok(members);
});

// Получение всех участников и поиск по имени
app.MapGet("/api/members", async (
    AppDbContext db,
    string? search) =>
{
    var query = db.Members.AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(m =>
            EF.Functions.ILike(m.Name, $"%{search}%"));
    }

    var members = await query.ToListAsync();

    return Results.Ok(members);
});

// Получение участника по ID
app.MapGet("/api/members/{id}", async (
    int id,
    AppDbContext db) =>
{
    var member = await db.Members.FindAsync(id);

    return member == null
        ? Results.NotFound()
        : Results.Ok(member);
});

// Добавление участника
app.MapPost("/api/members", async (
    Member newMember,
    AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(newMember.Name) ||
        newMember.Age < 14 ||
        newMember.Age > 100)
    {
        return Results.BadRequest("Некорректные данные участника.");
    }

    newMember.Id = 0;

    db.Members.Add(newMember);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/api/members/{newMember.Id}",
        newMember);
});

// Обновление участника
app.MapPut("/api/members/{id}", async (
    int id,
    Member updatedMember,
    AppDbContext db) =>
{
    var member = await db.Members.FindAsync(id);

    if (member == null)
    {
        return Results.NotFound();
    }

    if (string.IsNullOrWhiteSpace(updatedMember.Name) ||
        updatedMember.Age < 14 ||
        updatedMember.Age > 100)
    {
        return Results.BadRequest("Некорректные данные участника.");
    }

    member.Name = updatedMember.Name;
    member.Age = updatedMember.Age;
    member.MembershipPlanId = updatedMember.MembershipPlanId;
    member.TrainerId = updatedMember.TrainerId;
    member.IsActive = updatedMember.IsActive;
    member.PhoneNumber = updatedMember.PhoneNumber;

    await db.SaveChangesAsync();

    return Results.Ok(member);
});

// Удаление участника
app.MapDelete("/api/members/{id}", async (
    int id,
    AppDbContext db) =>
{
    var member = await db.Members.FindAsync(id);

    if (member == null)
    {
        return Results.NotFound();
    }

    db.Members.Remove(member);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";

app.Run($"http://0.0.0.0:{port}");

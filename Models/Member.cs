using System.ComponentModel.DataAnnotations;

namespace wt_lab7_7postgres_efcore_talashkevich.Models;

public class Member
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите имя участника")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(14, 100, ErrorMessage = "Возраст должен быть от 14 до 100 лет")]
    public int Age { get; set; }

    public int MembershipPlanId { get; set; }

    public int TrainerId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
}
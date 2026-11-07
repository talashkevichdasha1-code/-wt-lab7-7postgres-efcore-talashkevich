using wt_lab7_7postgres_efcore_talashkevich.Models;

namespace wt_lab7_7postgres_efcore_talashkevich.Data;

public static class MemberStore
{
    public static List<Member> Members { get; } = new()
    {
        new Member
        {
            Id = 1,
            Name = "РђРЅРЅР° РџРµС‚СЂРѕРІР°",
            Age = 22,
            MembershipPlanId = 1,
            TrainerId = 1,
            IsActive = true
        },
        new Member
        {
            Id = 2,
            Name = "РњР°РєСЃРёРј РРІР°РЅРѕРІ",
            Age = 25,
            MembershipPlanId = 2,
            TrainerId = 2,
            IsActive = true
        },
        new Member
        {
            Id = 3,
            Name = "Р•Р»РµРЅР° РЎРјРёСЂРЅРѕРІР°",
            Age = 21,
            MembershipPlanId = 1,
            TrainerId = 3,
            IsActive = false
        },
        new Member
        {
            Id = 4,
            Name = "Р”РјРёС‚СЂРёР№ РљРѕР·Р»РѕРІ",
            Age = 28,
            MembershipPlanId = 3,
            TrainerId = 1,
            IsActive = true
        },
        new Member
        {
            Id = 5,
            Name = "РЎРѕС„РёСЏ РњРѕСЂРѕР·РѕРІР°",
            Age = 23,
            MembershipPlanId = 2,
            TrainerId = 2,
            IsActive = true
        }
    };
}


using Zenject;

public class ShootTimeSkill : BaseSkill
{
    public ShootTimeSkill(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) : base(signalBus, gameSaver, moneyService) {
    }

    public override void SetStartValues() {
        SkillType = SkillType.TimeShootSkill;
        ImprovementMultiplier = 0.75f;
        DefaultPrice = 1;
        MaxLevel = 3;
    }

    public override bool Upgrade() {
        if (base.Upgrade()) {
            SignalBus.Fire<SignalUpdateShootTime>();
            return true;
        }
        return false;
    }
}

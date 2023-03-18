using Zenject;

public class RadiusSkill : BaseSkill
{
    public RadiusSkill(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) : base(signalBus, gameSaver, moneyService) {
    }

    public override void SetStartValues() {
        SkillType = SkillType.RadiusSkill;
        ImprovementMultiplier = 1.3f;
        DefaultPrice = 1;
        MaxLevel = 3;
    }

    public override bool Upgrade() {
        if (base.Upgrade()) {
            SignalBus.Fire<SignalUpdateRadius>();
            return true;
        }
        return false;
    }
}

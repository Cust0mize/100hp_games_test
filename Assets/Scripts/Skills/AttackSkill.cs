using Zenject;

public class AttackSkill : BaseSkill
{
    public AttackSkill(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) : base(signalBus, gameSaver, moneyService) {
    }

    public override void SetStartValues() {
        SkillType = SkillType.AttackSkill;
        ImprovementMultiplier = 1;
        DefaultPrice = 1;
        MaxLevel = 3;
    }
}

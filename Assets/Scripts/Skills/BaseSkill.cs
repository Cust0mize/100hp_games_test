using Zenject;

public abstract class BaseSkill
{
    protected SignalBus SignalBus { get; private set; }
    protected GameSaver GameSaver { get; private set; }

    public float ImprovementMultiplier { get; protected set; }
    public float DefaultPrice { get; protected set; }
    public SkillType SkillType { get; protected set; }
    public int MaxLevel { get; protected set; }
    public int Level { get; private set; }

    private MoneyService _moneyService;

    [Inject]
    public BaseSkill(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) {
        SetStartValues();
        SignalBus = signalBus;
        GameSaver = gameSaver;
        _moneyService = moneyService;
        Level = GameSaver.GetSkillLevel(SkillType);
    }

    public virtual bool Upgrade() {
        if (Level < MaxLevel) {
            GameSaver.SetSkillValue(SkillType, ImprovementMultiplier);
            _moneyService.RemoveCoinFromSkillBuy(Level * DefaultPrice);
            Level++;
            GameSaver.SetSkillLevel(SkillType, Level);
            return true;
        }
        return false;
    }

    public abstract void SetStartValues();
}
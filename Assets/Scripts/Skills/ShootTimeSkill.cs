using Zenject;

public class ShootTimeSkill : ISkill
{
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private MoneyService _moneyService;
    private SkillType _skillType = SkillType.TimeShootSkill;
    private float _improvementMultiplier = 0.75f;
    private float _defaultPrice = 1;
    private int _level = 1;
    private int _maxLevel = 3;

    public float DefaultPrice => _defaultPrice;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public SkillType SkillType => _skillType;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        _moneyService = moneyService;
        _level = _gameSaver.GetShootTimeSkillLevel();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetShootTime(_gameSaver.GetShootTime() * _improvementMultiplier);
            _moneyService.RemoveCoinFromSkillBuy(_level * _defaultPrice);
            _gameSaver.SetShootTimeLevel(_level);
            _signalBus.Fire<SignalUpdateShootTime>();
            _level++;
            _gameSaver.SetShootTimeLevel(_level);
            return true;
        }
        return false;
    }
}

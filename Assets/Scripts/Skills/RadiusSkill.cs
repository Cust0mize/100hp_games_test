using Zenject;

public class RadiusSkill : ISkill
{
    private MoneyService _moneyService;
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private SkillType _skillType = SkillType.RadiusSkill;
    private float _improvementMultiplier = 1.3f;
    private float _defaultPrice = 1;
    private int _level = 1;
    private int _maxLevel = 3;

    public int Level => _level;
    public float DefaultPrice => _defaultPrice;
    public int MaxLevel => _maxLevel;
    public SkillType SkillType => _skillType;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        _moneyService = moneyService;
        _level = _gameSaver.GetTowerRadiusLevel();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetTowerRadius(_gameSaver.GetTowerRadius() * _improvementMultiplier);
            _moneyService.RemoveCoinFromSkillBuy(_level * _defaultPrice);
            _signalBus.Fire<SignalUpdateRadius>();
            _level++;
            _gameSaver.SetRadiusTowerLevel(_level);
            return true;
        }
        return false;
    }
}
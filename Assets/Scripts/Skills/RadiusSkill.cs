using Zenject;

public class RadiusSkill : ISkill
{
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private SkillType _skillType = SkillType.RadiusSkill;
    private float _defaultPrice = 1;
    private int _level = 1;
    private int _maxLevel = 3;

    public int Level { get => _level; set => _level = value; }
    public float DefaultPrice => _defaultPrice;
    public int MaxLevel => _maxLevel;
    public SkillType Type => _skillType;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        Level = _gameSaver.GetTowerRadiusLevel();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetTowerRadius(_gameSaver.GetTowerRadius() * 1.5f);
            _gameSaver.SetCurrentCoint(_level * -_defaultPrice);
            _signalBus.Fire<SignalUpdateRadius>();
            _gameSaver.SetRadiusTowerLevel(_level);
            return true;
        }
        return false;
    }
}

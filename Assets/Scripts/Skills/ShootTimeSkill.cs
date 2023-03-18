using Zenject;

public class ShootTimeSkill : ISkill
{
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private SkillType _skillType = SkillType.TimeShootSkill;
    private float _defaultPrice = 1;
    private int _level = 1;
    private int _maxLevel = 3;

    public float DefaultPrice => _defaultPrice;
    public int Level { get => _level; set => _level = value; }
    public int MaxLevel => _maxLevel;
    public SkillType SkillType => _skillType;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        Level = _gameSaver.GetShootTimeSkillLevel();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetShootTime(_gameSaver.GetShootTime() * 0.75f);
            _gameSaver.SetCurrentCoint(_level * -_defaultPrice);
            _gameSaver.SetShootTimeLevel(_level);
            _signalBus.Fire<SignalUpdateShootTime>();
            _gameSaver.SetShootTimeLevel(_level);
            return true;
        }
        return false;
    }
}

public class AttackSkill : ISkill
{
    private GameSaver _gameSaver;
    private SkillType _skillType = SkillType.AttackSkill;
    private float _defaultPrice = 1;
    private int _level;
    private int _maxLevel = 3;

    public float DefaultPrice => _defaultPrice;
    public int Level { get => _level; set => _level = value; }
    public int MaxLevel => _maxLevel;
    public SkillType Type => _skillType;


    public AttackSkill(GameSaver gameSaver) {
        _gameSaver = gameSaver;
        _level = (int)_gameSaver.GetBulletDamage();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetBulletDamage(_level + 1);
            _gameSaver.SetCurrentCoint(_level * -_defaultPrice);
            return true;
        }
        return false;
    }
}

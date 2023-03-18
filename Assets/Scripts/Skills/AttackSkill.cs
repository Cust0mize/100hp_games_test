public class AttackSkill : ISkill
{
    private MoneyService _moneyService;
    private GameSaver _gameSaver;
    private SkillType _skillType = SkillType.AttackSkill;
    private float _improvementMultiplier = 1f;
    private float _defaultPrice = 1;
    private int _level;
    private int _maxLevel = 3;

    public float DefaultPrice => _defaultPrice;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public SkillType SkillType => _skillType;

    public AttackSkill(GameSaver gameSaver, MoneyService moneyService) {
        _gameSaver = gameSaver;
        _moneyService = moneyService;
        _level = (int)_gameSaver.GetBulletDamage();
    }

    public bool Upgrade() {
        if (_level < _maxLevel) {
            _gameSaver.SetBulletDamage(_level + _improvementMultiplier);
            _moneyService.RemoveCoinFromSkillBuy(_level * _defaultPrice);
            _level++;
            return true;
        }
        return false;
    }
}

public class AttackSkill : ISkill
{
    private GameSaver _gameSaver;
    private float _defaultPrice = 10;
    private int _level;

    public float DefaultPrice => _defaultPrice;
    public int Level { get => _level; set => _level = value; }

    public AttackSkill(GameSaver gameSaver) {
        _gameSaver = gameSaver;
        _level = (int)_gameSaver.GetBulletDamage();
    }

    public void Upgrade(GameSaver gameSaver) {
        gameSaver.SetBulletDamage(_level + 1);
        gameSaver.SetCurrentCoint(_level * -_defaultPrice);
    }
}

using Zenject;

public class MoneyService
{
    private GameSaver _gameSaver;

    [Inject]
    public MoneyService(GameSaver gameSaver) {
        _gameSaver = gameSaver;
    }

    public void AddCoinForEnemyKill(Enemy enemy) {
        _gameSaver.SetCurrentCoint(enemy.GetReward());
    }

    public void RemoveCoinFromSkillBuy(float value) {
        _gameSaver.SetCurrentCoint(-value);
    }
}
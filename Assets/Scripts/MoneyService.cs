using Zenject;

public class MoneyService
{
    private GameSaver _gameSaver;

    [Inject]
    public MoneyService(GameSaver gameSaver) {
        _gameSaver = gameSaver;
    }

    public void AddCoinForEnemyKill(Enemy enemy) {
        _gameSaver.SetCurrentCoins(enemy.GetReward());
    }

    public void RemoveCoinFromSkillBuy(float value) {
        _gameSaver.SetCurrentCoins(-value);
    }
}
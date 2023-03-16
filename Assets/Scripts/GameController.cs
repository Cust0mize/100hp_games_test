using Zenject;
public class GameController
{
    private GameSaver _gameSaver;
    private SignalBus _signalBus;

    public GameController(GameSaver gameSaver, SignalBus signalBus) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
        _signalBus.Subscribe<SignalRemoveEnemy>(AddCoin);
    }

    private void AddCoin(SignalRemoveEnemy signalRemoveEnemy) {
        _gameSaver.SetCurrentCoint(signalRemoveEnemy.Enemy.GetReward());
    }
}
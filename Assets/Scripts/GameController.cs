using System;
using Zenject;
public class GameController
{
    private GameSaver _gameSaver;
    private SignalBus _signalBus;
    private UIRoot _uiRoot;

    public GameController(GameSaver gameSaver, SignalBus signalBus, UIRoot uIRoot) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
        _uiRoot = uIRoot;
        _signalBus.Subscribe<SignalRemoveEnemy>(AddCoin);
        _signalBus.Subscribe<SignalGameOver>(EndGame);
    }

    private void EndGame() {
        _signalBus.Unsubscribe<SignalGameOver>(EndGame);
        _uiRoot.ShowPanel<LosePanel>();
    }

    private void AddCoin(SignalRemoveEnemy signalRemoveEnemy) {
        _gameSaver.SetCurrentCoint(signalRemoveEnemy.Enemy.GetReward());
    }
}
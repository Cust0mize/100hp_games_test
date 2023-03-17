using System;
using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private EnemiesFactory _enemysFactory;
    [SerializeField] private Tower _tower;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus, UIRoot uIRoot) {
        _signalBus = signalBus;
        GameController gameController = new GameController(gameSaver, _signalBus, uIRoot);
    }

    private void Start() {
        DoInit();
        StartGame();
    }

    private void DoInit() {
        _tower.DoInit();
    }

    private void StartGame() {
        _enemysFactory.Init(_tower);
        _tower.Init(_enemysFactory);

        _signalBus.Subscribe<SignalGameOver>(StopGame);
    }

    private void StopGame() {
        _signalBus.Unsubscribe<SignalGameOver>(StopGame);
    }
}

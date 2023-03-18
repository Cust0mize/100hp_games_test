using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private EnemiesFactory _enemysFactory;
    [SerializeField] private Tower _tower;
    private SignalBus _signalBus;
    private UIRoot _uiRoot;

    [Inject]
    public void Construct(SignalBus signalBus, UIRoot uIRoot) {
        _signalBus = signalBus;
        _uiRoot = uIRoot;
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
        _signalBus.Subscribe<SignalGameOver>(EndGame);
    }

    private void EndGame() {
        _signalBus.Unsubscribe<SignalGameOver>(EndGame);
        _uiRoot.ShowPanel<LosePanel>();
    }
}

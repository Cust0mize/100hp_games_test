using TMPro;
using UnityEngine;
using Zenject;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinValue;
    private SignalBus _signalBus;
    private GameSaver _gameSaver;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
    }

    private void Start() {
        _coinValue.text = _gameSaver.GetCurrentCoins().ToString();
        _signalBus.Subscribe<SignalRemoveEnemy>(UpdateCoinValue);
    }

    private void UpdateCoinValue(SignalRemoveEnemy signalRemoveEnemy) {
        _coinValue.text = _gameSaver.GetCurrentCoins().ToString();
    }
}

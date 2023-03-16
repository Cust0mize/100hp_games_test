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
        _signalBus.Subscribe<SignalUpdateCoinValue>(UpdateCoinValue);
        _coinValue.text = _gameSaver.GetCurrentCoins().ToString();
    }

    private void Start() {

    }

    private void UpdateCoinValue() {
        _coinValue.text = _gameSaver.GetCurrentCoins().ToString();
    }
}

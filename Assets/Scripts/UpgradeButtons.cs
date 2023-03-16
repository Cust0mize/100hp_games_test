using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Zenject;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _radiusButton;
    [SerializeField] private Button _shootTimeButton;
    [SerializeField] private TextMeshProUGUI _attackLevelText;
    [SerializeField] private TextMeshProUGUI _radiusLevelText;
    [SerializeField] private TextMeshProUGUI _shootTimeLevelText;
    private ISkill _currentSkill;
    private GameSaver _gameSaver;

    private ShootTimeSkill _shootTimeSkill;
    private AttackSkill _attackSkill;
    private RadiusSkill _radiusSkill;

    [Inject]
    public void Construct(GameSaver gameSaver, ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        _gameSaver = gameSaver;
        _shootTimeSkill = shootTimeSkill;
        _attackSkill = attackSkill;
        _radiusSkill = radiusSkill;
    }

    private void Start() {
        SubscribeButton();
        UpdateText();
    }

    private void TryUpgradeCurrentSkill() {
        if (_gameSaver.GetCurrentCoins() >= _currentSkill.Level * _currentSkill.DefaultPrice) {
            _currentSkill.Upgrade(_gameSaver);
            _currentSkill.Level++;
            UpdateText();
        }
    }

    private void OnUpgradeShootTime() {
        _currentSkill = _shootTimeSkill;
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeRadius() {
        _currentSkill = _radiusSkill;
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeAttack() {
        _currentSkill = _attackSkill;
        TryUpgradeCurrentSkill();
    }

    private void UpdateText() {
        _attackLevelText.text = $"Level: {_attackSkill.Level}";
        _radiusLevelText.text = $"Level: {_radiusSkill.Level}";
        _shootTimeLevelText.text = $"Level: {_shootTimeSkill.Level}";
    }

    private void SubscribeButton() {
        _attackButton.onClick.AddListener(OnUpgradeAttack);
        _radiusButton.onClick.AddListener(OnUpgradeRadius);
        _shootTimeButton.onClick.AddListener(OnUpgradeShootTime);
    }
}

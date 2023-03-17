using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _radiusButton;
    [SerializeField] private Button _shootTimeButton;
    [SerializeField] private List<ButtonBuySkill> _buttonsBuySkill;

    private ISkill _currentSkill;
    private ButtonBuySkill _currentButtonelement;
    private GameSaver _gameSaver;
    private SignalBus _signalBus;

    private ShootTimeSkill _shootTimeSkill;
    private AttackSkill _attackSkill;
    private RadiusSkill _radiusSkill;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus, ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
        _shootTimeSkill = shootTimeSkill;
        _attackSkill = attackSkill;
        _radiusSkill = radiusSkill;
    }

    private void Start() {
        SubscribeButton();
        UpdateAllText(_shootTimeSkill, _attackSkill, _radiusSkill);
    }

    private void TryUpgradeCurrentSkill() {
        if (_gameSaver.GetCurrentCoins() >= _currentSkill.Level * _currentSkill.DefaultPrice) {
            if (_currentSkill.Upgrade()) {
                _currentSkill.Level++;
                _signalBus.Fire<SignalUpdateCoinValue>();
                UpdateText();
            }
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
        SearchCurrentButtonItem();
        _currentButtonelement.UpdateElement(_currentSkill);
    }

    private void SearchCurrentButtonItem() {
        for (int i = 0; i < _buttonsBuySkill.Count; i++) {
            if (_buttonsBuySkill[i].SkillType == _currentSkill.Type) {
                _currentButtonelement = _buttonsBuySkill[i];
            }
        }
    }

    private void SubscribeButton() {
        _attackButton.onClick.AddListener(OnUpgradeAttack);
        _radiusButton.onClick.AddListener(OnUpgradeRadius);
        _shootTimeButton.onClick.AddListener(OnUpgradeShootTime);
    }

    private void UpdateAllText(params ISkill[] skill) {
        for (int i = 0; i < _buttonsBuySkill.Count; i++) {
            for (int j = 0; j < skill.Length; j++) {
                if (_buttonsBuySkill[i].SkillType == skill[j].Type) {
                    _buttonsBuySkill[i].UpdateElement(skill[j]);
                }
            }
        }
    }
}

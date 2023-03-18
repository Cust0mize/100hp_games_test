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
    private ButtonBuySkill _currentButtonElement;
    private GameSaver _gameSaver;
    private SignalBus _signalBus;
    private SkillService _skillService;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus, SkillService skillService) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
        _skillService = skillService;
    }

    private void Start() {
        SubscribeButton();
        UpdateAllText(_skillService.GetShootTimeSkill(), _skillService.GetAttackSkill(), _skillService.GetRadiusSkill());
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
        _currentSkill = _skillService.GetShootTimeSkill();
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeRadius() {
        _currentSkill = _skillService.GetRadiusSkill();
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeAttack() {
        _currentSkill = _skillService.GetAttackSkill();
        TryUpgradeCurrentSkill();
    }

    private void UpdateText() {
        SearchCurrentButtonItem();
        _currentButtonElement.UpdateElement(_currentSkill);
    }

    private void SearchCurrentButtonItem() {
        for (int i = 0; i < _buttonsBuySkill.Count; i++) {
            if (_buttonsBuySkill[i].GetSkillType() == _currentSkill.SkillType) {
                _currentButtonElement = _buttonsBuySkill[i];
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
                if (_buttonsBuySkill[i].GetSkillType() == skill[j].SkillType) {
                    _buttonsBuySkill[i].UpdateElement(skill[j]);
                }
            }
        }
    }
}

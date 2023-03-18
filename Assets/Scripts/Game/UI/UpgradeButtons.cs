using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Linq;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] private List<ButtonBuySkill> _buttonsBuySkill;
    [SerializeField] private Button _shootTimeButton;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _radiusButton;

    private ButtonBuySkill _currentButtonElement;
    private SkillService _skillService;
    private BaseSkill _currentSkill;
    private GameSaver _gameSaver;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus, SkillService skillService) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
        _skillService = skillService;
    }

    private void Start() {
        SubscribeButton();
        UpdateAllText(_skillService.GetSkill(SkillType.AttackSkill), _skillService.GetSkill(SkillType.TimeShootSkill), _skillService.GetSkill(SkillType.RadiusSkill));
    }

    private void TryUpgradeCurrentSkill() {
        if (_gameSaver.GetCurrentCoins() >= _currentSkill.Level * _currentSkill.DefaultPrice) {
            if (_currentSkill.Upgrade()) {
                _signalBus.Fire<SignalUpdateCoinValue>();
                UpdateText();
            }
        }
    }

    private void OnUpgradeShootTime() {
        _currentSkill = _skillService.GetSkill(SkillType.TimeShootSkill);
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeRadius() {
        _currentSkill = _skillService.GetSkill(SkillType.RadiusSkill);
        TryUpgradeCurrentSkill();
    }

    private void OnUpgradeAttack() {
        _currentSkill = _skillService.GetSkill(SkillType.AttackSkill);
        TryUpgradeCurrentSkill();
    }

    private void UpdateText() {
        SearchCurrentButtonItem();
        _currentButtonElement.UpdateElement(_currentSkill);
    }

    private void SearchCurrentButtonItem() {
        _currentButtonElement = _buttonsBuySkill.FirstOrDefault(x => x.GetSkillType() == _currentSkill.SkillType);
    }

    private void SubscribeButton() {
        _attackButton.onClick.AddListener(OnUpgradeAttack);
        _radiusButton.onClick.AddListener(OnUpgradeRadius);
        _shootTimeButton.onClick.AddListener(OnUpgradeShootTime);
    }

    private void UpdateAllText(params BaseSkill[] skill) {
        for (int i = 0; i < _buttonsBuySkill.Count; i++) {
            for (int j = 0; j < skill.Length; j++) {
                if (_buttonsBuySkill[i].GetSkillType() == skill[j].SkillType) {
                    _buttonsBuySkill[i].UpdateElement(skill[j]);
                }
            }
        }
    }
}

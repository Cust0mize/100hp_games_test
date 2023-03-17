using UnityEngine;
using TMPro;

public class ButtonBuySkill : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] public SkillType SkillType;

    public void UpdateElement(ISkill skill) {
        if (skill.Level >= skill.MaxLevel) {
            MaxLevel();
            return;
        }

        _level.text = $"Level: {skill.Level}";
        _price.text = $"Price: {skill.Level * skill.DefaultPrice}";
    }

    internal void MaxLevel() {
        _level.text = "Max";
        _price.text = "Max";
    }
}

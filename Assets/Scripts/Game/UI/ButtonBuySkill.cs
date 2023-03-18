using UnityEngine;
using TMPro;

public class ButtonBuySkill : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private SkillType _skillType;

    public void UpdateElement(BaseSkill skill) {
        if (skill.Level >= skill.MaxLevel) {
            MaxLevel();
            return;
        }

        _level.text = $"Level: {skill.Level}";
        _price.text = $"Price: {skill.Level * skill.DefaultPrice}";
    }

    public SkillType GetSkillType() {
        return _skillType;
    }

    public void MaxLevel() {
        _level.text = "Max";
        _price.text = "Max";
    }
}

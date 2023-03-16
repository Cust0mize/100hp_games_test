using UnityEngine;
using TMPro;

public class LowButtonItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] public SkillType SkillType;

    public void UpdateElement(ISkill skill) {
        if (skill.Level >= skill.MaxLevel) {
            MaxLevel();
            return;
        }

        _level.text = skill.Level.ToString();
        _price.text = (skill.Level * skill.DefaultPrice).ToString();
    }

    internal void MaxLevel() {
        _level.text = "Max";
        _price.text = "Max";
    }
}

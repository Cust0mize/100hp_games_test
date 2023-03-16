using UnityEngine;
using TMPro;

public abstract class IButtonElement : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI Level { get; }
    [field: SerializeField] public TextMeshProUGUI Price { get; }
    [field: SerializeField] public SkillType SkillType { get; }

    public void UpdateElement(ISkill skill) {
        Level.text = skill.Level.ToString();
        Price.text = (skill.Level * skill.DefaultPrice).ToString();
    }
}
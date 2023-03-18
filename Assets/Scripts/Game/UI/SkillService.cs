using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SkillService
{
    HashSet<ISkill> _skills = new HashSet<ISkill>();

    [Inject]
    public SkillService(ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        AddSkills(shootTimeSkill, attackSkill, radiusSkill);
    }

    public ISkill GetSkill(SkillType skillType) {
        ISkill result = null;
        foreach (var skillElement in _skills) {
            if (skillType == skillElement.SkillType) {
                result = skillElement;
            }
        }
        return result;
    }

    private void AddSkills(params ISkill[] skills) {
        _skills = skills.ToHashSet();
    }
}

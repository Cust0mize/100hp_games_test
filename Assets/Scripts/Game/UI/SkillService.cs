using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SkillService
{
    HashSet<ISkill> _skills;

    [Inject]
    public SkillService(ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        _skills = new HashSet<ISkill>() { shootTimeSkill, attackSkill, radiusSkill };
    }

    public ISkill GetSkill(SkillType skillType) {
        return _skills.FirstOrDefault(x => x.SkillType == skillType);
    }
}

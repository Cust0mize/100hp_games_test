using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SkillService
{
    HashSet<BaseSkill> _skills;

    [Inject]
    public SkillService(ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        _skills = new HashSet<BaseSkill>() { shootTimeSkill, attackSkill, radiusSkill };
    }

    public BaseSkill GetSkill(SkillType skillType) {
        return _skills.FirstOrDefault(x => x.SkillType == skillType);
    }
}

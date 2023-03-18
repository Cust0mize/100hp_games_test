using Zenject;

public class SkillService
{
    private ShootTimeSkill _shootTimeSkill;
    private AttackSkill _attackSkill;
    private RadiusSkill _radiusSkill;

    [Inject]
    public SkillService(ShootTimeSkill shootTimeSkill, AttackSkill attackSkill, RadiusSkill radiusSkill) {
        _shootTimeSkill = shootTimeSkill;
        _attackSkill = attackSkill;
        _radiusSkill = radiusSkill;
    }

    public ShootTimeSkill GetShootTimeSkill()
    {
        return _shootTimeSkill;
    }    
    
    public AttackSkill GetAttackSkill()
    {
        return _attackSkill;
    }    
    
    public RadiusSkill GetRadiusSkill()
    {
        return _radiusSkill;
    }
}
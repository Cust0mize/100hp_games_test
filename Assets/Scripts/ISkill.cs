public interface ISkill
{
    public SkillType SkillType { get; }
    public float DefaultPrice { get; }
    public int Level { get; }
    public int MaxLevel { get; }
    public bool Upgrade();
}

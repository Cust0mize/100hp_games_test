public interface ISkill
{
    public SkillType Type { get; }
    public float DefaultPrice { get; }
    public int Level { get; set; }
    public int MaxLevel { get; }
    public bool Upgrade();
}

public interface ISkill
{
    public float DefaultPrice { get; }
    public int Level { get; set; }
    public void Upgrade(GameSaver gameSaver);
}

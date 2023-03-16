public interface ISkill1
{
    float DefaultPrice { get; }
    int Level { get; set; }

    void Upgrade();
}
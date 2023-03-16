using Zenject;

public class ShootTimeSkill : ISkill
{
    private SignalBus _signalBus;
    private float _defaultPrice = 10;
    private int _level = 1;

    public float DefaultPrice => _defaultPrice;
    public int Level { get => _level; set => _level = value; }

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void Upgrade(GameSaver gameSaver) {
        gameSaver.SetShootTime(gameSaver.GetShootTime() * 0.75f);
        gameSaver.SetCurrentCoint(_level * -_defaultPrice);
        _signalBus.Fire<SignalUpdateShootTime>();
    }
}

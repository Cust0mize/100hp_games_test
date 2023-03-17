using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private UIRoot uiRoot;

    public override void InstallBindings() {
        BindBaseSceneDependencies();
        BindSkillDependencies();
        //BindMemoryPools();
        BindSignals();
        BindUI();
    }

    private void BindUI() {
        Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle();

    }

    private void BindSkillDependencies() {
        Container.Bind<AttackSkill>().AsSingle();
        Container.Bind<RadiusSkill>().AsSingle();
        Container.Bind<ShootTimeSkill>().AsSingle();
    }

    private void BindSignals() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SignalGameOver>();
        Container.DeclareSignal<SignalNewWave>();
        Container.DeclareSignal<SignalRemoveEnemy>();
        Container.DeclareSignal<SignalUpdateRadius>();
        Container.DeclareSignal<SignalUpdateShootTime>();
        Container.DeclareSignal<SignalUpdateCoinValue>();
    }

    private void BindBaseSceneDependencies() {

        Container.Bind<GameSaver>().AsSingle();

    }
}
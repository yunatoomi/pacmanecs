﻿using UnityEngine;
using Leopotam.Ecs;
using Death.Systems;
using Ghosts.Systems;
using Items.Food.Systems;
using Items.Systems;
using Moving.Systems;
using Players.Systems;
using Portals.Systems;
using Teleports.Systems;
using Ui.GameStates;
using Ui.GameStates.Systems;
using Ui.ScoreTable.Systems;
using Walls.Systems;
using World.Systems;

public sealed class Startup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    private void OnEnable()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

        _systems
            .Add(new WorldInitSystem())
            .Add(new PlayerInitSystem())
            .Add(new GhostInitSystem())
            .Add(new WallInitSystem())
            .Add(new PortalInitSystem())
            .Add(new FoodInitSystem())
            .Add(new ScoreTableInitSystem())
            .Add(new GameStateInitSystem())
            .Add(new PlayerInputSystem())
            .Add(new GhostSystem())
            .Add(new UpdateDirectionSystem())
            .Add(new MoveSystem())
            .Add(new ItemSystem())
            .Add(new FoodSystem())
            .Add(new EnergizerSystem())
            .Add(new GhostFearStateSystem())
            .Add(new DeathSystem())
            .Add(new PortalSystem())
            .Add(new TeleportSystem())
            .Add(new WorldSystem())
            .Add(new ScoreTableSystem())
            .Add(new GameStateSystem())
            .Initialize();
    }

    private void Update()
    {
        _systems.Run();
        _world.RemoveOneFrameComponents();
    }

    private void OnDisable()
    {
        _systems.Dispose();
        _systems = null;
        _world.Dispose();
        _world = null;
    }

    public void RestartGame()
    {
        _world.CreateEntityWith<ChangeGameStateEvent>().State = GameStates.RESTART;
    }

    public void ContinueGame()
    {
        _world.CreateEntityWith<ChangeGameStateEvent>().State = GameStates.START;
    }

    public void QuitGame()
    {
        _world.CreateEntityWith<ChangeGameStateEvent>().State = GameStates.EXIT;
    }
}
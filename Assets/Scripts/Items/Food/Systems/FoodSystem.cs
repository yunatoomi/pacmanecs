﻿using Leopotam.Ecs;
using Moving;
using Players;
using Ui.ScoreTable;

namespace Items.Food.Systems
{
    [EcsInject]
    public class FoodSystem : IEcsRunSystem
    {
        private readonly EcsWorld _ecsWorld = null;
        private readonly EcsFilter<FoodComponent, TakenItemComponent> _takenFoods = null;

        public void Run()
        {
            for (int i = 0; i < _takenFoods.EntitiesCount; i++)
            {
                int playerEntity = _takenFoods.Components2[i].PlayerEntity;
                var player = _ecsWorld.GetComponent<PlayerComponent>(playerEntity);
                var moveComponent = _ecsWorld.GetComponent<MoveComponent>(playerEntity);

                player.Scores += _takenFoods.Components1[i].Scores;
                moveComponent.Speed -= _takenFoods.Components1[i].SpeedPenalty;

                _ecsWorld.CreateEntityWith<UpdateScoreTableEvent>();
            }
        }
    }
}
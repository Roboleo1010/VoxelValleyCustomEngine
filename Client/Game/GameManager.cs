using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Engine.Core.ComponentSystem;
using VoxelValley.Engine.Core.ComponentSystem.Components;
using VoxelValley.Engine.Core.Diagnostics;
using VoxelValley.Engine.Graphics;
using VoxelValley.Game.Entities;
using VoxelValley.Game.Enviroment;
using VoxelValley.Game.Input;


namespace VoxelValley.Game
{
    public class GameManager : GameObject
    {
        Type type = typeof(GameManager);
        float time = 0;

        public GameManager(string name) : base(name)
        {

        }

        protected override void OnStart()
        {
            ReferencePointer.GameManager = this;

            ReferencePointer.World = new World("World", this.gameObject);
            this.AddChild(ReferencePointer.World);

            ReferencePointer.Player = new Player("Player", this.gameObject, new Vector3(0, Constants.World.chunkSize.Y, 0));
            this.AddChild(ReferencePointer.Player);
        }

        protected override void OnUpdate(float deltaTime)
        {
            time += (float)deltaTime;
        }
    }
}
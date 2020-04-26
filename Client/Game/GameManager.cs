using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common.ComponentSystem;
using VoxelValley.Common.Enviroment;
using VoxelValley.Game;

namespace VoxelValley.Client.Game
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
using System;
using System.Drawing;
using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment.Generation
{
    public class VoronoiDiagram
    {
        Vector2i worldPos;
        Vector2i cellCount;
        int cellSize;
        float jitterFactor;

        public Vector2i[,] Seeds { get; private set; }
        public int[,] Regions { get; private set; }//stores unique identifier for each region
        public int[,] Diagram { get; private set; }

        public VoronoiDiagram(Vector2i worldPos, Vector2i cellCount, int cellSize, float jitterFactor)
        {
            this.worldPos = worldPos;
            this.cellCount = cellCount;
            this.cellSize = cellSize;
            this.jitterFactor = jitterFactor;
        }

        public void GenerateDiagram()
        {
            GenerateVoronoiBaseData();

            Diagram = new int[cellCount.X * cellSize, cellCount.Y * cellSize];

            for (int x = 0; x < cellCount.X * cellSize; x++)
                for (int y = 0; y < cellCount.Y * cellSize; y++)
                {
                    Vector2i closestSeedIndex = GetClosestSeedIndex(new Vector2i(x, y));

                    Diagram[x, y] = Regions[closestSeedIndex.X, closestSeedIndex.Y];
                }
        }

        /// <summary>
        /// Generates Seeds & Region identifiers
        /// </summary>
        void GenerateVoronoiBaseData()
        {
            Seeds = new Vector2i[cellCount.X, cellCount.Y];
            Regions = new int[cellCount.X, cellCount.Y];

            Random random = new Random();

            int regionIndex = 0;

            for (int x = 0; x < cellCount.X; x++)
                for (int y = 0; y < cellCount.Y; y++)
                {
                    int baseX = ((cellSize / 2) + (x * cellSize));
                    int baseY = ((cellSize / 2) + (y * cellSize));

                    int jitter = random.Next((int)-jitterFactor, (int)jitterFactor); //TODO Generate seeded
                    //int jitter = (int)((GenerationUtilities.FBMPerlin(worldPos.X + x, worldPos.Y + y, 1, 2, 10) * 2 - 1) * jitterFactor);

                    Seeds[x, y] = new Vector2i(baseX + jitter, baseY + jitter);
                    Regions[x, y] = regionIndex++;
                }
        }

        Vector2i GetClosestSeedIndex(Vector2i currentPosition)
        {
            double smallestDistance = double.MaxValue;
            Vector2i index = Vector2i.Zero;

            for (int x = 0; x < cellCount.X; x++)
                for (int y = 0; y < cellCount.Y; y++)
                {
                    double d1 = Vector2.Distance(currentPosition.ToVector2(), Seeds[x, y].ToVector2());

                    double d2 = Math.Sqrt(Math.Pow(currentPosition.X - Seeds[x, y].X, 2) + Math.Pow(currentPosition.Y - Seeds[x, y].Y, 2));

                    if (d2 < smallestDistance)
                    {
                        smallestDistance = d2;
                        index = new Vector2i(x, y);
                    }
                }

            return index;
        }

        public Color[,] GetColors()
        {
            Color[] regionColors = new Color[cellCount.X * cellCount.Y];
            Random random = new Random();

            Color[,] colors = new Color[cellCount.X * cellSize, cellCount.Y * cellSize];

            for (int i = 0; i < regionColors.Length; i++)
                regionColors[i] = Color.FromArgb(255, (int)(random.NextDouble() * 255), (int)(random.NextDouble() * 255), (int)(random.NextDouble() * 255));

            for (int x = 0; x < cellCount.X * cellSize; x++)
                for (int y = 0; y < cellCount.Y * cellSize; y++)
                    colors[x, y] = regionColors[Diagram[x, y]];

            for (int x = 0; x < cellCount.X; x++)
                for (int y = 0; y < cellCount.Y; y++)
                    colors[Seeds[x, y].X, Seeds[x, y].Y] = Color.Black;

            return colors;
        }
    }
}
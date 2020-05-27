using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Kaitai.Core.Formats;

namespace VoxelValley.Tools.ModelConverter
{
    public class MagicaVoxelParser : ModelParser
    {
        List<MagicavoxelVox.Voxel> mVoxels;
        List<MagicavoxelVox.Color> colors;

        public MagicaVoxelParser(string inputPath, string outputPath, string name) : base(inputPath, outputPath, name) { }

        public override void Parse()
        {
            GetData();
            ConvertToVoxels();
        }

        void GetData()
        {
            Console.WriteLine("Loading MagicaVoxel Data...");

            MagicavoxelVox model = MagicavoxelVox.FromFile(InputPath);

            mVoxels = ((MagicavoxelVox.Xyzi)model.Main.ChildrenChunks[1].ChunkContent).Voxels;
            colors = ((MagicavoxelVox.Rgba)model.Main.ChildrenChunks[14].ChunkContent).Colors;

            Console.WriteLine($"Found {mVoxels.Count} voxels and {colors.Count} colors");
        }

        void ConvertToVoxels()
        {
            List<Voxel> voxels = new List<Voxel>();

            foreach (MagicavoxelVox.Voxel mVoxel in mVoxels)
            {
                MagicavoxelVox.Color mColor = colors.ElementAt(mVoxel.ColorIndex);
                voxels.Add(new Voxel(Color.FromArgb(mColor.A, mColor.R, mColor.G, mColor.B), new int[] { mVoxel.X, mVoxel.Z, mVoxel.Y }));
            }

            Voxels = voxels.ToArray();
        }
    }
}
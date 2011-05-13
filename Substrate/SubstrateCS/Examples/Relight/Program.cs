﻿using System;
using Substrate;

namespace Relight
{
    class Program
    {
        static void Main (string[] args)
        {
            if (args.Length < 1) {
                Console.WriteLine("You must specify a target directory");
                return;
            }
            string dest = args[0];

            // Load the world, supporting either alpha or beta format
            INBTWorld world;
            if (args.Length >= 2 && args[1] == "alpha") {
                world = AlphaWorld.Open(dest);
            }
            else {
                world = BetaWorld.Open(dest);
            }

            // Grab a generic chunk manager reference
            IChunkManager cm = world.GetChunkManager();

            // First blank out all of the lighting in all of the chunks
            foreach (ChunkRef chunk in cm) {
                if (chunk.X < -20 || chunk.X > 0 || chunk.Z < -20 || chunk.Z > 0) continue;

                chunk.Blocks.RebuildHeightMap();
                chunk.Blocks.ResetBlockLight();
                chunk.Blocks.ResetBlockSkyLight();
                cm.Save();

                Console.WriteLine("Reset Chunk {0},{1}", chunk.X, chunk.Z);
            }

            // In a separate pass, reconstruct the light
            foreach (ChunkRef chunk in cm) {
                if (chunk.X < -20 || chunk.X > 0 || chunk.Z < -20 || chunk.Z > 0) continue;

                chunk.Blocks.RebuildBlockLight();
                chunk.Blocks.RebuildBlockSkyLight();

                // Save the chunk to disk so it doesn't hang around in RAM
                cm.Save();

                Console.WriteLine("Lit Chunk {0},{1}", chunk.X, chunk.Z);
            }
        }
    }
}
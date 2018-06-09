
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Substrate.Core;
using Substrate.Nbt;
using Substrate.Source.Nbt;

namespace Substrate.Tests
{
    [TestClass]
    public class LevelTests
    {
        NbtTree LoadLevelTree(string path)
        {
            NBTFile nf = new NBTFile(path);
            NbtTree tree = null;

            using (Stream nbtstr = nf.GetDataInputStream())
            {
                if (nbtstr == null)
                {
                    return null;
                }

                tree = new NbtTree(nbtstr);
            }

            return tree;
        }

        [TestMethod]
        public void LoadTreeTest_1_6_4_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_6_4-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_7_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_7_2-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_7_10_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_7_10-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_8_3_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_8_3-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_9_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_9_2-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_12_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\level.dat");

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root, out NbtVerificationResults verificationResults);
            Assert.IsNotNull(level);


            NbtTree mineshaftTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\Mineshaft.dat");
            //Assert.IsTrue(new NbtVerifier(mineshaftTree.Root, _schema).Verify());

            NbtTree templeTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\Temple.dat");

            NbtTree villageTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\Village.dat");

            NbtTree villagesTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\villages.dat");
            Assert.IsTrue(NbtVerifier.Verify(villagesTree.Root, Villages.Schema));

            NbtTree villagesEndTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\villages_end.dat");
            Assert.IsTrue(NbtVerifier.Verify(villagesEndTree.Root, Villages.Schema));

            NbtTree villagesNetherTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\data\villages_nether.dat");
            Assert.IsTrue(NbtVerifier.Verify(villagesNetherTree.Root, Villages.Schema));
        }

        [TestMethod]
        public void LoadTreeTest_1_12_2_survival_SchemaBuilderLoader()
        {
            NbtTree levelTree = LoadLevelTree(@"..\..\Data\1_12_2-survival\level.dat");
            Assert.IsTrue(NbtVerifier.Verify(levelTree.Root, Level.Schema));

            var level = new Level(null);
            var level2 = new Level(null);

            SchemaBuilder.LoadCompound(level, levelTree.Root, Level.Schema);

            level2.LoadTree(levelTree.Root);

            Assert.IsNotNull(level);
            Assert.IsNotNull(level2);
        }

        [TestMethod]
        public void OpenTest_Climatic_Islands_survival()
        {
            if (!Directory.Exists(@"..\..\Data\Climatic Islands [ENG]\"))
            {
                Assert.Inconclusive("Level not found, skipping test");
            }

            NbtWorld world = NbtWorld.Open(@"..\..\Data\Climatic Islands [ENG]\");
            Assert.IsNotNull(world);

            NbtTree villagesNetherTree = LoadLevelTree(@"..\..\Data\Climatic Islands [ENG]\level.dat");
            Assert.IsTrue(NbtVerifier.Verify(villagesNetherTree.Root, Level.Schema));
        }
    }
}

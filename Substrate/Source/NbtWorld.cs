using System;
using Substrate.Core;
using Substrate.Nbt;
using Substrate.Data;

namespace Substrate
{
    /// <summary>
    /// An abstract representation of any conforming chunk-based world.
    /// </summary>
    public abstract class NbtWorld
    {
        private const string _DATA_DIR = "data";

		/// <summary>
		/// Gets or sets the path to the directory containing the world.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the directory containing data resources, rooted in the world directory.
		/// </summary>
		public string DataDirectory { get; set; } = _DATA_DIR;

		/// <summary>
		/// Gets a reference to this world's <see cref="Level"/> object.
		/// </summary>
		public abstract Level Level { get; }

		/// <summary>
		/// Gets an <see cref="IBlockManager"/> for the given dimension.
		/// </summary>
		/// <param name="dim">The id of the dimension to look up.</param>
		/// <returns>An <see cref="IBlockManager"/> tied to the given dimension in this world.</returns>
		public IBlockManager GetBlockManager(int dim = Dimension.DEFAULT) => GetBlockManagerVirt(dim);

		public IBlockManager GetBlockManager(string dim) => GetBlockManagerVirt(dim);

		/// <summary>
		/// Gets an <see cref="IChunkManager"/> for the given dimension.
		/// </summary>
		/// <param name="dim">The id of the dimension to look up.</param>
		/// <returns>An <see cref="IChunkManager"/> tied to the given dimension in this world.</returns>
		public IChunkManager GetChunkManager(int dim = Dimension.DEFAULT) => GetChunkManagerVirt(dim);

		public IChunkManager GetChunkManager(string dim) => GetChunkManagerVirt(dim);

		/// <summary>
		/// Gets an <see cref="IPlayerManager"/> for maanging players on multiplayer worlds.
		/// </summary>
		/// <returns>An <see cref="IPlayerManager"/> for this world.</returns>
		public IPlayerManager GetPlayerManager() => GetPlayerManagerVirt();

		/// <summary>
		/// Gets a <see cref="DataManager"/> for managing data resources, such as maps.
		/// </summary>
		/// <returns>A <see cref="DataManager"/> for this world.</returns>
		public DataManager GetDataManager() => GetDataManagerVirt();

		/// <summary>
		/// Attempts to determine the best matching world type of the given path, and open the world as that type.
		/// </summary>
		/// <param name="path">The path to the directory containing the world.</param>
		/// <returns>A concrete <see cref="NbtWorld"/> type, or null if the world cannot be opened or is ambiguos.</returns>
		public static NbtWorld Open (string path, out NbtErrors errors)
        {
            OpenWorldEventArgs eventArgs = new OpenWorldEventArgs(path);

			NbtWorld world;
			bool isCorrectVersion;

			if (AnvilWorld.TryOpen(path, out world, out errors, out isCorrectVersion) || isCorrectVersion)
				return world;
			if (BetaWorld.TryOpen(path, out world, out errors, out isCorrectVersion) || isCorrectVersion)
				return world;
			if (AlphaWorld.TryOpen(path, out world, out errors, out isCorrectVersion) || isCorrectVersion)
				return world;

			errors = NbtErrors.FromMessage(NbtErrorKind.Error_IOError, "Unknown world file format.");
            return null;
        }

        /// <summary>
        /// Saves the world's <see cref="Level"/> data, and any <see cref="IChunk"/> objects known to have unsaved changes.
        /// </summary>
        public abstract void Save ();

        #region Covariant Return-Type Helpers

        /// <summary>
        /// Virtual implementor of <see cref="GetBlockManager(int)"/>.
        /// </summary>
        /// <param name="dim">The given dimension to fetch an <see cref="IBlockManager"/> for.</param>
        /// <returns>An <see cref="IBlockManager"/> for the given dimension in the world.</returns>
        protected abstract IBlockManager GetBlockManagerVirt (int dim);

        /// <summary>
        /// Virtual implementor of <see cref="GetChunkManager(int)"/>.
        /// </summary>
        /// <param name="dim">The given dimension to fetch an <see cref="IChunkManager"/> for.</param>
        /// <returns>An <see cref="IChunkManager"/> for the given dimension in the world.</returns>
        protected abstract IChunkManager GetChunkManagerVirt (int dim);

		protected virtual IBlockManager GetBlockManagerVirt(string dim) => throw new NotImplementedException();

		protected virtual IChunkManager GetChunkManagerVirt(string dim) => throw new NotImplementedException();

		/// <summary>
		/// Virtual implementor of <see cref="GetPlayerManager"/>.
		/// </summary>
		/// <returns>An <see cref="IPlayerManager"/> for the given dimension in the world.</returns>
		protected abstract IPlayerManager GetPlayerManagerVirt ();

		/// <summary>
		/// Virtual implementor of <see cref="GetDataManager"/>
		/// </summary>
		/// <returns>A <see cref="DataManager"/> for the given dimension in the world.</returns>
		protected virtual DataManager GetDataManagerVirt() => throw new NotImplementedException();

		#endregion
	}
}

namespace Substrate
{
	/// <summary>
	/// Specifies the type of gameplay associated with a world.
	/// </summary>
	public enum GameType
    {
        /// <summary>
        /// The world will be played in Survival mode.
        /// </summary>
        SURVIVAL = 0,

        /// <summary>
        /// The world will be played in Creative mode.
        /// </summary>
        CREATIVE = 1,

        /// <summary>
        /// The world will be played in Adventure mode.
        /// </summary>
        ADVENTURE = 2,

        /// <summary>
        /// The world will be played in Spectator mode.
        /// </summary>
        SPECTATOR = 3,
    }
}

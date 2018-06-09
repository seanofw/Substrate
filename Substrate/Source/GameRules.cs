using Substrate.Core;

namespace Substrate
{
	/// <summary>
	/// Encompases data to specify game rules.
	/// </summary>
	public class GameRules : ICopyable<GameRules>
    {
        private bool _commandBlockOutput = true;
        private bool _doFireTick = true;
        private bool _doMobLoot = true;
        private bool _doMobSpawning = true;
        private bool _doTileDrops = true;
        private bool _keepInventory = false;
        private bool _mobGriefing = true;
        private bool _doDaylightCycle = true;
        private bool _logAdminCommands = true;
        private bool _naturalRegeneration = true;
        private int _randomTickSpeed = 3;
        private bool _sendCommandFeedback = true;
        private bool _showDeathMessages = true;

        /// <summary>
        /// Gets or sets whether or not actions performed by command blocks are displayed in the chat.
        /// </summary>
        public bool CommandBlockOutput
        {
            get { return _commandBlockOutput; }
            set { _commandBlockOutput = value; }
        }

        /// <summary>
        /// Gets or sets whether to spread or remove fire.
        /// </summary>
        public bool DoFireTick
        {
            get { return _doFireTick; }
            set { _doFireTick = value; }
        }

        /// <summary>
        /// Gets or sets whether mobs should drop loot when killed.
        /// </summary>
        public bool DoMobLoot
        {
            get { return _doMobLoot; }
            set { _doMobLoot = value; }
        }

        /// <summary>
        /// Gets or sets whether mobs should spawn naturally.
        /// </summary>
        public bool DoMobSpawning
        {
            get { return _doMobSpawning; }
            set { _doMobSpawning = value; }
        }

        /// <summary>
        /// Gets or sets whether breaking blocks should drop the block's item drop.
        /// </summary>
        public bool DoTileDrops
        {
            get { return _doTileDrops; }
            set { _doTileDrops = value; }
        }

        /// <summary>
        /// Gets or sets whether players keep their inventory after they die.
        /// </summary>
        public bool KeepInventory
        {
            get { return _keepInventory; }
            set { _keepInventory = value; }
        }

        /// <summary>
        /// Gets or sets whether mobs can destroy blocks (creeper explosions, zombies breaking doors, etc.).
        /// </summary>
        public bool MobGriefing
        {
            get { return _mobGriefing; }
            set { _mobGriefing = value; }
        }

        /// <summary>
        /// Whether to do the Daylight Cycle or not. True by default.
        /// </summary>
        public bool DoDaylightCycle
        {
            get { return _doDaylightCycle; }
            set { _doDaylightCycle = value; }
        }

        /// <summary>
        /// Whether to log admin commands to server log. True by default.
        /// </summary>
        public bool LogAdminCommands
        {
            get { return _logAdminCommands; }
            set { _logAdminCommands = value; }
        }

        /// <summary>
        /// Whether the player naturally regenerates health if hunger is high enough. True by default.
        /// </summary>
        public bool NaturalRegeneration
        {
            get { return _naturalRegeneration; }
            set { _naturalRegeneration = value; }
        }

        /// <summary>
        /// How often a random tick occurs, such as plant growth, leaf decay, etc. 3 by default.
        /// </summary>
        public int RandomTickSpeed
        {
            get { return _randomTickSpeed; }
            set { _randomTickSpeed = value; }
        }

        /// <summary>
        /// Whether the feedback from commands executed by a player should show up in chat. True by default.
        /// </summary>
        public bool SendCommandFeedback
        {
            get { return _sendCommandFeedback; }
            set { _sendCommandFeedback = value; }
        }

        /// <summary>
        /// Whether a message appears in chat when a player dies. True by default.
        /// </summary>
        public bool ShowDeathMessages
        {
            get { return _showDeathMessages; }
            set { _showDeathMessages = value; }
        }

        #region ICopyable<GameRules> Members

        /// <inheritdoc />
        public GameRules Copy ()
        {
            GameRules gr = new GameRules();
            gr._commandBlockOutput = _commandBlockOutput;
            gr._doFireTick = _doFireTick;
            gr._doMobLoot = _doMobLoot;
            gr._doMobSpawning = _doMobSpawning;
            gr._doTileDrops = _doTileDrops;
            gr._keepInventory = _keepInventory;
            gr._mobGriefing = _mobGriefing;
            gr._doDaylightCycle = _doDaylightCycle;
            gr._logAdminCommands = _logAdminCommands;
            gr._naturalRegeneration = _naturalRegeneration;
            gr._randomTickSpeed = _randomTickSpeed;
            gr._sendCommandFeedback = _sendCommandFeedback;
            gr._showDeathMessages = _showDeathMessages;

            return gr;
        }

        #endregion
    }
}

using Substrate.Nbt;

namespace Substrate
{
	public class LevelData
    {
        [TagNode]
        public long Time { get; set; }

        [TagNode]
        public Player Player { get; set; }
    }
}

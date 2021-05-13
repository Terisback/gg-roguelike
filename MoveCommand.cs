namespace DungeonMaster
{
    public class MoveCommand : Command
    {
        private Room target;

        public MoveCommand(Dungeon dungeon, Room target) : base(dungeon)
        {
            this.target = target;
        }

        public override bool Execute()
        {
            dungeon.SetRoom(target);
            return true;
        }
    }
}
namespace DungeonMaster
{
    public abstract class Command
    {
        protected Dungeon dungeon;
        protected Room previousRoom;

        public Command(Dungeon dungeon)
        {
            this.dungeon = dungeon;
            previousRoom = this.dungeon.GetCurrentRoom();
        }

        public void Undo()
        {
            dungeon.SetRoom(previousRoom);
        }

        // Returns true if it changed current dungeon room
        public virtual bool Execute() { return false; }
    }
}
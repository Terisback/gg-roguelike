using System.Collections.Generic;
namespace DungeonMaster
{
    public class Dungeon
    {
        private static Dungeon instance;
        private static CommandHistory history;
        private static Room currentRoom;

        private Dungeon()
        {
            history = new CommandHistory();
        }

        public static Dungeon GetInstance()
        {
            if (instance == null)
            {
                instance = new Dungeon();
            }
            return instance;
        }

        public void GenerateRooms()
        {
            Room parent = new Room(0, 0, Color.None);
            RoomGenerator rg = new RoomGenerator(parent, 6);
            rg.Generate();
            currentRoom = parent;
        }

        public Room GetCurrentRoom()
        {
            return currentRoom;
        }

        public void SetRoom(Room room)
        {
            currentRoom = room;
        }

        public void MoveTo(Room target)
        {
            MoveCommand mc = new MoveCommand(instance, target);
            if (mc.Execute())
            {
                history.Push(mc);
            }
        }

        public void Undo()
        {
            history.Pop()?.Undo();
        }
    }
}
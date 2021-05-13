using System;
using System.Collections.Generic;
namespace DungeonMaster
{
    public class RoomGenerator
    {
        private static Dictionary<(int, int), Room> rooms; // In case you need to check if such rooms exist
        private static int roomCount;

        private Room parent;

        public RoomGenerator(Room parent, int roomCount)
        {
            this.parent = parent;
            RoomGenerator.roomCount = roomCount;
            rooms = new Dictionary<(int, int), Room>();
            rooms.Add((parent.X, parent.Y), parent);
        }

        public void Generate()
        {
            if (roomCount <= 0) return;

            Random r = new Random();

            // One branch generation
            // For multiple branches we can do IEnumerator RoomGenerator that will yield after every room
            while (roomCount > 0)
            {
                bool dir = r.Next() % 2 == 0 ? true : false;
                Room child = new Room(parent.X + (dir ? 1 : 0), parent.Y + (dir ? 0 : 1), (Color)r.Next(4));
                rooms.Add((child.X, child.Y), child);
                parent.Connect(child);
                parent = child;
                roomCount--;
            }
        }
    }
}

using System.Collections.Generic;
namespace DungeonMaster
{
    public class Room
    {
        public readonly int X;
        public readonly int Y;
        public readonly Color Color; // Kind of Room
        public HashSet<Room> Connected { get; private set; } // Connected rooms

        public Room(int X, int Y, Color Color)
        {
            this.X = X;
            this.Y = Y;
            this.Color = Color;
            Connected = new HashSet<Room>();
        }

        // Add connection between this room and other
        public void Connect(Room other)
        {
            Connected.Add(other);
            other._Connect(this);
        }

        private void _Connect(Room other)
        {
            Connected.Add(other);
        }
    }
}
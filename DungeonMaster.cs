using System;
using Console = Colorful.Console;
using System.Drawing;

namespace DungeonMaster
{
    class DungeonMaster
    {

        static Dungeon dungeon;
        static Room parentRoom;
        static Room currentRoom;

        static void Main(string[] args)
        {
            dungeon = Dungeon.GetInstance();
            dungeon.GenerateRooms();
            parentRoom = dungeon.GetCurrentRoom();

            bool redraw = true;
            while (true)
            {
                if (redraw)
                {
                    // Works only for one not looped branch
                    currentRoom = dungeon.GetCurrentRoom();
                    DisplayRooms(parentRoom);
                    redraw = false;
                }

                var ch = Console.ReadKey().Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        redraw = CheckAndMove(Direction.Up);
                        break;
                    case ConsoleKey.LeftArrow:
                        redraw = CheckAndMove(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        redraw = CheckAndMove(Direction.Right);
                        break;
                    case ConsoleKey.DownArrow:
                        redraw = CheckAndMove(Direction.Down);
                        break;
                    case ConsoleKey.R:
                        dungeon.GenerateRooms();
                        parentRoom = dungeon.GetCurrentRoom();
                        currentRoom = parentRoom;
                        redraw = true;
                        break;
                    case ConsoleKey.Z:
                        dungeon.Undo();
                        currentRoom = dungeon.GetCurrentRoom();
                        redraw = true;
                        break;
                    default:
                        break;
                }
            }
        }

        enum Direction
        {
            Up,
            Left,
            Right,
            Down,
        }

        static bool CheckAndMove(Direction dir)
        {
            int checkX = currentRoom.X;
            int checkY = currentRoom.Y;
            switch (dir)
            {
                case Direction.Up:
                    checkY--;
                    break;
                case Direction.Left:
                    checkX--;
                    break;
                case Direction.Right:
                    checkX++;
                    break;
                case Direction.Down:
                    checkY++;
                    break;
            }
            foreach (var room in currentRoom.Connected)
            {
                if (checkX == room.X && checkY == room.Y)
                {
                    dungeon.MoveTo(room);
                    return true;
                }
            }
            return false;
        }

        static void DisplayRooms(Room parent)
        {
            // Go to (0,0)
            while (parent.X != 0 && parent.Y != 0)
            {
                foreach (var room in parent.Connected)
                {
                    if (room.X < parent.X || room.Y < parent.Y)
                    {
                        parent = room;
                        break;
                    }
                }
                Console.WriteLine((parent.X, parent.Y));
            }

            Console.Clear();
            DrawRoom(parent, parent);
            Room previousRoom = null;
            while (true)
            {
                foreach (var room in parent.Connected)
                {
                    if (room == previousRoom) continue;
                    DrawRoom(parent, room);
                    previousRoom = parent;
                    parent = room;
                    break;
                }
                if (parent.Connected.Count == 1)
                    if (parent.Connected.Contains(previousRoom)) break;
            }
            WriteAt("Use Arrow keys for moving between rooms | <R> for regenerate rooms | <Z> for undo", 0, Console.CursorTop + 2, Color.None);
        }

        protected static void DrawRoom(Room parent, Room room)
        {
            int leftMargin = 4 + room.X * 7;
            int upMargin = 2 + room.Y * 4;
            if (parent != room)
            {
                if (parent.X < room.X)
                {
                    WriteAt("==", leftMargin - 2, upMargin + 1, Color.None);
                }
                else
                {
                    WriteAt("|||", leftMargin + 1, upMargin - 1, Color.None);
                }
            }
            WriteAt("+---+", leftMargin, upMargin, room.Color);
            if (room.X == currentRoom.X && room.Y == currentRoom.Y)
            {
                WriteAt("| A |", leftMargin, upMargin + 1, room.Color);
            }
            else
            {
                WriteAt("|///|", leftMargin, upMargin + 1, room.Color);
            }
            WriteAt("+---+", leftMargin, upMargin + 2, room.Color);
        }

        protected static int origRow = 0;
        protected static int origCol = 0;

        protected static void WriteAt(string s, int x, int y, Color color)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s, color.KnownColor());
            }
            catch { }
        }
    }
}

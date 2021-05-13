using System.Collections.Generic;

namespace DungeonMaster
{
    public class CommandHistory
    {
        private Stack<Command> history;

        public CommandHistory() => history = new Stack<Command>();

        public void Push(Command command)
        {
            history.Push(command);
        }

        public Command Pop()
        {
            if (history.Count > 0)
            {
                return history.Pop();
            }
            else
            {
                return null;
            }
        }
    }
}
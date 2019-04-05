using System.Collections.Generic;
using UnityEngine;

namespace GameSys.Commands
{
    public class CommandRegistry
    {
        private static CommandRegistry instance;
        /// <summary>
        /// Returns the singleton reference of the command registry.
        /// </summary>
        public static CommandRegistry GetInstance()
        {
            return instance ?? (instance = new CommandRegistry());
        }

        private Dictionary<string, ICommand> registry = null;
        private List<string> unsafeCommands = null;

        private CommandRegistry()
        {
            registry = new Dictionary<string, ICommand>();
            unsafeCommands = new List<string>();
        }

        /// <summary>
        /// Register a command in the registry.
        /// </summary>
        public void Register(string trigger, ICommand command, bool isUnsafe = true)
        {
            if (registry.ContainsKey(trigger))
            {
                Debug.Log(string.Format("[CommandRegistry] Duplicate command trigger {0} found in registry.", trigger));
                return;
            }

            registry.Add(trigger, command);

            if (isUnsafe)
            {
                unsafeCommands.Add(trigger);
            }
        }

        /// <summary>
        /// Unregisters a command from the registry.
        /// </summary>
        public void Unregister(string trigger)
        {
            if (unsafeCommands.Contains(trigger))
            {
                registry.Remove(trigger);
                unsafeCommands.Remove(trigger);
            }
        }

        /// <summary>
        /// Clears all unsafe commands from the registry.
        /// </summary>
        public void ClearRegistry()
        {
            foreach (string trigger in unsafeCommands)
            {
                registry.Remove(trigger);
            }
            unsafeCommands.Clear();
        }

        /// <summary>
        /// Execute a registered command with no parameters. Returns false if the command trigger isn't registered.
        /// </summary>
        public bool Execute(string trigger, System.Action<bool> commandComplete = null)
        {
            ICommand command;
            if (registry.TryGetValue(trigger, out command))
            {
                command.Execute();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Execute a registered command with parameters. Returns false if the command trigger isn't registered.
        /// </summary>
        public bool Execute(string trigger, string[] args, System.Action<bool> commandComplete = null)
        {
            ICommand command;
            if (registry.TryGetValue(trigger, out command))
            {
                switch (args.Length)
                {
                    case 0:
                        command.Execute(commandComplete);
                        return true;
                    case 1:
                        command.Execute(commandComplete, args[0]);
                        return true;
                    case 2:
                        command.Execute(commandComplete, args[0], args[1]);
                        return true;
                    case 3:
                        command.Execute(commandComplete, args[0], args[1], args[2]);
                        return true;
                    case 4:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3]);
                        return true;
                    case 5:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4]);
                        return true;
                    case 6:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4], args[5]);
                        return true;
                    case 7:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                        return true;
                    case 8:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                        return true;
                    case 9:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                        return true;
                    case 10:
                        command.Execute(commandComplete, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);
                        return true;
                }
            }
            return false;
        }
    }
}

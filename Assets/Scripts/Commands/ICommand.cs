namespace GameSys.Commands
{
    public interface ICommand
    {
        void Execute(System.Action<bool> commandComplete = null);
        void Execute(System.Action<bool> commandComplete = null, params string[] args);
    }
}

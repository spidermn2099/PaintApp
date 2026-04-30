namespace PaintApp.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();  // Добавьте этот метод
    }
}
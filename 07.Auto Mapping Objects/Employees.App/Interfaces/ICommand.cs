namespace Employees.App.Interfaces
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}

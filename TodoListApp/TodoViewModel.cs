using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TodoListApp;

public class TodoViewModel : INotifyPropertyChanged
{
    public ObservableCollection<TodoItem> Tasks { get; } = new();

    private string _newTaskTitle;
    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set
        {
            if (_newTaskTitle != value)
            {
                _newTaskTitle = value;
                OnPropertyChanged(); 
            }
        }
    }

    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand ToggleTaskCommand { get; }

    public TodoViewModel()
    {
        AddTaskCommand = new Command(() =>
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                Tasks.Add(new TodoItem { Title = NewTaskTitle.Trim(), IsCompleted = false });
                NewTaskTitle = string.Empty; 
            }
        });

        DeleteTaskCommand = new Command<TodoItem>((task) =>
        {
            if (task != null && Tasks.Contains(task))
            {
                Tasks.Remove(task);
            }
        });

        ToggleTaskCommand = new Command<TodoItem>((task) =>
        {
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
            }
        });

        Tasks.Add(new TodoItem { Title = "Сделать зарядку", IsCompleted = true });
        Tasks.Add(new TodoItem { Title = "Купить хлеб", IsCompleted = false });
        Tasks.Add(new TodoItem { Title = "Написать отчет", IsCompleted = false });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
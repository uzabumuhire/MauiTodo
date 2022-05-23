using MauiTodo.Data;
using MauiTodo.Models;

namespace MauiTodo;

public partial class MainPage : ContentPage
{
	// To-do items to be displayed on screen
    string _todoListData = string.Empty;

    readonly Database _database;
    public MainPage()
	{
		InitializeComponent();
		_database = new Database();
        _ = Initialise();
    }

    async Task Initialise()
    {
        var todos = await _database.GetTodos();

        foreach (var todo in todos)
            _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";

        TodosLabel.Text = _todoListData;
    }

    async void Button_Clicked(object sender, EventArgs e)
    {
        var todo = new TodoItem
        {
            Due = DueDatePicker.Date,
            Title = TodoTitleEntry.Text
        };

        var inserted = await _database.AddTodo(todo);

        if (inserted != 0)
        {
            _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
            TodosLabel.Text = _todoListData;
            TodoTitleEntry.Text = string.Empty;
            DueDatePicker.Date = DateTime.Now;
        }
    }
}


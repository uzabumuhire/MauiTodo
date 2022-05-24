using System.Collections.ObjectModel;

using MauiTodo.Data;
using MauiTodo.Models;

namespace MauiTodo;

public partial class MainPage : ContentPage
{
    public ObservableCollection<TodoItem> Todos { get; set; } = new();

    readonly Database _database;

    public MainPage()
	{
		InitializeComponent();
        // The ObservableCollection is set as the binding source
        // for a CollectionView.
        TodosCollection.ItemsSource = Todos;

        _database = new Database();
        _ = Initialise();
        
    }

    async Task Initialise()
    {
        var todos = await _database.GetTodos();

        // Add to-do items to the ObservableCollection
        // when the page initializes. This will automatically
        // update the CollectionView in the UI.
        foreach (var todo in todos)
            Todos.Add(todo);
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
            // Add to-do item to the ObservableCollection
            // when a user adds a new to-do item. This will
            // automatically update the CollectionView in the UI.
            Todos.Add(todo);

            // Reset the form
            TodoTitleEntry.Text = string.Empty;
            DueDatePicker.Date = DateTime.Now;
        }
    }
}


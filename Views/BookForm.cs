using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BookLibrary.Models;
using BookLibrary.Presenters;
using BookLibrary.Repositories;

namespace BookLibrary.Views;

public partial class BookForm : Form, IBookView
{
    private readonly BookPresenter _presenter;

    // Элементы управления
    private DataGridView booksGridView;
    private TextBox titleTextBox;
    private TextBox authorTextBox;
    private TextBox yearTextBox;
    private TextBox genreTextBox;
    private TextBox searchTextBox;
    private Button addButton;
    private Button updateButton;
    private Button deleteButton;
    private Button searchButton;
    private Button sortTitleButton;
    private Button sortYearButton;

    public BookForm()
    {
        InitializeComponent();
        InitializeControls();
        var repository = new JsonBookRepository("books.json");
        _presenter = new BookPresenter(this, repository);
        _presenter.LoadBooks();
    }

    private void InitializeControls()
    {
        // Настройка формы
        this.Text = "Библиотека";
        this.Width = 800;
        this.Height = 600;

        // DataGridView для отображения книг
        booksGridView = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 300,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };
        booksGridView.Columns.Add("Id", "ID");
        booksGridView.Columns.Add("Название", "Название");
        booksGridView.Columns.Add("Автор", "Автор");
        booksGridView.Columns.Add("Год", "Год");
        booksGridView.Columns.Add("Жанр", "Жанр");

        this.Controls.Add(booksGridView);

        // Группа для редактирования
        var editGroup = new GroupBox
        {
            Text = "Подробности",
            Dock = DockStyle.Top,
            Height = 150
        };
        this.Controls.Add(editGroup);

        // Поля ввода
        titleTextBox = new TextBox { Location = new System.Drawing.Point(110, 20), Width = 180 };
        authorTextBox = new TextBox { Location = new System.Drawing.Point(110, 50), Width = 180 };
        yearTextBox = new TextBox { Location = new System.Drawing.Point(110, 80), Width = 180 };
        genreTextBox = new TextBox { Location = new System.Drawing.Point(110, 110), Width = 180 };

        // Метки
        editGroup.Controls.Add(new Label { Text = "Название:", Location = new System.Drawing.Point(10, 20) });
        editGroup.Controls.Add(titleTextBox);
        editGroup.Controls.Add(new Label { Text = "Автор:", Location = new System.Drawing.Point(10, 50) });
        editGroup.Controls.Add(authorTextBox);
        editGroup.Controls.Add(new Label { Text = "Год:", Location = new System.Drawing.Point(10, 80) });
        editGroup.Controls.Add(yearTextBox);
        editGroup.Controls.Add(new Label { Text = "Жанр:", Location = new System.Drawing.Point(10, 110) });
        editGroup.Controls.Add(genreTextBox);

        // Кнопки CRUD
        addButton = new Button { Text = "Добавить", Location = new System.Drawing.Point(300, 20) };
        updateButton = new Button { Text = "Обновить", Location = new System.Drawing.Point(300, 50) };
        deleteButton = new Button { Text = "Удалить", Location = new System.Drawing.Point(300, 80) };
        addButton.Click += addButton_Click;
        updateButton.Click += updateButton_Click;
        deleteButton.Click += deleteButton_Click;
        editGroup.Controls.Add(addButton);
        editGroup.Controls.Add(updateButton);
        editGroup.Controls.Add(deleteButton);

        // Поиск и сортировка
        var searchGroup = new GroupBox
        {
            Text = "Поиск и сортировка",
            Dock = DockStyle.Top,
            Height = 80
        };
        this.Controls.Add(searchGroup);

        searchTextBox = new TextBox { Location = new System.Drawing.Point(110, 20), Width = 180 };
        searchButton = new Button { Text = "Поиск", Location = new System.Drawing.Point(300, 20) };
        sortTitleButton = new Button { Text = "Сортировка по названию", Location = new System.Drawing.Point(380, 20), Width = 160 };
        sortYearButton = new Button { Text = "Сортировка по году", Location = new System.Drawing.Point(540, 20), Width = 160 };
        searchButton.Click += searchButton_Click;
        sortTitleButton.Click += sortTitleButton_Click;
        sortYearButton.Click += sortYearButton_Click;

        searchGroup.Controls.Add(new Label { Text = "Поиск:", Location = new System.Drawing.Point(10, 20) });
        searchGroup.Controls.Add(searchTextBox);
        searchGroup.Controls.Add(searchButton);
        searchGroup.Controls.Add(sortTitleButton);
        searchGroup.Controls.Add(sortYearButton);
    }

    public void DisplayBooks(IEnumerable<Book> books)
    {
        booksGridView.Rows.Clear();
        foreach (var book in books)
        {
            booksGridView.Rows.Add(book.Id, book.Title, book.Author, book.Year, book.Genre);
        }
    }

    public void DisplayMessage(string message)
    {
        MessageBox.Show(message, "Иноформация", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public int SelectedBookId =>
        booksGridView.SelectedRows.Count > 0 ?
        (int)booksGridView.SelectedRows[0].Cells["Id"].Value :
        0;

    public Book GetBookData()
    {
        return new Book(
            0, // ID будет установлен при добавлении
            titleTextBox.Text,
            authorTextBox.Text,
            int.Parse(yearTextBox.Text),
            genreTextBox.Text
        );
    }

    private void addButton_Click(object sender, EventArgs e)
    {
        _presenter.AddBook();
        ClearInputs();
    }

    private void updateButton_Click(object sender, EventArgs e)
    {
        _presenter.UpdateBook();
        ClearInputs();
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
        _presenter.DeleteBook();
        ClearInputs();
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
        _presenter.SearchBooks(searchTextBox.Text);
    }

    private void sortTitleButton_Click(object sender, EventArgs e)
    {
        _presenter.SortBooksByTitle();
    }

    private void sortYearButton_Click(object sender, EventArgs e)
    {
        _presenter.SortBooksByYear();
    }

    private void ClearInputs()
    {
        titleTextBox.Clear();
        authorTextBox.Clear();
        yearTextBox.Clear();
        genreTextBox.Clear();
    }
}
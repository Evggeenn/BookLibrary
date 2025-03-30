using System.Collections.Generic;
using BookLibrary.Models;
using BookLibrary.Views;

namespace BookLibrary.Presenters;

public class BookPresenter
{
    private readonly IBookView _view;
    private readonly IBookRepository _repository;

    public BookPresenter(IBookView view, IBookRepository repository)
    {
        _view = view;
        _repository = repository;
    }

    public void LoadBooks()
    {
        var books = _repository.GetAllBooks();
        _view.DisplayBooks(books);
    }

    public void AddBook()
    {
        try
        {
            var book = _view.GetBookData();
            _repository.AddBook(book);
            LoadBooks();
            _view.DisplayMessage("Книга успешно добавлена!");
        }
        catch (Exception ex)
        {
            _view.DisplayMessage($"Ошибка: {ex.Message}");
        }
    }

    public void UpdateBook()
    {
        try
        {
            var book = _view.GetBookData();
            book.Id = _view.SelectedBookId;
            if (book.Id == 0)
            {
                _view.DisplayMessage("Пожалуйста, выберите книгу для обновления.");
                return;
            }
            _repository.UpdateBook(book);
            LoadBooks();
            _view.DisplayMessage("Книга успешно обновлена!");
        }
        catch (Exception ex)
        {
            _view.DisplayMessage($"Ошибка: {ex.Message}");
        }
    }

    public void DeleteBook()
    {
        try
        {
            var id = _view.SelectedBookId;
            if (id == 0)
            {
                _view.DisplayMessage("Пожалуйста, выберите книгу для удаления.");
                return;
            }
            _repository.DeleteBook(id);
            LoadBooks();
            _view.DisplayMessage("Книга успешно удалена!");
        }
        catch (Exception ex)
        {
            _view.DisplayMessage($"Ошибка: {ex.Message}");
        }
    }

    public void SearchBooks(string searchTerm)
    {
        var books = _repository.SearchBooks(searchTerm);
        _view.DisplayBooks(books);
    }

    public void SortBooksByTitle()
    {
        var books = _repository.SortBooksByTitle();
        _view.DisplayBooks(books);
    }

    public void SortBooksByYear()
    {
        var books = _repository.SortBooksByYear();
        _view.DisplayBooks(books);
    }
}
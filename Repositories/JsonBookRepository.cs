using System.Text.Json;
using BookLibrary.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookLibrary.Repositories;

public class JsonBookRepository : IBookRepository
{
    private readonly string _filePath;
    private List<Book> _books;

    public JsonBookRepository(string filePath)
    {
        _filePath = filePath;
        LoadBooks();
    }

    private void LoadBooks()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }
        else
        {
            _books = new List<Book>();
        }
    }

    private void SaveBooks()
    {
        var json = JsonSerializer.Serialize(_books);
        File.WriteAllText(_filePath, json);
    }

    public void AddBook(Book book)
    {
        book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(book);
        SaveBooks();
    }

    public void UpdateBook(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.Genre = book.Genre;
            SaveBooks();
        }
    }

    public void DeleteBook(int id)
    {
        _books.RemoveAll(b => b.Id == id);
        SaveBooks();
    }

    public Book GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public IEnumerable<Book> GetAllBooks() => _books;

    public IEnumerable<Book> SearchBooks(string searchTerm) =>
        _books.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                         b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Book> SortBooksByTitle() =>
        _books.OrderBy(b => b.Title);

    public IEnumerable<Book> SortBooksByYear() =>
        _books.OrderBy(b => b.Year);
}
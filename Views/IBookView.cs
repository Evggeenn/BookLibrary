using System.Collections.Generic;
using BookLibrary.Models;

namespace BookLibrary.Views;

public interface IBookView
{
    void DisplayBooks(IEnumerable<Book> books);
    void DisplayMessage(string message);
    int SelectedBookId { get; }
    Book GetBookData();
}
namespace BookLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }

    public Book(int id, string title, string author, int year, string genre)
    {
        Id = id;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Year = year;
        Genre = genre ?? throw new ArgumentNullException(nameof(genre));
    }
}
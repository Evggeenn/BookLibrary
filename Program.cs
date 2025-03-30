using BookLibrary.Views;
using System;
using System.Windows.Forms;

namespace BookLibrary;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new BookForm());
    }
}
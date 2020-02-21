using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookLister.Windows;

namespace BookLister
{
    // The main UI and interaction logic for BookLister
    // List of books are stored in the List<BookData> currentBooks
    // Individual BookData are stored in a text file named BookData that is modified with the BookFileManagement static class
    // BookData are loaded into two ListBox, one named FinishedBooks and the other named UnfinishedBooks
    // The index in currentBooks that a BookData in either list refers to can be found by converting the Tag property of the selected ListBoxItem and converting it to an int
    public partial class MainWindow : Window
    {
        // holding data on books
        List<BookData> currentBooks;
        List<String> unfinishedBooksSelectedGenres = new List<String>(); // list of currently selected genres in each list
        List<String> finishedBooksSelectedGenres = new List<String>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeBookGenreGrids();
            LoadBooks();
        }

        // Calls BookFileManagement methods to read BookData from file
        // adds individual BookData into appropriate list of MainWindow
        private void LoadBooks()
        {
            finishedBooks.Items.Clear(); // cleans up any items that may still be in the ListBox
            unfinishedBooks.Items.Clear();

            ListBoxItem tempItem;
            currentBooks = BookFileManagement.ReadBooksFromFile();
            for (int i = 0; i < currentBooks.Count(); i++) // creates a new ListBoxItem for each BookData, adds into correct list
            {
                tempItem = new ListBoxItem
                {
                    Content = currentBooks[i].GetTitle(),
                    Tag = i // tag that holds index of item in BookData for later use
                };
                
                if (currentBooks[i].GetIsRead() && (finishedBooksSelectedGenres.Contains(currentBooks[i].GetBookGenre())))
                {
                    finishedBooks.Items.Add(tempItem);
                }
                else if (!(currentBooks[i].GetIsRead()) && unfinishedBooksSelectedGenres.Contains(currentBooks[i].GetBookGenre()))
                {
                    unfinishedBooks.Items.Add(tempItem);
                }
            }
        }

        // when user selects a new item in either ListBox, find the BookData that belongs to that item in currentBooks
        // then replace the string in CurrentBookSelected with the information from that BookData
        // Deselects the value in the opposite ListBox
        private void ListBoxSelectionChanged(object sender, EventArgs e)
        {
            CurrentBookSelected.Clear();

            if ((sender as ListBox).Name.Equals(finishedBooks.Name))
            {
                unfinishedBooks.UnselectAll();
            }
            else if ((sender as ListBox).Name.Equals(unfinishedBooks.Name))
            {
                finishedBooks.UnselectAll();
            }

            ListBoxItem selectedListBoxItem = (sender as ListBox).SelectedItem as ListBoxItem;

            if ((sender as ListBox).SelectedItem != null)
            {
                BookData dataToRetrieve = currentBooks[Convert.ToInt32(selectedListBoxItem.Tag)];
                string textToAdd;
                textToAdd = "Title: " + dataToRetrieve.GetTitle() + "\n";
                textToAdd = textToAdd + "Date Published: " + dataToRetrieve.GetDatePublished() + "\n";
                textToAdd = textToAdd + "Author: " + dataToRetrieve.GetAuthor() + "\n";
                textToAdd = textToAdd + "Completed: " + dataToRetrieve.GetIsRead() + "\n";
                textToAdd = textToAdd + "Genre: " + dataToRetrieve.GetBookGenre() + "\n";
                textToAdd = textToAdd + "Description: \n" + dataToRetrieve.GetDescriptionUnfiltered() + "\n";

                CurrentBookSelected.AppendText(textToAdd);
            }
        }

        // fired when the AddNewBook button is clicked
        // creates a new BookData object, hands it to a new AddNewBookMenu window
        // then adds it to the currentBooks field, calls writesBooksToFile to save the new book
        // then calls LoadBooks to load all the books into the appropriate ListBox
        private void AddNewBook(object sender, EventArgs e)
        {
            BookData tempHolder;
            AddNewBookMenu addingMenu = new AddNewBookMenu(); // modifies tempHolder based on what user enters in window
            addingMenu.ShowDialog();
            tempHolder = addingMenu.NewEntry();

            if (tempHolder != null && tempHolder.IsEmpty() == false)
            {
                currentBooks.Add(tempHolder);
                BookFileManagement.WriteBooksToFile(currentBooks);
                LoadBooks();
            }
        }

        // Opens the menu for adding and removing genres
        // Once the window is closed, GenreGrids are recalculated
        private void GenresMenu(object sender, EventArgs e)
        {
            GenreManagement newMenu = new GenreManagement();
            newMenu.ShowDialog();
            CleanUpMenuItems();
            InitializeBookGenreGrids();
        }

        // Removes all the children of the UI items with variable amounts of children
        // Called before initializing them again with updated information
        private void CleanUpMenuItems()
        {
            unfinishedBooksSelectedGenres = new List<String>();
            finishedBooksSelectedGenres = new List<String>();
            unfinishedBooks.Items.Clear();
            finishedBooks.Items.Clear();
        }

        // fired when ModifyBook button is clicked
        // retrieves the BookData from currentBooks that is associated with the selected ListBoxItem
        // hands the BookData object to a new ModifyBookMenu window, where it is either altered by the user
        // or kept as is. then the data is written to file using the writeBooksToFile method, then reloaded
        // using LoadBooks
        private void ModifyBook(object sender, EventArgs e)
        {
            if (unfinishedBooks.SelectedItem != null)
            {
                int currentDataIndex = Convert.ToInt32((unfinishedBooks.SelectedItem as ListBoxItem).Tag);
                ModifyBookMenu modifyMenu = new ModifyBookMenu(currentBooks[currentDataIndex]);
                modifyMenu.ShowDialog();
                currentBooks[currentDataIndex] = modifyMenu.ModifiedEntry();
                BookFileManagement.WriteBooksToFile(currentBooks);
                LoadBooks();
            }
            else if (finishedBooks.SelectedItem != null)
            {
                int currentDataIndex = Convert.ToInt32((finishedBooks.SelectedItem as ListBoxItem).Tag);
                ModifyBookMenu modifyMenu = new ModifyBookMenu(currentBooks[currentDataIndex]);
                modifyMenu.ShowDialog();
                currentBooks[currentDataIndex] = modifyMenu.ModifiedEntry();
                BookFileManagement.WriteBooksToFile(currentBooks);
                LoadBooks();
            }
        }

        private void DeleteEntry(object sender, EventArgs e)
        {
            int selectedCurrentBooksIndex;

            if (unfinishedBooks.SelectedItem != null)
            {
                selectedCurrentBooksIndex = Convert.ToInt32((unfinishedBooks.SelectedItem as ListBoxItem).Tag);
                currentBooks.RemoveAt(selectedCurrentBooksIndex);
            }
            else if (finishedBooks.SelectedItem != null)
            {
                selectedCurrentBooksIndex = Convert.ToInt32((finishedBooks.SelectedItem as ListBoxItem).Tag);
                currentBooks.RemoveAt(selectedCurrentBooksIndex);

            }
            BookFileManagement.WriteBooksToFile(currentBooks);
            LoadBooks();
        }

        // adds or removes sorted genres in each ListBox
        private void GenreSelectButton(object sender, EventArgs e)
        {
            string[] currentTag = (string[])(sender as Button).Tag;
            string genreInformation = currentTag[1];

            if (currentTag[0].Equals("unfinished"))
            {
                if (unfinishedBooksSelectedGenres.Contains(genreInformation))
                {
                    unfinishedBooksSelectedGenres.Remove(genreInformation); // removes from search
                    (sender as Button).Background = Brushes.DarkRed; // for deselected
                }
                else
                {
                    unfinishedBooksSelectedGenres.Add(genreInformation); // adds back to search
                    (sender as Button).Background = Brushes.LightBlue; // for selected
                }
            }
            else if (currentTag[0].Equals("finished"))
            {
                if (finishedBooksSelectedGenres.Contains(genreInformation))
                {
                    finishedBooksSelectedGenres.Remove(genreInformation);
                    (sender as Button).Background = Brushes.DarkRed; 
                }
                else
                {
                    finishedBooksSelectedGenres.Add(genreInformation);
                    (sender as Button).Background = Brushes.LightBlue;
                }

            }

            LoadBooks();
        }

        // Initializes the grid of buttons above each list of books that show the genre buttons
        // for selecting and deselecting genres to look for
        private void InitializeBookGenreGrids()
        {
            // Loads genres, setting them to be selected by default
            foreach (String genre in GenreFileManagement.ReadGenresFromFile())
            {
                unfinishedBooksSelectedGenres.Add(genre);
                finishedBooksSelectedGenres.Add(genre);
            }
            int i = 0; // for finding current column index
            foreach (String genres in GenreFileManagement.ReadGenresFromFile())
            {
                FinishedBooksGrid.ColumnDefinitions.Add(new ColumnDefinition()); // adds a new column for each button to add in each list
                UnfinishedBooksGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Button buttonToAddUnfinished = new Button // defense a new button for the unfinished and finished grids
                {
                    Content = genres.ToString(),
                    Tag = new string[] { "unfinished", genres.ToString() },
                    FontSize = 10
                };
                Button buttonToAddFinished = new Button
                {
                    Content = genres.ToString(),
                    Tag = new string[] { "finished", genres.ToString() },
                    FontSize = 10
                };
                buttonToAddUnfinished.Background = Brushes.LightBlue; // default genre selected button is made to be LightBlue
                buttonToAddFinished.Background = Brushes.LightBlue;
                buttonToAddUnfinished.Click += GenreSelectButton;
                buttonToAddFinished.Click += GenreSelectButton;
                UnfinishedBooksGrid.Children.Add(buttonToAddUnfinished); // adds to UnfinishedBookGrid, sets its location
                Grid.SetRow(buttonToAddUnfinished, 0);
                Grid.SetColumn(buttonToAddUnfinished, i);
                FinishedBooksGrid.Children.Add(buttonToAddFinished); // adds to FinishedBooksGrid, sets its location
                Grid.SetRow(buttonToAddFinished, 0);
                Grid.SetColumn(buttonToAddFinished, i);
                i++;
            }
        }

    }
}

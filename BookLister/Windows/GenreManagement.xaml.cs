using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookLister.Windows
{
    /// <summary>
    /// Interaction logic for GenreManagement.xaml
    /// </summary>
    public partial class GenreManagement : Window
    {
        private List<String> currentGenres = new List<String>(); // for holding user additions and removals of genres
        public GenreManagement()
        {
            InitializeComponent();
            InitializeGenreListIntoListBox();
        }

        // Reads the list of genres on file from Genres.txt, or loads a default listing of genres
        // Populates the GenreList ListBox with an entry for each genre, defaults the default index to the first item
        // Populates the currentGenres List<String> with the read genres
        private void InitializeGenreListIntoListBox()
        {
            List<String> readGenres = GenreFileManagement.ReadGenresFromFile(); // get list of genres
            foreach (String genre in readGenres)
            {
                ListBoxItem newListBoxItemToAdd = new ListBoxItem()
                {
                    Content = genre,
                    Tag = genre
                };
                GenreList.Items.Add(newListBoxItemToAdd);
                currentGenres.Add(genre);
            }

            GenreList.SelectedIndex = 0; // SelectedIndex set to first item (should be NONE) by default
        }

        private void AddGenre(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(GenreInputBox.Text))
            {
                string currentGenre = GenreInputBox.Text;
                currentGenres.Add(currentGenre);
                ListBoxItem newListBoxItemToAdd = new ListBoxItem()
                {
                    Content = currentGenre,
                    Tag = currentGenre
                };
                GenreList.Items.Add(newListBoxItemToAdd);
            }
        }

        private void RemoveGenre(object sender, EventArgs e)
        {
            try
            {
                currentGenres.Remove((string)(GenreList.SelectedItem as ListBoxItem).Tag);
                GenreList.Items.Remove(GenreList.SelectedItem);
            }
            catch(ArgumentNullException) // catches exist so the program doesn't alter anything if null references given
            {
                Console.WriteLine("Null argument given in GenreManagement.RemoveGenre()");
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Null object reference given in GenreManagement.RemoveGenre()");
            }
            
        }

        private void SubmitChanges(object sender, EventArgs e)
        {
            GenreFileManagement.WriteGenresToFile(currentGenres);
            Close();
        }
    }
}

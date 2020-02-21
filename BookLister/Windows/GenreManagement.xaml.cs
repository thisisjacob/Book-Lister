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
        public GenreManagement()
        {
            InitializeComponent();
            InitializeGenreListIntoListBox();
        }

        // Reads the list of genres on file from Genres.txt, or loads a default listing of genres
        // Populates the GenreList ListBox with an entry for each genre, defaults the default index to the first item
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
            }

            GenreList.SelectedIndex = 0; // SelectedIndex set to first item (should be NONE) by default
        }

        private void AddGenre(object sender, EventArgs e)
        {

        }

        private void RemoveGenre(object sender, EventArgs e)
        {

        }

        private void SubmitChanges(object sender, EventArgs e)
        {

        }
    }
}

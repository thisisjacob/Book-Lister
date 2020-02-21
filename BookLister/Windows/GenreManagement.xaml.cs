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

        private void InitializeGenreListIntoListBox()
        {
            foreach (BookData.Genre genres in Enum.GetValues(typeof(BookData.Genre)))
            {
                ListBoxItem newListBoxItemToAdd = new ListBoxItem()
                {
                    Content = genres.ToString(),
                    Tag = genres
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
    }
}

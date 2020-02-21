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
    /// Interaction logic for AddNewBookMenu.xaml
    /// </summary>
    public partial class AddNewBookMenu : Window
    {
        BookData dataBeingModified;
        public AddNewBookMenu()
        {
            InitializeComponent();
            InitializeGenreListIntoListBox();
        }

        public void CreateData(object sender, EventArgs e)
        {
            BookData tempObject;
            if (IsReadBox.IsChecked == true)
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, true, (string)(GenreList.SelectedItem as ListBoxItem).Tag, DescBox.Text);
            }
            else
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, false, (string)(GenreList.SelectedItem as ListBoxItem).Tag, DescBox.Text); 
            }

            if (!tempObject.IsEmpty())
            {
                dataBeingModified = new BookData(tempObject.GetTitle(), tempObject.GetAuthor(), tempObject.GetDatePublished(), tempObject.GetIsRead(), tempObject.GetBookGenre(), tempObject.GetDescriptionUnfiltered());
            }

            this.Close();
        }

        public BookData NewEntry()
        {
            return dataBeingModified;
        }

        private void InitializeGenreListIntoListBox()
        {
            foreach (String genres in GenreFileManagement.ReadGenresFromFile()) 
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
    }
}

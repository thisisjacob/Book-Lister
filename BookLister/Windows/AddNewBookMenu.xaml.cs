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
        readonly BookData dataBeingModified;
        public AddNewBookMenu(BookData dataToCreate)
        {
            dataBeingModified = dataToCreate;
            InitializeComponent();
            InitializeGenreList();
        }

        public void CreateData(object sender, EventArgs e)
        {
            BookData tempObject;
            if (IsReadBox.IsChecked == true)
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, true, (BookData.Genre)GenreList.SelectedIndex, DescBox.Text);
            }
            else
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, false, (BookData.Genre)GenreList.SelectedIndex, DescBox.Text); 
            }

            if (!tempObject.IsEmpty())
            {
                dataBeingModified.SetAuthor(tempObject.GetAuthor());
                dataBeingModified.SetTitle(tempObject.GetTitle());
                dataBeingModified.SetDatePublished(tempObject.GetDatePublished());
                dataBeingModified.SetIsRead(tempObject.GetIsRead());
                dataBeingModified.SetGenre(tempObject.GetBookGenre());
                dataBeingModified.SetDescription(tempObject.GetDescription());
            }

            this.Close();
        }

        private void InitializeGenreList()
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
    }
}

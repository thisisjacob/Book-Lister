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

namespace BookLister
{
    /// <summary>
    /// Interaction logic for ModifyBookMenu.xaml
    /// </summary>
    public partial class ModifyBookMenu : Window
    {
        readonly BookData modifiedEntry;
        public ModifyBookMenu(BookData entryToModify)
        {
            modifiedEntry = entryToModify; // reference to passed BookData that is to be modified
            InitializeComponent();
            InitializeGenreList();
            TitleBox.Text = modifiedEntry.GetTitle(); // initializes with preexisting information
            DateBox.Text = modifiedEntry.GetDatePublished();
            AuthorBox.Text = modifiedEntry.GetAuthor();
            IsReadBox.IsChecked = modifiedEntry.GetIsRead();
        }

        private void ModifyData(object sender, EventArgs e)
        {
            BookData tempObject;
            if (IsReadBox.IsChecked == true)
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, true, (BookData.Genre)GenreList.SelectedIndex); // TODO: replace placeholder genre
            }
            else
            {
                tempObject = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, false, (BookData.Genre)GenreList.SelectedIndex); // TODO: replace placeholder genre
            }
            
            if (!tempObject.IsEmpty())
            {
                modifiedEntry.SetAuthor(tempObject.GetAuthor());
                modifiedEntry.SetTitle(tempObject.GetTitle());
                modifiedEntry.SetDatePublished(tempObject.GetDatePublished());
                modifiedEntry.SetIsRead(tempObject.GetIsRead());
                modifiedEntry.SetGenre(tempObject.GetBookGenre());
            }

            this.Close();

        }

        // loads GenreList with ListBoxItems that represent the list of genres available in BookData
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

            GenreList.SelectedItem = GenreList.Items.GetItemAt(Convert.ToInt32(modifiedEntry.GetBookGenre()));
        }
    }
}

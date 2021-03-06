﻿using System;
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
    /// Interaction logic for ModifyBookMenu.xaml
    /// </summary>
    public partial class ModifyBookMenu : Window
    {
        readonly BookData originalEntry;
        BookData newEntry;
        public ModifyBookMenu(BookData entryToModify)
        {
            InitializeComponent();
            InitializeGenreList(entryToModify);
            TitleBox.Text = entryToModify.GetTitle(); // initializes with preexisting information
            DateBox.Text = entryToModify.GetDatePublished();
            AuthorBox.Text = entryToModify.GetAuthor();
            IsReadBox.IsChecked = entryToModify.GetIsRead();
            DescBox.Text = entryToModify.GetDescriptionUnfiltered();
            originalEntry = entryToModify;
        }

        private void ModifyData(object sender, EventArgs e)
        {
            if (IsReadBox.IsChecked == true)
            {
                newEntry = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, true, (string)(GenreList.SelectedItem as ListBoxItem).Tag, DescBox.Text); 
            }
            else
            {
                newEntry = new BookData(TitleBox.Text, AuthorBox.Text, DateBox.Text, false, (string)(GenreList.SelectedItem as ListBoxItem).Tag, DescBox.Text); 
            }
            this.Close();
        }

        public BookData ModifiedEntry()
        {
            if (newEntry == null || newEntry.IsEmpty()) // if newEntry incomplete, or window is closed, then the original file is returned
            {
                return originalEntry;
            }
            else // otherwise, modified entry is returned
            {
                return newEntry;
            }
        }

        // loads GenreList with ListBoxItems that represent the list of genres available in BookData
        private void InitializeGenreList(BookData entryToReadFrom)
        {
            int count = 0;
            int indexOfEntryToReadFrom = 0;
            foreach (String genres in GenreFileManagement.ReadGenresFromFile())
            {
                ListBoxItem newListBoxItemToAdd = new ListBoxItem()
                {
                    Content = genres.ToString(),
                    Tag = genres
                };
                GenreList.Items.Add(newListBoxItemToAdd);
                if (genres.Equals(entryToReadFrom.GetBookGenre())) // saves the index of the preselected entry
                {
                    indexOfEntryToReadFrom = count;
                }
                count++;
            }

            GenreList.SelectedIndex = indexOfEntryToReadFrom;
        }
    }
}

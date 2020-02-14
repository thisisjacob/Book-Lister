using System;
using System.Collections.Generic;
using System.Text;

namespace BookLister
{
    class GenreFileManagement
    {
    }

    class GenreData
    {
        List<string> GenreList = new List<string>();

        GenreData(List<string> givenGenres)
        {
            foreach (string element in givenGenres)
            {
                GenreList.Add(element);
            }
        }

        public List<string> GetGenreList()
        {
            List<string> GenreListCopy = new List<string>();
            foreach (string element in GenreList)
            {
                string copy = element;
                GenreListCopy.Add(copy);
            }
            return GenreListCopy;
        }
    }
}

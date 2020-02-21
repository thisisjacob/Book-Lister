using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BookLister
{
    class GenreFileManagement
    {
        const string GENRE_FILE_PATH = "Genres.txt";

        // Accepts a List<String> representing genres
        // Writes each genre onto a new line in the GenreFilepath constant
        public static void WriteGenresToFile(List<String> listOfGenres)
        {
            try
            {
                File.WriteAllLines(GENRE_FILE_PATH, listOfGenres);
            }
            catch (IOException)
            {
                Console.WriteLine("Error writing genres to file in GenreFileManagement.WriteGenresToFile() Path: " + GENRE_FILE_PATH);
            }
        }

        // Reads each line of the GenreFilepath constant file
        // Returns each line as a separate entry in a List<String> representing the genres
        public static List<String> ReadGenresFromFile()
        {
            List<String> genres = new List<String>();

            if (!File.Exists(GENRE_FILE_PATH))
            {
                File.WriteAllText(GENRE_FILE_PATH, "");
            }

            try
            {
                string[] lines = File.ReadAllLines(GENRE_FILE_PATH);
                foreach (String element in lines)
                {
                    genres.Add(element);
                }
            }
            catch(IOException)
            {
                Console.WriteLine("IOError in GenreFileManagement.ReadGenresFromFile() Path: " + GENRE_FILE_PATH);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Index Range Error in GenreFileManagement.ReadGenresFromFile()");
            }


            return genres;
        }
    }

    class GenreData
    {
        private readonly List<string> GenreList = new List<string>();

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

    class GenrePublicMethods
    {
    }

}

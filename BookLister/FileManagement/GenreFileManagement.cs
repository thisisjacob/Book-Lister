using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BookLister
{
    class GenreFileManagement
    {
        private const string GENRE_FILE_PATH = "Genres.txt";
        private const string NO_GENRE_DEFAULT = "None";
        private readonly static List<String> DefaultGenres = new List<String> { "Fiction", "Nonfiction", "Fantasy", "Scifi", "Romance", "Horror" }; // read if no Genre.txt file exists

        // Accepts a List<String> representing genres
        // Writes each genre onto a new line in the GenreFilepath constant
        public static void WriteGenresToFile(List<String> listOfGenres)
        {
            List<String> genresToWrite = new List<String>(); // valid genres, to be written to file
            // TODO: STOP WRITING NO_GENRE_DEFAULT
            foreach (String element in listOfGenres)
            {
                if (!((element.Equals(NO_GENRE_DEFAULT)) || (String.IsNullOrEmpty(element)))) // if not NO_GENRE_DEFAULT, and not null/empty, then the element string will be written
                {
                    genresToWrite.Add(element);
                }
            }

            try
            {
                File.WriteAllLines(GENRE_FILE_PATH, genresToWrite);
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
            genres.Add(NO_GENRE_DEFAULT); // default first value, even if nothing in file

            // If file does not exist, return a copy of DefaultGenres
            if (!File.Exists(GENRE_FILE_PATH))
            {
                File.WriteAllText(GENRE_FILE_PATH, "");
                foreach (String element in DefaultGenres)
                {
                    genres.Add(element);
                }
                return genres;
            }

            // Adds the genres from file into genres
            try
            {
                string[] lines = File.ReadAllLines(GENRE_FILE_PATH);
                foreach (String element in lines)
                {
                    if (!element.Equals(NO_GENRE_DEFAULT)) // prevents double reading NO_GENRE_DEFAULT if it was in file (should not happen in normal operation)
                    {
                        genres.Add(element);
                    }
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


            return genres; // Returns the genres from file
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

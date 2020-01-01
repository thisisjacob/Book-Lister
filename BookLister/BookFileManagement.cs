using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BookLister
{
    // For reading/writing BookData Lists to/from file
    static class BookFileManagement
    {
        static readonly string filePath = "BookData.txt"; // location of storage

        // writes a List of BookData to filePath in a format that can be used to recreate book objects
        // Each field returned by the returnInformation() method of BookData is printed on a line, with each field separated by a space
        // Each BookData gets its own line
        public static void WriteBooksToFile(List<BookData> listOfBooks)
        {
            List<string> bookFields;
            List<string> allLinesToWrite = new List<string>();


            for (int i = 0; i < listOfBooks.Count; i++)
            {
                bookFields = listOfBooks[i].ReturnInformation();
                for (int j = 0; j < bookFields.Count; j++)
                {
                    allLinesToWrite.Add(bookFields[j]);
                }

            }

            try
            {
                File.WriteAllLines(filePath, allLinesToWrite);
            }
            catch (IOException)
            {
                Console.WriteLine("Error found while writing files in BookFileManagement.WriteBooksToFile() File: " + filePath);
            }

            
        }

        // reads books from filePath as a List of BookData, returns a List of BookData
        public static List<BookData> ReadBooksFromFile()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            
            string[] rawFileData = File.ReadAllLines(filePath);
            string[] lineInformation;
            List<BookData> readInformation = new List<BookData>();
            BookData tempHolder;

            for (int i = 0; i < (rawFileData.Length / 6); i++)
            {
                lineInformation = new string[]{ rawFileData[0 + (i * 6)], rawFileData[1 + (i * 6)], rawFileData[2 + (i * 6)], rawFileData[3 + (i * 6)], rawFileData[4 + (i * 6)], rawFileData[5 + (i * 6)] };
                tempHolder = new BookData(lineInformation[0], lineInformation[1], lineInformation[2], Convert.ToBoolean(lineInformation[3]), Enum.Parse<BookData.Genre>(lineInformation[4], true), lineInformation[5]);
                if (tempHolder != null && !tempHolder.IsEmpty()) // if null or empty, do not add to readInformation
                {
                    readInformation.Add(tempHolder);
                }
            }



            return readInformation;
        }
        
    }

    // A class for holding all the information needed about a book
    // title, author, date published and whether it was read or not
    public class BookData
    {
        public enum Genre // enum for genre type, public so that enum values can be entered into BookData definition
        { 
            NONE,
            Fiction,
            Nonfiction,
            Fantasy,
            Scifi,
            Romance,
            Horror
        };

        // data on individual book
        string title;
        string author;
        string datePublished;
        bool isRead;
        Genre bookGenre;
        string description;

        public BookData(string givenTitle, string givenAuthor, string givenDate, bool isCompleted, Genre givenGenre, string givenDescription)
        {
            title = givenTitle;
            author = givenAuthor;
            datePublished = givenDate;
            isRead = isCompleted;
            bookGenre = givenGenre;
            description = givenDescription;
        }

        public BookData()
        {
            title = "N/A";
            author = "N/A";
            datePublished = "N/A";
            isRead = false;
            bookGenre = 0;
            description = "N/A";
        }

        public List<string> ReturnInformation()
        {
            List<string> returnedInformation = new List<string>
            {
                title,
                author,
                datePublished,
                isRead.ToString(),
                bookGenre.ToString(),
                description
            };

            return returnedInformation;
        }

        // Returns true if the BookData has an empty or whitespace title, or one that consists of "N/A". returns false otherwise
        public bool IsEmpty()
        {
            if ((title.Equals("N/A") || String.IsNullOrWhiteSpace(title)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetTitle(string givenTitle)
        {
            title = givenTitle;
        }

        public void SetAuthor(string givenAuthor)
        {
            author = givenAuthor;
        }

        public void SetDatePublished(string givenDatePublished)
        {
            datePublished = givenDatePublished;
        }

        public void SetIsRead(bool givenStatus)
        {
            isRead = givenStatus;
        }

        public void SetGenre(Genre givenNewGenre)
        {
            bookGenre = givenNewGenre;
        }

        public void SetDescription(string newDescription)
        {
            description = newDescription;
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetAuthor()
        {
            return author;
        }

        public string GetDatePublished()
        {
            return datePublished;
        }
        
        public bool GetIsRead()
        {
            return isRead;
        }

        public Genre GetBookGenre()
        {
            return bookGenre;
        }

        public string GetDescription()
        {
            return description;
        }

    }
}

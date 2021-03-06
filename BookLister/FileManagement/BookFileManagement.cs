﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BookLister
{
    // For reading/writing BookData Lists to/from file
    static class BookFileManagement
    {
        const string filePath = "BookData.txt"; // location of storage

        // writes a List of BookData to filePath in a format that can be used to recreate book objects
        // Each field returned by the returnInformation() method of BookData is printed on a line, with each field separated by a space
        // Each BookData gets its own line
        public static void WriteBooksToFile(List<BookData> listOfBooks)
        {
            List<string> bookFields;
            List<string> allLinesToWrite = new List<string>();


            // Writes each BookData in listOfBooks to storage in turn
            for (int i = 0; i < listOfBooks.Count; i++)
            {
                if (listOfBooks[i] != null && !listOfBooks[i].IsEmpty()) // prevents invalid BookData from being read and causing null reference exceptions
                {

                    bookFields = listOfBooks[i].ReturnInformation();
                    for (int j = 0; j < bookFields.Count; j++)
                    {
                        allLinesToWrite.Add(bookFields[j]);
                    }
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
                try
                {
                    lineInformation = new string[] { rawFileData[0 + (i * 6)], rawFileData[1 + (i * 6)], rawFileData[2 + (i * 6)], rawFileData[3 + (i * 6)], rawFileData[4 + (i * 6)], rawFileData[5 + (i * 6)] };
                    tempHolder = new BookData(lineInformation[0], lineInformation[1], lineInformation[2], Convert.ToBoolean(lineInformation[3]), lineInformation[4], lineInformation[5]);
                    if (tempHolder != null && !tempHolder.IsEmpty()) // if null or empty, do not add to readInformation
                    {
                        readInformation.Add(tempHolder);
                    }
                }
                catch (IndexOutOfRangeException) // if end of line reached before all data of current BookData can be read, end loop prematurely
                {
                    break;
                }
                catch(IOException)
                {
                    Console.WriteLine("Error found while reading files in BookFileManage.ReadBooksFromFile() File: " + filePath);
                }

            }

            return readInformation;
        }
        
    }

    // A class for holding all the information needed about a book
    // title, author, date published and whether it was read or not
    public class BookData
    {
        private const string NEW_LINE_REPLACEMENT = "​​"; // for filtering and unfiltering new line from text when moved into and out of storage

        // data on individual book
        private string title;
        private string author;
        private string datePublished;
        private bool isRead;
        private string bookGenre;
        private string description;

        public BookData(string givenTitle, string givenAuthor, string givenDate, bool isCompleted, string givenGenre, string givenDescription)
        {
            title = givenTitle;
            author = givenAuthor;
            datePublished = givenDate;
            isRead = isCompleted;
            bookGenre = givenGenre;
            description = givenDescription.Replace(Environment.NewLine, NEW_LINE_REPLACEMENT); // Replacing new line with replacement to store
        }

        public BookData()
        {
            title = "N/A";
            author = "N/A";
            datePublished = "N/A";
            isRead = false;
            bookGenre = "";
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


        // Returns the title
        public string GetTitle()
        {
            return title;
        }

        // Returns the author
        public string GetAuthor()
        {
            return author;
        }

        // Returns a string representing the date published
        public string GetDatePublished()
        {
            return datePublished;
        }
        
        // Returns true if the book has been read, false otherwise
        public bool GetIsRead()
        {
            return isRead;
        }

        // Returns the Genre enum of the book's Genre
        public string GetBookGenre()
        {
            return bookGenre;
        }

        // Returns the description, with new lines filtered out
        public string GetDescription()
        {
            return description;
        }

        // For returning the description with the new lines included
        public string GetDescriptionUnfiltered()
        {
            return description.Replace(NEW_LINE_REPLACEMENT, Environment.NewLine);
        }

    }
}

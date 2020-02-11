# Book-Lister
A program for managing a collection of books. Built using C#, .NET Core. WPF was chosen as the UI framework for this program.

The user can create books (along with its name, author, date and description.) Primarily an exercise in saving and reading collections of data, so specific date and naming formats are not enforced. 

Future plans are to implement an SQLite local database for storing information, as well as performing general improvements in the interface.

![alt tag](https://i.imgur.com/i3p4mId.png)




How To Compile:
1. Open BookLister.sln in Visual Studio, build solution
Or
2. Using Developer Command Prompt for Visual Studio, 
	a. Run the "dotnet restore" command on BookLister.csproj
	b. Run the "msbuild" command on BookLister.csproj
 
Notes On Modifying:
MainWindow is where the user selects and chooses what to do with books.
AddNewBookMenu and ModifyBookMenu are new windows, which the user chooses when to open.
BookFileManagement includes the BookData class, which is for holding information on a specific book, as well as methods for saving and loading lists of books from file.



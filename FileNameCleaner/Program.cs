using System;
using System.IO;
using System.Linq;

namespace FileNameCleaner
{
    public class Program
    {
        public static void Main()
        {
            MusTrackRenamer.Renamer();
        }
    }

    public class MusTrackRenamer
    {
        public static void Renamer()
        {
            var currentPath = AppDomain.CurrentDomain.BaseDirectory; //retrieve path of our application
            var allmp3FilesInDirectory = Directory.GetFiles(currentPath, "*.mp3").Select(Path.GetFileName).ToArray(); //all mp3 files in this folder
            var allm4aFilesInDirectory = Directory.GetFiles(currentPath, "*.m4a").Select(Path.GetFileName).ToArray();//all m4a files in this folder
            const string digitsFormp3 = "124567890";
            const string digitsForm4a = "123567890";

            foreach (var oneFile in allmp3FilesInDirectory)
            {
                var nameOfOneFile = oneFile;
                nameOfOneFile = nameOfOneFile.Replace("03", ""); //mp3 exception
                nameOfOneFile = nameOfOneFile.Replace("13", ""); //mp3 exception
                nameOfOneFile = nameOfOneFile.Replace("_", " "); //replacing underscores with space
                nameOfOneFile = digitsFormp3.Aggregate(nameOfOneFile, (current, c) => current.Replace(c.ToString(), "")); //removing of digits
                nameOfOneFile = nameOfOneFile.Replace(". ", ""); //cleaning
                File.Move(oneFile, nameOfOneFile); //renaming of file
            }

            foreach (var oneFile in allm4aFilesInDirectory)
            {
                var nameOfOneFile = oneFile;
                nameOfOneFile = nameOfOneFile.Replace("_", " "); //replacing underscores with space
                nameOfOneFile = nameOfOneFile.Replace("04", ""); //m4a exception
                nameOfOneFile = nameOfOneFile.Replace("14", ""); //m4a exception
                nameOfOneFile = digitsForm4a.Aggregate(nameOfOneFile, (current, c) => current.Replace(c.ToString(), ""));//removing digits from the name
                //foreach (char c in digitsForm4a) //this is the same as previous line but not on LINQ
                //{
                //    nameOfOneFile = nameOfOneFile.Replace(c.ToString(), "");
                //}
                nameOfOneFile = nameOfOneFile.Replace(". ", ""); //cleaning
                File.Move(oneFile, nameOfOneFile); //renaming of file
            }
        }
    }
}
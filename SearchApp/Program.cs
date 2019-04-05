using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static BL.Search;
using BL;
using System.Threading;

namespace SearchApp
{
    class Program
    {


        public static void printMenu()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to search app!");
            Console.WriteLine("1. Enter file name to search");
            Console.WriteLine("2. Enter file name to search + parent directory to search in ");
            Console.WriteLine("3. Exit");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine();

        }

        public static bool validateNotNull(string userInput)
        {

            if (string.IsNullOrEmpty(userInput)) { Console.WriteLine("empty user input!"); return true; } else { return false; }


        }

        public static void swithcUserInput(string input)
        {
            Boolean isEmptyInput;
            string defaultDirectory = "c:\\test\\";
            string userInput;
            IEnumerable<string> lines;
            DateTime nowTime = DateTime.Now;


            void insertResultsToDataBaseNameOnly(IEnumerable<string> userLines)
            {
                if (userLines.Count() != 0)
                {
                    foreach (var line in userLines)
                    {
                        addPathDB(line, userInput, defaultDirectory, nowTime, "name only");

                    }
                }
                else
                {
                    Console.WriteLine("file not found");
                    addPathDB("N/A", userInput, defaultDirectory, nowTime, "name only");
                }
            }
            void insertResultsToDataBaseNameAndPath(IEnumerable<string> userLines)
            {
                if (userLines.Count() != 0)
                {
                    foreach (var line in userLines)
                    {
                        Search.addPathDB(line, userInput, defaultDirectory, nowTime, "name + path");

                    }
                }
                else
                {
                    Console.WriteLine("file not found");
                    Search.addPathDB("N/A", userInput, defaultDirectory, nowTime, "name + path");
                }
            }
            void forEachPrintResult(IEnumerable<string> list)
            {
                foreach(var v in list) {
                    Thread.Sleep(500);
                    Console.WriteLine(v);
                }

            }

            switch (input)
            {

                case "1":

                    Console.WriteLine("Enter file name to search:");
                    userInput = Console.ReadLine();
                    //validate input is not 0 or null
                    isEmptyInput = validateNotNull(userInput);
                    if (isEmptyInput) break;

                    Console.WriteLine("searching in folder: {0} searching for term: {1}.....", defaultDirectory, userInput);
                    lines = SearchFiles(defaultDirectory, userInput);
                    forEachPrintResult(lines);
                    insertResultsToDataBaseNameOnly(lines);
                    break;

                case "2":

                    Console.WriteLine("Enter file name to search:");
                    userInput = Console.ReadLine();

                    //validate search term
                    isEmptyInput = validateNotNull(userInput);
                    if (isEmptyInput) break;


                    Console.WriteLine("Enter parent directory to search in:");
                    defaultDirectory = Console.ReadLine();


                    //validate path
                    isEmptyInput = validateNotNull(defaultDirectory);
                    if (isEmptyInput) break;


                    Console.WriteLine("searching in folder: {0} searching for term: {1}.....", defaultDirectory, userInput);

                    lines = SearchFiles(defaultDirectory, userInput);
                    forEachPrintResult(lines);
                    insertResultsToDataBaseNameAndPath(lines);
                    break;

                case "3":
                    Console.WriteLine("goodbye!");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("invalid input - please try again");
                    break;

            }




        }


        static void Main(string[] args)
        {


        
            string userInput;

            

            while(true)
            {
                printMenu();
                userInput = Console.ReadLine();


                //switch user choice -> switch case
                swithcUserInput(userInput);

                if ( userInput == "3" ) break; continue;

                

            }
            


            


            Console.ReadLine();
        }
    }
}

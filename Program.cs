using System;
using System.Collections.Generic;
using System.IO;

namespace ngram
{
    /* 
     * Project: N-gram Extractor- INFO-3142 Emerging Technology
     * Purpose: Learning NLP 
     * Date: Due Oct. 28th 2021
     * Coders: Roberto Davies-Amaral & Sonia Friesen  
     */
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader(); //custom filereader class to read and process data
            Console.WriteLine();
            Console.WriteLine("INFO-3142 Emerging Technology");
            Console.WriteLine("Project 1 - N-gram Extractor");
            Console.WriteLine("ngram.exe -> NLP By: Roberto Davies-Amaral and Sonia Friesen");
            Console.WriteLine("_______________________________________________________________");
            Console.WriteLine();

            //check command line arguments
            if (args.Length < 1)
            {
                Console.Write("Not enough arguments");
                Environment.Exit(1); //close the application
            }
            //check command line arguments
            if (args.Length > 1)
            {
                Console.Write("Too many arguments");
                Environment.Exit(1); //close the application
            }
            // determine textdata, get  NounsData.txt and NounsIndex.txt into dictionaries
            string data = args[0]; //filename

                    
            string testData = reader.readFile(data);           
            List<string> nounsIndex = reader.readNouns("NounsIndex.txt"); // file is in bin/debug/net5.0
            List<string> nounsData = reader.readNouns("NounsData.txt"); // file is in bin/debug/net5.0
            //write the data file to the console
            Console.WriteLine();
            Console.WriteLine(testData);       
            using (StreamWriter writer = new StreamWriter("debug.txt", false))
            {
                writer.WriteLine(testData);
            }

            //convert lists into dictionaries
            Dictionary<string, string> nIndexs = reader.convertToDictionary(nounsIndex);
            Dictionary<string, string> nData = reader.convertToDictionary(nounsData);
           
            //get each sentence
            string[] s = testData.Split(".");        
            foreach (string sentence in s)
            {
                if (sentence == "") 
                {
                    break;
                }
                string literal = sentence.Trim();
                //newData = testData.Replace(".", "");
                using (StreamWriter writer = new StreamWriter("debug.txt", true))
                {
                    writer.WriteLine();
                    writer.WriteLine(literal);
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(literal);
                Console.WriteLine();

                //process the n-gram levels on each sentence
                //n-gram 2,3,4 process
                /* Using a for loop here to reduce function calls and code*/
                for(int i = 2; i < 5; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{i} level n-gram");
                    using (StreamWriter writer = new StreamWriter("debug.txt", true))
                    {
                        writer.WriteLine();
                        writer.WriteLine($"{i} level n-gram");
                        writer.WriteLine();
                    }
                    Console.WriteLine();
                    string[] words = literal.Split(); //string of every sentence 
                    List<string> ngrams = reader.turnIntoNgram(i, words);
                    List<string> definitions = reader.getngram(ngrams, nIndexs, nData);

                    reader.print(definitions); //print to console
                    reader.writetofile(definitions); //write to debug.txt
                    Console.WriteLine();
                }          
            }       
            Console.WriteLine("\n");
            Console.WriteLine("Thanks for using N-gram Extractor by Roberto Davies-Amaral and Sonia Friesen");
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine();
                writer.WriteLine("Thanks for using N-gram Extractor by Roberto Davies-Amaral and Sonia Friesen");
                writer.WriteLine();
            }
            Environment.Exit(0); //close the application
        }
    }
}

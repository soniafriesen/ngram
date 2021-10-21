﻿using System;
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
            // determine textdata, get  NounsData.txt and NounsIndex.txt into dictionaries
            string data = args[0]; //filename

                    
            string testData = reader.readFile(data);
            string newData = testData.Replace(".","");
            List<string> nounsIndex = reader.readNouns("NounsIndex.txt"); // file is in bin/debug/net5.0
            List<string> nounsData = reader.readNouns("NounsData.txt"); // file is in bin/debug/net5.0
            //string debugFile = Path.Combine(Environment.CurrentDirectory, @"", "debug.txt");

            //test file reading
            //reader.test(testData, nounsData, nounsIndex);

            //write the data file to the console
            Console.WriteLine();
            Console.WriteLine(testData);
            //string[] s = testData.Split(".");
            //string sentence = s[0];
            Console.WriteLine("\n");
            Console.WriteLine(newData);
            using(StreamWriter writer = new StreamWriter("debug.txt", false))
            {
                writer.WriteLine(testData);               
            }
            using(StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine();
                writer.WriteLine(newData);                
            }

            //convert lists into dictionaries
            Dictionary<string, string> nIndexs = reader.convertToDictionary(nounsIndex);
            Dictionary<string,string> nData = reader.convertToDictionary(nounsData);

            //dictionary fill test
            //reader.testDictionary(nIndexs);
            //reader.testDictionary(nData);

            //n-gram 2,3,4 process

            //n-gram level 2 process
            Console.WriteLine();
            Console.WriteLine("2 level n-gram");
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine();
                writer.WriteLine("2 level n-gram");
                writer.WriteLine();
            }
            Console.WriteLine();
            string[] words = newData.Split(); //string of every sentence 
            List<string> ngrams = reader.turnIntoNgram(2,words);

            //searching for the indexs and definitions if the word has any
            List<string> definitions = reader.getngram(ngrams, nIndexs, nData);

            reader.print(definitions); //print to console
            reader.writetofile(definitions); //write to debug.txt
            Console.WriteLine();

            //n-gram level 3 process
        
            Console.WriteLine("3 level n-gram");
            Console.WriteLine();
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine();
                writer.WriteLine("3 level n-gram");
                writer.WriteLine();
            }
            ngrams = reader.turnIntoNgram(3, words);
            definitions = reader.getngram(ngrams, nIndexs, nData);
            reader.print(definitions);
            reader.writetofile(definitions);

            //4 level ngram
            Console.WriteLine();
            Console.WriteLine("4 level n-gram");
            Console.WriteLine();
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine();
                writer.WriteLine("4 level n-gram");
                writer.WriteLine();
            }
            ngrams = reader.turnIntoNgram(4, words);
            definitions = reader.getngram(ngrams, nIndexs, nData);
            reader.print(definitions);
            reader.writetofile(definitions);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ngram
{
    public class Reader
    {
        /*
         * Method:readFile
         * Purpose: reads the test file into a string
         * Parameters: file (string)
         * Returns: string (context of the file)
         */
        public string readFile(string filename)
        {
            string context = "";
            try
            {
                context = File.ReadAllText(filename);               
                return context;
            }catch(IOException ex)
            {
                return ex.Message;
            }
       
        }
        /*
        * Method:readNouns
        * Purpose: reads the nouns files into a list
        * Parameters: file (string)
        * Returns: List<string> (context of the file line by line)
        */
        public List<string> readNouns(string filename)
       {
           string[] nounscontext;
           try
           {
               nounscontext = File.ReadAllLines(filename);
               return nounscontext.ToList();
           }catch (IOException ex)
           {
               Console.WriteLine( ex.Message);
               return null;
           }            
       }

        /*
        * Method:readNouns
        * Purpose: reads the nouns files into a list
        * Parameters: file (string)
        * Returns: List<string> (context of the file line by line)
        */
        public Dictionary<string,string> convertToDictionary(List<string> list)
        {
            Dictionary<string, string> context = new Dictionary<string, string>();

            //extract each element in array split ('|')
            foreach (string s in list)
            {
                string beforesign = s.Split('|')[0];
                string aftersign = s.Split('|')[1];
                context.Add(beforesign, aftersign);
            }

            return context;
        }

        public List<string> getNgrams(int token, string[] words)
        {
            List<string> ngrams = new List<string>();

            switch(token)
            {
                case 2:
                    {
                        int x = 1;
                        for (int i = 0; i < words.Length - 1; i++)
                        {  
                            if (x == words.Length - 1)
                            {
                                x = words.Length - 1;
                                ngrams.Add($"{words[i]}_{words[x]}");
                            }
                            else
                            {
                                ngrams.Add($"{words[i]}_{words[x]}");
                                x++;
                            }
                            
                        }
                        return ngrams;
                    }
                default: return null;
            }
           
        }


        /*
         * Method: test()
         * Purpose: to test file reading
         * Parameters: test file, to two lists<string> containing Nouns context
         * Returns: none
         */
        public void test(string testData, List<string> nounsData, List<string> nounsIndex)
        {
            Console.WriteLine(testData);
            Console.WriteLine();
            foreach (string x in nounsData)
            {
                Console.WriteLine(x);
            }
            foreach (string x in nounsIndex)
            {
                Console.WriteLine(x);
            }
        }

        /*
        * Method: testDictionary()
        * Purpose: make sure our dictionaries are filled wiht the proper data
        * Parameters: Dictionary<string,string> (nouns dictionary)
        * Returns: none
        */
        public void testDictionary(Dictionary<string,string> dictionary)
        {
            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                Console.WriteLine($"{entry.Key} with value {entry.Value}");
            }
        }
    }
}

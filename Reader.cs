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

        /*
        * Method: getNgrams()
        * Purpose: get the token to make ngrams
        * Parameters: List<string>, Dictionary<string,string>,Dictionary<string,string>
        * Returns: List<string>
        */
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
        * Method: get2levelngram()
        * Purpose: to get 2 level ngram
        * Parameters: List<string>, Dictionary<string,string>,Dictionary<string,string>
        * Returns: List<string>
        */
        public List<string> get2levelngram(List<string> ngrams, Dictionary<string,string> nIndexs, Dictionary<string, string> nData)
        {
            List<string> definitions = new List<string>();
            foreach (string n in ngrams)
            {
                string lowered = n.ToLower();
                if (nIndexs.ContainsKey(lowered))
                {
                    string nounsdef = "";
                    string value = nIndexs[lowered];
                    //if value has more than one definition
                    string[] keys = value.Split(',');
                    if (keys.Length > 1) //mutliple keys/definitions for this noun
                    {
                        foreach (string k in keys)
                        {

                            nounsdef = $"{nounsdef};{nData[k]}";

                        }
                        nounsdef = nounsdef.Trim();
                        nounsdef = nounsdef.Remove(0, 1); //removed inital ; 
                        definitions.Add($"{n}, {nounsdef}");
                    }
                    else
                    {
                        nounsdef = nData[value];
                        definitions.Add($"{n}, {nounsdef}");
                    }
                }
                else
                    definitions.Add($"{n},");
            }
            return definitions;
        }
        public void print(List<string> definitions)
        {
            foreach (string x in definitions)
            {
                Console.WriteLine(x);
            }
        }
        public void writetofile(List<string> definitions)
        {
            foreach (string x in definitions)
            {
                using (StreamWriter writer = new StreamWriter("debug.txt", true))
                {
                    writer.WriteLine(x);
                }
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

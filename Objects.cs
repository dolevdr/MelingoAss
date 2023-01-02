using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrdList
{
    class Objects
    {
        public Dictionary<string, string> words = new Dictionary<string, string>();
        public Dictionary<string, LinkedList<string>> examples = new Dictionary<string, LinkedList<string>>();
        
        public Objects()
        {
            readWords(@"C:\Users\dolevdrori\Desktop\WordListProj\WrdList\WrdList\Resources\Entriesnodu.csv");
            readExample(@"C:\Users\dolevdrori\Desktop\WordListProj\WrdList\WrdList\Resources\Examples.csv");
        }

        private void readWords(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var splitted = line.Split(',');
                    if (splitted[0] == "MelingoId")
                    {
                        continue;
                    }
                    
                    words.Add(splitted[2], splitted[0]);
                }
            }
        }
        private void readExample(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var splitted = line.Split(',');
                    if (splitted[0] == "ID")
                    {
                        continue;
                    }
                    if (!examples.ContainsKey(splitted[1]))
                    {
                        examples[splitted[1]] = new LinkedList<string>();
                    }
                    examples[splitted[1]].AddLast(splitted[2]);
                }
            }
        }

        public LinkedList<string> filter(string word)
        {
            if(!words.ContainsKey(word) || !examples.ContainsKey(words[word]))
            {
                return new LinkedList<string>();
            }
            return examples[words[word]];
        }



    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upload_Video_To_Youtube
{
    class TagsClass
    {
        string[] tagList;
        int arrayLegnth = 0;
        public string[] GetTagList(string tagPath)
        {
            Run(tagPath);
            return tagList;
        }
        void Run(string tagPath)
        {
            Console.WriteLine("Getting Tags");
            using (StreamReader sr = new StreamReader(tagPath))
            {                
                while (sr.ReadLine() != null) // This Loop to count file lines
                {
                    arrayLegnth++;
                }
            }
            using (StreamReader sr = new StreamReader(tagPath))
            {
                int i = 0;
                tagList = new string[arrayLegnth];
                while (sr.Peek() != -1)
                {
                    tagList[i] = sr.ReadLine();
                    i += 1;
                }
            }
            Console.WriteLine("End of Getting Tags");
        }
    }
}

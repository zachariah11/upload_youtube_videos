using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Diagnostics;
namespace  Zachariah
{
	class MainClass
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Entre The Path of Tag file");
			string DescPath = Console.ReadLine();
			string[] tagList;
			var sw = Stopwatch.StartNew();
			int arrayLegnth=0;
			StringBuilder str = new StringBuilder();
			using (StreamReader sr = new StreamReader(DescPath))
            {
                Console.WriteLine("The program retrieving your file linses");
                while (sr.ReadLine() != null) // This Loop to count file lines
                {
                    arrayLegnth++;
                }
                
            }
            using (StreamReader sr = new StreamReader(DescPath))
            {
                int i = 0;
                tagList = new string[arrayLegnth];
                while (sr.Peek() != -1)
                {
                    tagList[i] = sr.ReadLine();
                    i += 1;
                }
            }
			sw.Stop();
			try
			{
			for (int i=0;i<=arrayLegnth;i++)
				Console.WriteLine(tagList[i]);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error:"+ex.Message);
			}
			Console.WriteLine("This Code Take" + sw.Elapsed + "s");
//" /mnt/c/Users/zacha/Downloads/keywordio_longtail_keywords.txt
			Console.ReadKey();
		}
	}
}
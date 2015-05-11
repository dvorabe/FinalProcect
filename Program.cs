using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream bible = new FileStream(@"C:\Users\edberko\Desktop\לימודים\bible11.txt", FileMode.Open,FileAccess.Read);
            FileStream test = new FileStream(@"C:\Users\edberko\Desktop\לימודים\test.txt", FileMode.Truncate,FileAccess.Write);
            StreamWriter writer = new StreamWriter(test);
            StreamReader reader = new StreamReader(bible);
            String str;
           // String[] word = new String [10];
            String [] word;
            
            for (; !reader.EndOfStream;)
            {
                
                str = reader.ReadLine();
                //word= str.Split(new string[] { @"\N" }, StringSplitOptions.None);
                word = str.Split('\t');
                for (int j = 0; j < word.Length; j++)
                {

                    if (word[j].Length > 0)
                    {
                        if ((word[j].ToCharArray(0, word[j].Length)[0] < 127) && (word[j].ToCharArray(0, word[j].Length)[0] > 33))
                        {
                            continue;
                        }
                        else if (word[j] == @"\N")
                            continue;
                        else
                        {
                            writer.Write(word[j] + "\t");
                        }
                    }
                    
                }
                writer.WriteLine();
                //writer.WriteLine(word[0]+word[1]+word[3]+word[4]);
                
            }

            bible.Close();
            test.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addgrams : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string line;
       // string BibleText;
       // string BibleTextFilePath;
        //string BiblePos;
        var context = new DatabaseEntities();
        
        //BibleTextFilePath = @"D:/MyProj/Assets/test.txt";//Server.MapPath(@"D:/MyProj/Assets/bible.txt");
        //FileStream BiblePos = new FileStream(@"C:\Users\edberko\Desktop\לימודים\test.txt", FileMode.Open, FileAccess.Read);
       // BibleText = System.IO.File.ReadAllText(BibleTextFilePath);

        FileStream bible = new FileStream(@"D:/MyProj/Assets/test.txt", FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(bible);
        int h = 0;
        for (; !reader.EndOfStream; )
        {
            h++;
            if (h > 5000)
                h++;
            line = reader.ReadLine();
            string[] words = line.Split('\t');
            string Sefer = words[0].Trim();
            string Perek = words[1].Replace(words[0], "").Trim();
            string MisparPasuk = words[2].Trim();
            string pasuk = words[3].Trim();

            //filter unwanted charecters
            pasuk = Helper.RemoveUnwantedChars(pasuk);

            //splitting to n-groups
            string[] bSplitted = Helper.splitNword(pasuk, 3);

       // loop over the n-splitted aray

            for (int i = 0; i < bSplitted.Length; i++)
            {
                //split the n-senetence to single variables of word
                var s = bSplitted[i].Split(' ');
                var TempFirstWord = s[0];
                var TempSecondWord = s[1];
                var TempThirdWord = s[2];

                //select the wordsd from the database
                var obju = context.grams3
                .Where(g => g.firstword == TempFirstWord
                    && g.secondword == TempSecondWord
                    && g.thirdword == TempThirdWord).FirstOrDefault();

               // ********************
              //  var obju1 = context.grams3pos.Where(g=>g.seferid==Sefer && g=>g.perekid == Perek && g=>pasukid == Misparpasuk);
            

                //if not exist -> insert
                if (obju == null)
                {
                    obju = new grams3();
                    obju.firstword = TempFirstWord;
                    obju.secondword = TempSecondWord;
                    obju.thirdword = TempThirdWord;
                    obju.freq = 1;

                    context.grams3.Add(obju);
                    context.SaveChanges();
                }
                else //if exist -> counter++
                {
                    obju.freq = obju.freq + 1;
                    context.SaveChanges();
                }
                var seferid = context.Sfarim.Where(sf => sf.seferName == Sefer).First().Id; //get the id of the book

                //insert to the grams3pos
                var g3p = context.grams3pos.Where(g => 
                    g.gram3id == obju.Id && g.pasukid == MisparPasuk &&
                    g.perekid == Perek && g.seferid == seferid).FirstOrDefault();
                if (g3p == null)
                {
                    context.grams3pos.Add(new grams3pos() { gram3id = obju.Id, pasukid = MisparPasuk, perekid = Perek, seferid = seferid });
                    context.SaveChanges();
                }
                else
                    continue;
            }
        }
    }


  //  public System.Linq.Expressions.Expression<Func<grams3, int, TResult>> Id { get; set; }
}
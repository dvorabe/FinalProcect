using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addgrams : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BibleText;
        string BibleTextFilePath;
        var context = new DatabaseEntities();
        

        BibleTextFilePath = @"D:/MyProj/Assets/bible.txt";//Server.MapPath(@"D:/MyProj/Assets/bible.txt");
        BibleText = System.IO.File.ReadAllText(BibleTextFilePath);
      
       //filter unwanted charecters
        BibleText = Helper.RemoveUnwantedChars(BibleText);
        
        //splitting to n-groups
        string[] bSplitted = Helper.splitNword(BibleText, 3);

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
                    && g.thirdword == TempThirdWord);
            
            //if not exist -> insert
            if (obju.Count() == 0)
            {
                var obj = new grams3();
                obj.firstword = TempFirstWord;
                obj.secondword = TempSecondWord;
                obj.thirdword = TempThirdWord;
                obj.freq = 1;

                context.grams3.Add(obj);
                context.SaveChanges();
            }
            else if (obju.Count() == 1) //if exist -> counter++
            {
                obju.First().freq = obju.First().freq + 1;
                context.SaveChanges();
            }
            else
            {
                //bug
                errorlbl.Text = "יש בעיה...";
            }

        }
    }

}
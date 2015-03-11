using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public _Default()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
    }

    public List<match> SearchInBibleText(string term, int TotalWords)
    {
        //we work just with 3-gram now

        var context = new DatabaseEntities();
        
        //filter unwanted charecters
        term = Helper.RemoveUnwantedChars(term);

        //splitting to groups
        string[] bSplitted = Helper.splitNword(term, TotalWords);

        List<match> _matches = new List<match>();
        for (int i = 0; i < bSplitted.Length; i++)
        {
            var s = bSplitted[i].Split(' ');
            string TempFirst = s[0];
            string TempSecond = s[1];
            string TempThird = s[2];

            var query = context.grams3
                .Where(g => g.firstword == TempFirst && g.secondword == TempSecond && g.thirdword == TempThird);
            if (query.Count() > 0)
                _matches.Add(new match(bSplitted[i], term.IndexOf(bSplitted[i])));
        }
        return _matches;

        //2 word
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Path.GetExtension(FileUpload1.FileName) != "txt")
        {
            errorlbl.Text = "הקובץ לא מסוג txt";
        }
    
        //the file name
        var userInput= "userinput"+ new Random().Next(0,999).ToString() + Path.GetExtension(FileUpload1.FileName);
        //the file full path (directory + filename)
        var filePath = Path.Combine(Server.MapPath("~"), "Assets", userInput);
        FileUpload1.SaveAs(filePath);
        int TotalWords = int.Parse(TextBox2.Text);
        var filelines = File.ReadAllText(filePath);
        var results = SearchInBibleText(filelines, TotalWords);
        
        for (int i = 0; i < results.Count; i++)
        {
            //PlaceHolderResultsPositions.Controls.Add(new Literal() { Text = string.Format("<div data-id='{0}' class='col-md-1 index'>{1}{2}", i, results[i].pos, "</div>") });
            filelines = filelines.Replace(results[i].text, string.Format("<span id='{0}' class='bg-success'>{1}{2}", i, results[i].text, "</span>"));
        }

        filelines = filelines.Replace(Environment.NewLine, "<br />");
        PlaceHolderTextResults.Controls.Add(new Literal() { Text = filelines });
    }
}
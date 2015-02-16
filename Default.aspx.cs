using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string BibleTextFilePath;
    string BibleText;

    public _Default()
    {
        BibleTextFilePath = @"D:/MyProj/Assets/bible.txt";//Server.MapPath(@"D:/MyProj/Assets/bible.txt");
        BibleText = System.IO.File.ReadAllText(BibleTextFilePath);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
    }

    public List<match> SearchInBibleText(string term, int TotalWords)
    {
        //filter unwanted charecters
        BibleText = RemoveUnwantedChars(BibleText);

        //splitting to groups
        string[] bSplitted = splitNword(BibleText, TotalWords);

        //1 word
        List<match> _matches = new List<match>();
        foreach (var item in bSplitted)
        {
            if (term.IndexOf(item) > -1)
            {
                _matches.Add(new match(item, term.IndexOf(item)));
            }
        }
        return _matches;

        //2 word
    }

    private string RemoveUnwantedChars(string b)
    {
        char[] filterChars = { ',', '.' };
        foreach (var charTofilter in filterChars)
            b.Replace(charTofilter, ' ');
        return b;
    }

    private string[] splitNword(string b, int p)
    {
        List<string> ret = new List<string>();
        var splitted = b.Split(' ');
        for(int i = p - 1; i < splitted.Count(); i++)
        {
            string word = string.Empty;
            for (int j = p - 1; j >= 0; j-- )
            {
                word += splitted[i - j];
                word += " ";
            }
            word = word.Substring(0, word.Length - 1);
            ret.Add(word);
        }
        return ret.ToArray();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Path.GetExtension(FileUpload1.FileName) != "txt")
        {
            errorlbl.Text = "הקובץ לא מסוג txt";
        }
        var BibleTextToShow = BibleText;
       // var TermToSearch = TextBox1.Text;
        var userInput= "userinput"+DateTime.Now.ToShortDateString()+Path.GetExtension(FileUpload1.FileName);
        FileUpload1.SaveAs(Path.Combine(Server.MapPath("~"), "Assets", userInput));
        var TotalWords = int.Parse(TextBox2.Text);
        var filelines = File.ReadAllText(userInput);
        var results = SearchInBibleText(filelines, TotalWords);

        for (int i = 0; i < results.Count; i++)
        {
            PlaceHolderResultsPositions.Controls.Add(new Literal() { Text = string.Format("<div data-id='{0}' class='col-md-1 index'>{1}{2}", i, results[i].pos, "</div>") });
            BibleTextToShow = BibleTextToShow.Replace(results[i].text, string.Format("<span id='{0}' class='bg-success'>{1}{2}", i, results[i].text, "</span>"));
        }

        PlaceHolderTextResults.Controls.Add(new Literal() { Text = BibleTextToShow });
    }
}
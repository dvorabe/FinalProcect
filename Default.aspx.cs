using System;
using System.Collections.Generic;
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
        BibleTextFilePath = Server.MapPath(@"/Assets/bible.txt");
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
            if (term.IndexOf(item) > 0)
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
        var BibleTextToShow = BibleText;
        var TermToSearch = TextBox1.Text;
        var TotalWords = int.Parse(TextBox2.Text);

        var results = SearchInBibleText(TermToSearch, TotalWords);

        for (int i = 0; i < results.Count; i++)
        {
            PlaceHolderResultsPositions.Controls.Add(new Literal() { Text = string.Format("{0}{1}{2}","<div class='col-md-1 '>", results[i].pos,"</div>") });      
        }
        BibleTextToShow = BibleTextToShow.Replace(TermToSearch, string.Format("{0}{1}{2}", "<p class='bg-success'>", TermToSearch, "</p>"));
        PlaceHolderTextResults.Controls.Add(new Literal() { Text = BibleTextToShow });
    }
}
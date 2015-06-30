using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            ////if TotalWors =3;
            var s = bSplitted[i].Split(' ');
            string TempFirst = s[0];
            string TempSecond = s[1];
            string TempThird = s[2];

            var query = context.grams3
                .Where(g => g.firstword == TempFirst && g.secondword == TempSecond && g.thirdword == TempThird);
            if (query.Count() > 0)
                _matches.Add(new match(bSplitted[i], term.IndexOf(bSplitted[i]), query.First().Id));
        }
        return _matches;

        //2 word
        //4word
      //  *******************************************
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        errorlbl.Text = string.Empty;
        if (Path.GetExtension(FileUpload1.FileName) != ".txt")
        {
            errorlbl.Text = "הקובץ לא מסוג txt";
            return;
        }
        else if (string.IsNullOrEmpty(TextBox2.Text))
        {
            errorlbl.Text = "לא הוזן מספר מילים";
            return;
        }
        //the file name
        var userInput= "userinput"+ new Random().Next(0,999).ToString() + Path.GetExtension(FileUpload1.FileName);
        //the file full path (directory + filename)
        var filePath = Path.Combine(Server.MapPath("~"), "Assets", userInput);
        FileUpload1.SaveAs(filePath);
        

        int TotalWords = int.Parse(TextBox2.Text);
        var filelines = File.ReadAllText(filePath);
        var results = SearchInBibleText(filelines, TotalWords);
        //check if was did merging
       // int flag = 0;
        if(results.Count() > 1)
        for (int j = 0; j < results.Count - 1; j++)
        {

            if (results[j].pos + results[j].lenght > results[j+1].pos)
            {
                bool success;
                var s = results[j].text;
                var s1 = results[j+1].text;
                string merging = Helper.merging(s, s1, out success);
                if (success)
                {
                    results[j].text = merging;
                    results[j].lenght = merging.Length;
                    results.RemoveAt(j + 1);
                }
             //   var checkLocal = Helper.checkLocal(s, s1, TotalWords);
              //  results[j].local = checkLocal;

            }

        }

        for (int i = 0; i < results.Count; i++)
        {
            var check = results[i].text.Split(' ');
            int count = check.Count();
            if (count == 2)
            {
                //PlaceHolderResultsPositions.Controls.Add(new Literal() { Text = string.Format("<div data-id='{0}' class='col-md-1 index'>{1}{2}", i, results[i].pos, "</div>") });
                filelines = filelines.Replace(results[i].text, string.Format("<a id='{0}' class='marked2 match' data-reference='{3}'>{1}{2}", i, results[i].text, "</a>", PrintRefernces(results[i].references)));
                continue;
            }
            if (count == 3)
            {
                filelines = filelines.Replace(results[i].text, string.Format("<a id='{0}' class='marked3 match' data-reference='{3}'>{1}{2}", i, results[i].text, "</a>", PrintRefernces(results[i].references)));
                continue;
            }
            if (count >= 4)
            {
                filelines = filelines.Replace(results[i].text, string.Format("<a id='{0}' class='marked4 match' data-reference='{3}'>{1}{2}", i, results[i].text, "</a>", PrintRefernces(results[i].references)));
                continue;
            }
        }
        filelines = filelines.Replace(Environment.NewLine, "<br />");
        PlaceHolderTextResults.Controls.Add(new Literal() { Text = filelines });
    }

    private string PrintRefernces(List<grams3pos> list, bool IsHtmlFormat = true)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var reference in list)
        {
            sb.AppendFormat("<a class='reference' onclick='GetPasuk(\"{0}\", \"{1}\", \"{2}\")' data-prk='{1}' data-psk='{0}' data-sfr='{2}'>{0}, {1}, {2}</a>", reference.pasukid, reference.perekid, reference.seferid);
            if (IsHtmlFormat)
                sb.Append("<br />");
            else
                sb.AppendLine();
        }

        return Server.HtmlEncode(sb.ToString());
    }
}
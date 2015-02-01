using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
   public static int num = 2;//nums of word in ציטוט

   //when a no in DB, like now..
    public static string a = "abc cde abc cde";
    public static string[] aSplitted;

    protected void Page_Load(object sender, EventArgs e)
    {
        aSplitted = splitNword(a);
        string b = "abc cde efg ghi";
    
        //filter unwanted charecters
        b = RemoveUnwantedChars(b);
    

        string[] bSplitted = splitNword(b); // 'num' word splitted array
       
        //search bSplitted in a
        string[] inA = searchBInA(bSplitted);
       

        //1 word
        foreach (var item in bSplitted)
            Response.Write(item + ": " + a.IndexOf(item) + "<br />");

        //search
        foreach (var item1 in inA)
            Response.Write("saerch:" + item1 + ": " + "<br />");

    }

    private string RemoveUnwantedChars(string b)
    {
        char[] filterChars = { ',', '.' };
        foreach (var charTofilter in filterChars)
            b.Replace(charTofilter, ' ');
        return b;
    }

    private string[] splitNword(string b)
    {
        List<string> ret = new List<string>();
        var splitted = b.Split(' ');
        for (int i = num - 1; i < splitted.Count(); i++)
        {
            string word = string.Empty;
            for (int j = num - 1; j >= 0; j--)
            {
                word += splitted[i - j];
                word += " ";
            }
            word = word.Substring(0, word.Length - 1);
            ret.Add(word);
        }
        return ret.ToArray();
    }

    private string[] searchBInA(string[] bSplitted)
    {
        List<string> ret =new List<string>();
        //?????לא הצלחתי למצוא 2 מופעים

      /*  int index;
         for (int j = 0; j <= num; j++)
           {
             //  string newA = a;
            //   while (newA.Length > 0)
              // {
                   index = a.IndexOf(bSplitted[j]);
                   if (index != -1)
                   {
                       ret.Add(bSplitted[j]);
                       //newA = a.Substring(index);//to chek more 1 apear
                   }
             //  }
           }*/
        for (int i = 0; i < aSplitted.Count(); i++)
        {
            for (int j = 0; j < bSplitted.Count(); j++)
            {
                int n = aSplitted[i].CompareTo(bSplitted[j]);
              //  if (aSplitted[i].Equals(bSplitted[j]))
                if (n == 0)
                    ret.Add(bSplitted[j] + '.'+ i);

            }
        }
        return ret.ToArray();
    }
}
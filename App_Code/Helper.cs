using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{

    /// <summary>
    /// This function will get a string from text file and will remove chars that not used to read text.
    /// </summary>
    /// <param name="b">string of text</param>
    /// <returns>filtered string</returns>
    public static string RemoveUnwantedChars(string b)
    {
        char[] filterChars = { ',', '.' };
        foreach (var charTofilter in filterChars)
            b = b.Replace(charTofilter, ' ');

        b = b.Replace(Environment.NewLine, " ");

        //very heavy
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex(@"[ ]{2,}", options);
        b = regex.Replace(b, @" ");
        return b;
    }

    public static string[] splitNword(string b, int p)
    {
        List<string> ret = new List<string>();
        var splitted = b.Split(' ');
        for (int i = p - 1; i < splitted.Count(); i++)
        {
            string word = string.Empty;
            for (int j = p - 1; j >= 0; j--)
            {
                word += splitted[i - j];
                word += " ";
            }
            word = word.Substring(0, word.Length - 1);
            ret.Add(word);
        }
        return ret.ToArray();
    }
    public static string merging(string s, string s1, out bool success)
    {
        success = false;
        string result = string.Empty;
         var sArr= s.Split(' ');
         var s1Arr = s1.Split(' ');
         for (int i = 0; i < sArr.Count(); i++)
         {
             result += sArr[i] + " ";
             if (sArr[i] == s1Arr[0])
             {
                 success = true;
                 result += s1.Replace(sArr[i] + " ", "");
                 break;
             }
                   

         }
         return result;
    }

 /*   public static bool checkLocal(string s, string s1, int TotalWords)
    {
        var context = new DatabaseEntities();
        switch (TotalWords)
        {
            //check if the location of both is a same
            case 2:
            //      var query = context.grams3
            //.Where(g => g.firstword == TempFirst && g.secondword == TempSecond && g.thirdword == TempThird);
            //      break;

            case 3:
                break;
            case 4:
                break;

        }
        return true;
    }*/
}
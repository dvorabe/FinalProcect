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
}
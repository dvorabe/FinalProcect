using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addgrams : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string word1;
        string word2;
        string word3;
        var context = new DatabaseEntities();
 
        //update
        var obju = context.grams3.Where(g => g.firstword == "a" && g.secondword == "b" && g.thirdword == "c");
        if (obju.Count() == 0)
        {
            //now row found
            //insert
            var obj = new grams3();
            obj.firstword = "a";
            obj.secondword = "b";
            obj.thirdword = "c";
            obj.freq = 1;

            context.grams3.Add(obj);
            context.SaveChanges();


        }
        else if (obju.Count() == 1)
        {
            obju.First().freq = obju.First().freq + 1;
            context.SaveChanges();
        }
        else
        {
            //bug
        }
    }
}
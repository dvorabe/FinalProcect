using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoadBible : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        FileStream bible = new FileStream(@"C:\Users\edberko\Desktop\לימודים\test.txt", FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(bible);

        for (; !reader.EndOfStream; )
        {
            string line = reader.ReadLine();
            string[] words = line.Split('\t');

            string Sefer = words[0].Trim();
            string Perek = words[1].Replace(words[0], "").Trim();
            string MisparPasuk = words[2].Trim();
            string pasuk = words[3].Trim();
            InsertToDb(Sefer, Perek, MisparPasuk, pasuk);
        }
    }

    private void InsertToDb(string Sefer, string Perek, string MisparPasuk, string Pasuk)
    {
        var context = new DatabaseEntities();
        var _sefer = context.Sfarim.Where(s => s.seferName == Sefer).FirstOrDefault();

        var i = context.Tanach.Where(t => t.sefer == _sefer.Id && t.perek == Perek && t.pasuk == MisparPasuk && t.txt == Pasuk);
        if (i.Count() > 0)
            return;
        //validate that Sefer exist in database if not - insert
        //if (_sefer.Count() == 0)
        //{
        //    context.Sfarim.Add(new Sfarim() { seferName = Sefer });
        //    context.SaveChanges();
        //}

        //insert the data
        context.Tanach.Add(new Tanach() { sefer = _sefer.Id, perek = Perek, pasuk = MisparPasuk, txt = Pasuk });
        context.SaveChanges();
        

        
    }

    private int GetMisparByChar(string ch)
    {
        if (ch == "א")
            return 1;
        return -1;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class match
{
    public string text;
    public int pos;
    public int lenght;
    public bool local;
    public List<grams3pos> references; 
    public match(string t, int p, int gram3id)
    {
        this.text = t;
        this.pos = p;
        this.lenght = t.Length;
        this.local = true;
        using (var context = new DatabaseEntities())
        {
            this.references = context.grams3pos.Where(a => a.gram3id == gram3id).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class match
{
    public string text;
    public int pos;
    public int lenght;

    public match(string t, int p)
    {
        this.text = t;
        this.pos = p;
        this.lenght = t.Length;
    }
}

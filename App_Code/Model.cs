﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class grams3
{
    public int freq { get; set; }
    public string firstword { get; set; }
    public string secondword { get; set; }
    public string thirdword { get; set; }
    public int Id { get; set; }
}

public partial class grams3pos
{
    public int gram3id { get; set; }
    public int seferid { get; set; }
    public string perekid { get; set; }
    public string pasukid { get; set; }
}

public partial class Sfarim
{
    public Sfarim()
    {
        this.Tanach = new HashSet<Tanach>();
    }

    public int Id { get; set; }
    public string seferName { get; set; }

    public virtual ICollection<Tanach> Tanach { get; set; }
}

public partial class Tanach
{
    public int sefer { get; set; }
    public string perek { get; set; }
    public string pasuk { get; set; }
    public string txt { get; set; }

    public virtual Sfarim Sfarim { get; set; }
}

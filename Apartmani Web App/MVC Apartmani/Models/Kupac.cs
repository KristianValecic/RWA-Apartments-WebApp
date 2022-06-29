using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class Kupac
{
    public int IDKupac { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public int GradID { get; set; }
    public override string ToString()
    {
        return $"{Ime} {Prezime}";
    }
}

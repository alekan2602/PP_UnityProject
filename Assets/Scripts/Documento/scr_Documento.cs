using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DocumentTest", menuName = "Document")]
public class scr_Documento : ScriptableObject
{
    public bool isValid;

    public string country;
    public string city;

    public string firstName;
    public string lastName;
    public string sex;

    public Sprite countrySymbol;

    public scr_Documento(bool v,string co,string ci,string fi,string la, string s,Sprite sy)
    {
        isValid = v;
        country = co;
        city = ci;
        firstName = fi;
        lastName = la;
        sex = s;
        countrySymbol = sy;
    }
}

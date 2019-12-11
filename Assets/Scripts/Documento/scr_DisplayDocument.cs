using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_DisplayDocument : MonoBehaviour
{

    public scr_Documento document;


    public bool isValid;

    public Text country;
    public Text city;
    public Text fullNameInverse;
    public Text sex;

    public Image countrySymbol;


    void Start()
    {
        DisplayDocument(document);
    }


    public void DisplayDocument(scr_Documento d)
    {
        this.isValid = d.isValid;
        this.country.text = d.country;
        this.city.text = d.city;
        //this.lastName.text = d.lastName;
        this.sex.text = d.sex;
        this.countrySymbol.sprite = d.countrySymbol;
        this.fullNameInverse.text = d.lastName + ", " + d.firstName;
    }

    //public Color ChooseColor(string country)
    //{
    //    switch(country)
    //    {
    //        case
    //    }
    //}
}

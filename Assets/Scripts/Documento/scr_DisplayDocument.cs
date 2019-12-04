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

    public Text firstName;
    public Text lastName;
    public Text sex;

    public Image countrySymbol;


    void Start()
    {
        DisplayDocument(document);
    }


    public void DisplayDocument(scr_Documento d)
    {
        isValid = d.isValid;
        country.text = d.country;
        city.text = d.city;
        firstName.text = d.firstName;
        lastName.text = d.lastName;
        sex.text = d.sex;
        countrySymbol.sprite = d.countrySymbol;
        
    }
}

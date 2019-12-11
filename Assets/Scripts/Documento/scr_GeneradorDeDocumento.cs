﻿using UnityEngine;
using UnityEngine.UI;
public class scr_GeneradorDeDocumento : MonoBehaviour
{

    public scr_DisplayDocument canvasDocument;
    public scr_DisplayIdentification canvasIdentification;

    [Range(0.0f,1f)]
    public float validProb;
    public bool validDoc;
    [Space(10)]
    public string[] validCountries;
    public string[,] validCities =
    {
        {"Stein","Hannover","Sonnestadt"},
        {"Grestin","Skal","Yurko"},
        {"Biblos","Arrapha","Heraclea Póntica"},
        {"Cristania","Tecsap","Templar"},
        {"Vivaldi","Passiflora","Redania"},
        {"Mononok","Zenibaba","Hotaru"},
        {"Byob","Tankian","Regkan"}
    };


    public string[] unValidCountries;
    public string[,] unValidCities =
    {
        {"Steain","Hanover","Sonnestat"},
        {"Greztin","Zkal","Yurco"},
        {"Viblos","Arapha","Hereclea Póntica"},
        {"Crhistania","Tecsa","Tenplar"},
        {"Vibaldi","Paciflora","Redanea"},
        {"Monok","Zenivaba","Hotaeru"},
        {"Biob","Tamkian","Regcan"}
    };

    [Space(10)]
    public string[] firstNames;
    public string[] lastNames;
    [Space(10)]
    public Sprite[] validSymbols;
    public Sprite[] unValidSymbols;

    public Text passportFullName;
    public GameObject passportBackground;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GenNewDocument();
        }
    }

    //Funciones
    public void GenNewDocument()
    {
        //Primer paso : Determinar si es válido o no y generar en base a eso
        if (Random.Range(0.0f, 1f) < validProb) validDoc = true;
        else validDoc = false;

        //Creación de Iteradores
        int country_Iterator = Random.Range(0, validCountries.Length);
        int city_Interator = Random.Range(0, 3);

        int fName_Iterator = Random.Range(0, firstNames.Length);
        int lName_Iterator = Random.Range(0, lastNames.Length);

        int sex_selector = Random.Range(0, 2);


        //Segundo paso : Rellenar con variables auxiliares el documento
        string aux_Country;
        string aux_City;
        string aux_firstName;
        string aux_lastName;
        string aux_sex;
        Sprite aux_Symbol;

        //Por default rellenar con los valores validos
        aux_Country = validCountries[country_Iterator];

        if(country_Iterator == 0) //Armunich
        {
            passportBackground.GetComponent<Image>().color = new Color32(195, 76, 76, 255);
        }
        else if(country_Iterator == 1) //Totzka
        {
            passportBackground.GetComponent<Image>().color = new Color32(213, 214, 213, 255);
        }
        else if (country_Iterator == 2) //República Provecta
        {
            passportBackground.GetComponent<Image>().color = new Color32(63, 62, 93, 255);
        }
        else if (country_Iterator == 3) //Lupope
        {
            passportBackground.GetComponent<Image>().color = new Color32(255, 239, 148, 255);
        }
        else if (country_Iterator == 4) //Novigrado
        {
            passportBackground.GetComponent<Image>().color = new Color32(255, 203, 126, 255);
        }
        else if (country_Iterator == 5) //Chigino
        {
            passportBackground.GetComponent<Image>().color = new Color32(23, 106, 0, 255);
        }
        else if (country_Iterator == 6) //Karazin
        {
            passportBackground.GetComponent<Image>().color = new Color32(42, 52, 91, 255);
        }


        aux_City = validCities[country_Iterator, city_Interator];
        aux_Symbol = validSymbols[country_Iterator];




        //Por mientras el sexo será random
        canvasIdentification.sex.text = "Mujer";
        aux_sex = "Mujer";
        if (sex_selector == 1)
        {
            canvasIdentification.sex.text = "Hombre";
            aux_sex = "Hombre";
        }
        //El nombre no esta equivocado ya que el que determinará el error será el documento de identificación
        aux_firstName = firstNames[fName_Iterator];
        aux_lastName = lastNames[lName_Iterator];
        canvasIdentification.firstName.text = aux_firstName;
        canvasIdentification.lastName.text = aux_lastName;

        this.canvasIdentification.fullName.text = aux_firstName + " " + aux_lastName;

        //Preguntar si el documento es invalido
        if (!validDoc)
        {
            //Si es inválido elegir país, ciudad o símbolo y poner el incorrecto
            int c = Random.Range(0, 6);
            Debug.Log(c);

            switch (c)
            {
                case 0:
                    aux_Country = unValidCountries[country_Iterator];
                    break;
                case 1:
                    aux_City = unValidCities[country_Iterator, city_Interator];
                    break;
                case 2:
                    aux_Symbol = unValidSymbols[country_Iterator];
                    break;
                case 3:
                    if(fName_Iterator == 0)
                        canvasIdentification.firstName.text = firstNames[fName_Iterator + 1];
                    else
                        canvasIdentification.firstName.text = firstNames[fName_Iterator - 1];

                    this.canvasIdentification.fullName.text = canvasIdentification.firstName.text + " " + aux_lastName;
                    break;

                case 4:

                    if(lName_Iterator == 0)
                        canvasIdentification.lastName.text = lastNames[lName_Iterator + 1];
                    else
                        canvasIdentification.lastName.text = lastNames[lName_Iterator - 1];

                    this.canvasIdentification.fullName.text = aux_firstName + " " + canvasIdentification.lastName.text;

                    break;
                case 5:
                    if (sex_selector == 1) canvasIdentification.sex.text = "Mujer";
                    else canvasIdentification.sex.text = "Hombre";
                    break;
            }


        }

        //canvasIdentification.fullName.text = aux_firstName + " " + aux_lastName;


        //Tercer paso : Mandar al display Document el nuevo documento creado


        //scr_Documento newDocument = new scr_Documento(true,aux_Country,aux_City,aux_firstName,aux_lastName,aux_Symbol);
        canvasDocument.DisplayDocument(new scr_Documento(validDoc, aux_Country, aux_City, aux_firstName, aux_lastName, aux_sex, aux_Symbol));
    }
}

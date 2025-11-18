using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject GoToQuest;
    public GameObject questPanel;
    public GameObject GoToLogros;
    public GameObject LogrosPanel;


    public TMP_Text Quest1;
    public TMP_Text Quest2;
    public TMP_Text Quest3;
    public TMP_Text Quest4;
    public TMP_Text Quest5;
    public TMP_Text Quest6;
    public TMP_Text Quest7;
    public TMP_Text Quest8;
    public TMP_Text Quest9;
    public TMP_Text Quest10;

    private bool Quest1Completed = false;
    private bool Quest2Completed = false;
    private bool Quest3Completed = false;
    private bool Quest4Completed = false;
    private bool Quest5Completed = false;
    private bool Quest6Completed = false;
    private bool Quest7Completed = false;
    private bool Quest8Completed = false;
    private bool Quest9Completed = false;
    private bool Quest10Completed = false;

    [SerializeField]
    Bonfire bonfire;

    Bandera_Monumento bandera_Monumento;
    BanderaMonumento2 banderaMonumento2;
    BanderaMonumentoHielo banderaMonumentoHielo;
    BanderaMonumentoCuracion banderaMonumentoCuracion;
    Campamento_Script campamento_Script;
    Map_Script map_Script;



    void Start()
    {
        questPanel.SetActive(false);
        LogrosPanel.SetActive(false);
        GoToLogros.SetActive(false);
        LogrosPanel.SetActive(false);

        //bonfire = FindObjectOfType<Bonfire>();
        bandera_Monumento = FindObjectOfType<Bandera_Monumento>();
        banderaMonumento2 = FindObjectOfType<BanderaMonumento2>();
        banderaMonumentoHielo = FindObjectOfType<BanderaMonumentoHielo>();
        banderaMonumentoCuracion = FindObjectOfType<BanderaMonumentoCuracion>();
        campamento_Script = FindObjectOfType<Campamento_Script>();
        map_Script = FindObjectOfType<Map_Script>();
    }

    private void Update()
    {
        // Abrir panel de quests
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questPanel.SetActive(true);
            GoToLogros.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && questPanel.gameObject.activeSelf)
        {
            questPanel.SetActive(false);
            LogrosPanel.SetActive(false);
            GoToLogros.SetActive(false);
            GoToQuest.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && LogrosPanel.gameObject.activeSelf)
        {
            questPanel.SetActive(false);
            LogrosPanel.SetActive(false);
            GoToLogros.SetActive(false);
            GoToQuest.SetActive(false);
        }

        // Si cualquiera de los dos paneles está abierto + cursor libre
        if (questPanel.activeSelf || LogrosPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (questPanel.activeSelf)
        {
            Quest1.gameObject.SetActive(true);
            Quest2.gameObject.SetActive(true);
            Quest3.gameObject.SetActive(true);
            Quest4.gameObject.SetActive(true);
            Quest5.gameObject.SetActive(true);
            Quest6.gameObject.SetActive(true);
            Quest7.gameObject.SetActive(true);
            Quest8.gameObject.SetActive(true);
            Quest9.gameObject.SetActive(true);
            Quest10.gameObject.SetActive(true);
        }
        
        if (!questPanel.activeSelf)
        {
            Quest1.gameObject.SetActive(false);
            Quest2.gameObject.SetActive(false);
            Quest3.gameObject.SetActive(false);
            Quest4.gameObject.SetActive(false);
            Quest5.gameObject.SetActive(false);
            Quest6.gameObject.SetActive(false);
            Quest7.gameObject.SetActive(false);
            Quest8.gameObject.SetActive(false);
            Quest9.gameObject.SetActive(false);
            Quest10.gameObject.SetActive(false);
        }

        Mision1();
        Mision2();
        Mision3();
        Mision4();
        Mision5();
        Mision6();
        Mision7();
        Mision8();
        Mision9();
        Mision10();
    }


    public void QuestToLogros()
    {
        questPanel.SetActive(false);
        LogrosPanel.SetActive(true);
        GoToLogros.SetActive(false);
        GoToQuest.SetActive(true);
    }

    public void LogrosToQuest()
    {
        questPanel.SetActive(true);
        LogrosPanel.SetActive(false);
        GoToLogros.SetActive(true);
        GoToQuest.SetActive(false);
    }


    public void Mision1()
    {
        //hacer la fogata
        if (bonfire.BonfireFinished1 == true)
        {
            Quest1Completed = true;
            Quest1.text = "Mision 1 Completada";
        }
    }

    public void Mision2()
    {
        //encuentra el campamento de supervivientes
        if (campamento_Script.Campamento_Desbloqueado == true)
        {
            Quest2Completed = true;
            Quest2.text = "Mision 2 Completada";
        }
    }

    public void Mision3()
    {
        //busca el mapa
        if (map_Script.Mapa_Desbloqueado == true)
        {
            Quest3Completed = true;
            Quest3.text = "Mision 3 Completada";
        }
    }

    public void Mision4()
    {
        //acaba con la horda de mutantes y activa el monumento
        if (Quest3Completed == true)
        {
            Quest4Completed = true;
            Quest4.text = "Mision 4 Completada";
        }
    }

    public void Mision5()
    {
        //contruye la radio
        if (Quest4Completed == true)
        {
            Quest5Completed = true;
            Quest5.text = "Mision 5 Completada";
        }
    }

    public void Mision6()
    {
        //activa los 4 pilares
        if (bandera_Monumento.Monumento1_Desbloqueado == true && banderaMonumento2.Monumento2_Desbloqueado == true && banderaMonumentoHielo.Monumento3_Desbloqueado == true && banderaMonumentoCuracion.Monumento4_Desbloqueado == true)
        {
            Quest6Completed = true;
            Quest6.text = "Mision 6 Completada";
        }
    }

    public void Mision7()
    {
        //
        if (Quest6Completed == true)
        {
            Quest7Completed = true;
            Quest7.text = "Mision 7 Completada";
        }
    }

    public void Mision8()
    {
        //construye una balsa
        if (Quest7Completed == true)
        {
            Quest8Completed = true;
            Quest8.text = "Mision 8 Completada";
        }
    }

    public void Mision9()
    {
        //navega en la balsa
        if (Quest8Completed == true)
        {
            Quest9Completed = true;
            Quest9.text = "Mision 9 Completada";
        }
    }

    public void Mision10()
    {
        //escapa de la isla
        if (Quest9Completed == true)
        {
            Quest10Completed = true;
            Quest10.text = "Mision 10 Completada";
        }
    }
}

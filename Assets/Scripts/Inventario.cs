using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public int DienteComun = 0;
    public int DienteRaro = 0;
    public int DienteEpico = 0;
    public int DienteLegendario = 0;
    public int Huesos = 0;

    public TMP_Text DientesComunCantidad;
    public TMP_Text DientesRaroCantidad;
    public TMP_Text DientesEpicoCantidad;
    public TMP_Text DientesLegendarioCantidad;
    public TMP_Text HuesosCantidad;

    public GameObject InventarioPanel;
    public bool inventarioAbierto = false;

    Comon_Mutant comon_Mutant;
    Rare_Mutant rare_Mutant;
    Epic_Mutant epic_Mutant;
    Legendary_Mutant legendary_Mutant;

    void Start()
    {
        comon_Mutant = FindObjectOfType<Comon_Mutant>();
        rare_Mutant = FindObjectOfType<Rare_Mutant>();
        epic_Mutant = FindObjectOfType<Epic_Mutant>();
        legendary_Mutant = FindObjectOfType<Legendary_Mutant>();
        InventarioPanel.SetActive(false);
        inventarioAbierto = false;
    }

    void Update()
    {
        DientesComunCantidad.text = "X" + DienteComun.ToString();
        DientesRaroCantidad.text = "X" + DienteRaro.ToString();
        DientesEpicoCantidad.text = "X" + DienteEpico.ToString();
        DientesLegendarioCantidad.text = "X" + DienteLegendario.ToString();
        HuesosCantidad.text = "X" +  Huesos.ToString();

        // SI TIENES 5 NO PUEDES RECIBIR MÁS
        comon_Mutant.puedeDropear = (DienteComun < 5);
        rare_Mutant.puedeDropear = (DienteRaro < 5);
        epic_Mutant.puedeDropear = (DienteEpico < 5);
        legendary_Mutant.puedeDropear = (DienteLegendario < 5);

        if (Input.GetKeyDown(KeyCode.I) && inventarioAbierto == false)
        {
            AbrirInventario();
        }
        if (inventarioAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarInventario();
        }

    }

    public void CerrarInventario()
    {
        InventarioPanel.SetActive(false);
        inventarioAbierto = false;
    }

    public void AbrirInventario()
    {
        InventarioPanel.SetActive(true);
        inventarioAbierto = true;
    }
}

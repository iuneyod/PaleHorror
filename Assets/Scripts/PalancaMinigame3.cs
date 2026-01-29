using System.Collections;
using UnityEngine;

public class PalancaMinigame3 : ObjetoInteractivo
{
    MINIGAME3 minigame3;
    MonsterMinigame3 monster;
    public GameObject palanca;
    public BoxCollider colliderPalanca;
    public GameObject luz1;
    public GameObject luz2;

    public float duracionBajada = 0.5f; // tiempo que tarda en bajar
    private bool bajando = false;

    private void Start()
    {
        minigame3 = FindObjectOfType<MINIGAME3>();
        monster = FindObjectOfType<MonsterMinigame3>();
    }

    public override void ActivarObjeto()
    {
        if (bajando) return;

        minigame3.palancasActivadas++;
        colliderPalanca.enabled = false;

        luz1.GetComponent<Light>().color = Color.green;
        luz2.GetComponent<Light>().color = Color.green;
        luz1.GetComponent<Light>().intensity = 5f;
        luz2.GetComponent<Light>().intensity = 5f;

        StartCoroutine(BajarPalanca());
    }

    IEnumerator BajarPalanca()
    {
        bajando = true;
        minigame3.ruido += 15f;

        // Rotación inicial relativa al padre
        Quaternion inicio = palanca.transform.localRotation;
        // Rotación final: baja "hacia abajo" respecto al padre
        Quaternion fin = inicio * Quaternion.Euler(-150f, 0f, 0f); // ajusta eje según cómo quieras que baje

        float tiempo = 0f;
        while (tiempo < duracionBajada)
        {
            tiempo += Time.deltaTime;
            palanca.transform.localRotation = Quaternion.Slerp(inicio, fin, tiempo / duracionBajada);
            yield return null;
        }

        palanca.transform.localRotation = fin;
        bajando = false;
    }

}

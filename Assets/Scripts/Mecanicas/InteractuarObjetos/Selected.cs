using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1.5f;

    public Texture2D cursorNormal;
    public Texture2D cursorInteract;

    public float normalSize = 32f;
    public float interactSize = 48f;

    public bool canInteract = false;
    public bool sistemaActivo = true;
    public float yMultiplier = 0.6f; // más pequeño = más estrecho en Y


    void Start()
    {
        mask = LayerMask.GetMask("RaycastDetect");
    }

    void Update()
    {
        if (!sistemaActivo)
        {
            canInteract = false;
            return;
        }

        RaycastHit hit;
        canInteract = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, mask))
        {
            if (hit.collider.CompareTag("ObjetoInteractivo"))
            {
                canInteract = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<ObjetoInteractivo>().ActivarObjeto();
                }
            }
        }

        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
    }

    void OnGUI()
    {
        if (!sistemaActivo) return;

        Texture2D tex = canInteract ? cursorInteract : cursorNormal;
        float width = canInteract ? interactSize : normalSize;
        float height = width * yMultiplier;

        Rect rect = new Rect(
            (Screen.width - width) / 2,
            (Screen.height - height) / 2,
            width,
            height
        );

        GUI.DrawTexture(rect, tex);
    }

}

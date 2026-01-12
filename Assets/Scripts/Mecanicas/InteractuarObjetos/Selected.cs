using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1.5f;

    public Texture2D cursor;

    public float normalSize = 32f;
    public float interactSize = 48f;

    public bool canInteract = false;

    void Start()
    {
        mask = LayerMask.GetMask("RaycastDetect");
    }

    void Update()
    {
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
        float size = canInteract ? interactSize : normalSize;

        Rect rect = new Rect(
            (Screen.width - size) / 2,
            (Screen.height - size) / 2,
            size,
            size
        );

        GUI.DrawTexture(rect, cursor);
    }
}

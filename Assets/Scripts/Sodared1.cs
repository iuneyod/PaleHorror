public class Sodared1 : ObjetoInteractivo
{
    public override void ActivarObjeto()
    {
        GameManager.Instance.ComerLata(); // suma al contador global
        Destroy(gameObject);
    }
}

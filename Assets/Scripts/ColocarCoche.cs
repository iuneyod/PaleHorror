public class ColocarCoche : ObjetoInteractivo
{
    public CogerCoche cogerCoche; // arrastrar el coche en el Inspector

    public override void ActivarObjeto()
    {
        if (cogerCoche == null) return;

        if (!cogerCoche.agarrandoObjeto) return;
        if (cogerCoche.ObjetoColocado) return;

        cogerCoche.colocarObjeto = true;
    }

}

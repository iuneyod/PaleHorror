using UnityEngine;

public static class CarryState
{
    public static bool carryingRock = false;
    public static bool carryingHammer = false;
    public static bool carryingAxe = false;
    public static bool carryingCoconut = false;
    public static bool carryingTomato = false;

    public static bool IsCarryingAnything()
    {
        return carryingRock || carryingHammer || carryingAxe || carryingCoconut || carryingTomato;
    }
}

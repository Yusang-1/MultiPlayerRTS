using UnityEngine;

public class SelectedController
{
    private Unit selectedUnit;

    public void GetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void UnitMove(Vector3 destination)
    {
        if (selectedUnit == null) return;

        selectedUnit.controller.AddDestination(destination);
    }
}

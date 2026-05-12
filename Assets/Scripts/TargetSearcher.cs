using System.Collections.Generic;
using UnityEngine;

public class TargetSearcher
{
    private readonly Unit unit;
    private readonly SpatialHash spatialHash;
    private const float attackRange = 1.2f;
    
    public TargetSearcher(Unit unit, SpatialHash spatialHash)
    {
        this.unit = unit;
        this.spatialHash = spatialHash;
    }
    
    private bool hasTarget;
    public void UpdateAttackController()
    {
        if(hasTarget) return;
        
        SearchTarget();
    }
    
    private void SearchTarget()
    {
        List<Unit> targets = spatialHash.GetUnitsInRange(unit.transform.position, attackRange);
        targets.Remove(unit);
        if(targets != null && targets.Count > 0)
        {
            hasTarget = true;
            Debug.Log($"{targets.Count} target found");
        }
    }
}

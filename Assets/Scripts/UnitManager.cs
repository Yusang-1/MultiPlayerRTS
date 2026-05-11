using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private SpatialHash spatialHash;
    public SpatialHash SpatialHash => spatialHash;
    
    [SerializeField] private Unit unit;
    private void Start()
    {
        spatialHash = new SpatialHash();
        Instantiate(unit).Initialize(spatialHash);
    }

    
}

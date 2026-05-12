using Unity.Mathematics;
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
        
        Instantiate(unit, new Vector3(6.7f, 0, 6.7f), quaternion.identity).Initialize(spatialHash);
    }
}

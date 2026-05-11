using UnityEngine;
using System.Collections.Generic;

public class PlayerController
{
    private readonly Unit unit;
    //private readonly SpatialHash spatialHash;

    private readonly Queue<Vector3> destinations;
    
    private bool isMoving;
    
    public PlayerController(Unit unit)
    {
        this.unit = unit;
        //this.spatialHash = spatialHash;
        destinations = new Queue<Vector3>();
    }

    public void UpdateController()
    {
        if(isMoving)
        {
            MoveUnit();            
        }
        else
        {
            GetNextDestination();
        }
    }

    public void AddDestination(Vector3 point)
    {
        destinations.Enqueue(point);
    }

    private Vector3 destination;
    private Vector3 direction;
    private const float standard = 0.01f;
    private void MoveUnit()
    {
        if(direction == Vector3.zero) return;
                
        unit.transform.position += direction * Time.deltaTime;
        
        // 이동 후 spatialHash위치에 변경이 있는지 확인
        // spatialHash.CheckUnitHash(unit);
        
        // 목적지와의 거리가 standard이하면 도착으로 간주하고 다음 목적지 설정
        if (Vector3.SqrMagnitude(destination - unit.transform.position) <= standard)
        {
            GetNextDestination();
        }
    }
    
    private void GetNextDestination()
    {
        if(destinations.Count == 0)
        {
            isMoving = false;
            direction = Vector3.zero;
            return;
        }
        
        isMoving = true;
        destination = destinations.Dequeue();
        direction = (destination - unit.transform.position).normalized;                        
    }
}

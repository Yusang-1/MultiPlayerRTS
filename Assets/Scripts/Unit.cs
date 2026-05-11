using UnityEngine;
using Unity.Netcode;
using System;

public class Unit : NetworkBehaviour, ISelectable
{
    public event Action OnSelected;
    public event Action OnSelectedEnd;

    public PlayerController controller;
    
    public Vector2 Position => transform.position;
    public Vector2Int CurrentKey;

    public float MoveSpeed;

    private void Update()
    {
        controller?.UpdateController();
    }
    
    public void Initialize(SpatialHash spatialHash)
    {
        controller = new PlayerController(this, spatialHash);
    }

    public void Selected()
    {
        OnSelected?.Invoke();
    }

    public void SelectedEnd()
    {
        OnSelectedEnd?.Invoke();
    }
}

public interface ISelectable
{
    public event Action OnSelected;
    public event Action OnSelectedEnd;

    public void Selected();
    public void SelectedEnd();
}

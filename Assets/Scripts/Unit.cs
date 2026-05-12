using UnityEngine;
using Unity.Netcode;
using System;

public class Unit : NetworkBehaviour, ISelectable
{
    public event Action OnSelected;
    public event Action OnSelectedEnd;

    public PlayerController controller;
    private TargetSearcher attackController;
    
    public Vector2Int CurrentKey;

    public float MoveSpeed;

    private void Update()
    {
        controller?.UpdateController();
        attackController?.UpdateAttackController();
    }
    
    public void Initialize(SpatialHash spatialHash)
    {
        controller = new PlayerController(this, spatialHash);
        attackController = new TargetSearcher(this, spatialHash);
    }

    public void Selected()
    {
        OnSelected?.Invoke();
        Debug.Log($"{CurrentKey}");
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

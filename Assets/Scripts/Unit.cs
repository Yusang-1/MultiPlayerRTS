using UnityEngine;
using Unity.Netcode;
using System;

public class Unit : NetworkBehaviour, ISelectable
{
    public event Action OnSelected;
    public event Action OnSelectedEnd;

    public PlayerController controller;

    private Vector2 position;
    public Vector2 Position => position;
    public Vector2Int CurrentKey;

    public float MoveSpeed;

    protected virtual void Start()
    {
        controller = new PlayerController(this);
    }

    private void Update()
    {
        controller?.UpdateController();
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

using UnityEngine;
using Unity.Netcode;

public class testPlayer : Unit
{
    // readonly NetworkVariable<Vector3> playerPosition = new();
    
    // protected override void Start()
    // {
    //     playerPosition.OnValueChanged += OnPositionChanged;
    //     base.Start();
    // }

    // [ServerRpc]
    // private void SendInputToServerServerRpc(Vector3 direction)
    // {
    //     Vector3 newPosition = transform.position + direction * Time.deltaTime;
    //     playerPosition.Value = newPosition;
    // }
    
    // private void OnPositionChanged(Vector3 previousValue, Vector3 newValue)
    // {
    //     transform.position = newValue;
    // }
}

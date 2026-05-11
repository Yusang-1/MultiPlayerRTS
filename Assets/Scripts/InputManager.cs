using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Unit selected;
    private SelectedController selectedController;

    private Vector2 mousePos;

    private void Start()
    {
        selectedController = new SelectedController();
    }

    public void OnCLick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            selected = null;
        }

        // if(context.performed) { }

        if (context.canceled)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.TryGetComponent<Unit>(out selected))
                {
                    Debug.Log($"{selected} : Selected");
                    selectedController.GetSelectedUnit(selected);
                }
                else
                {
                    Debug.Log($"Hit With Not Unit");
                }
            }
            else
            {
                Plane groundPlane = new(Vector3.up, 0f);
                if (groundPlane.Raycast(ray, out float distance)) // Ray가 평면과 교차하는 거리를 계산
                {
                    Vector3 clickedWorldPos = ray.GetPoint(distance); // Ray 상의 해당 거리 지점의 월드 좌표를 얻음
                    Debug.Log($"Ground : {clickedWorldPos}");

                    if (isReservating)
                    {
                        selectedController.ReservateUnitMove(clickedWorldPos); // 예약 이동
                    }
                    else
                    {
                        selectedController.UnitMove(clickedWorldPos); // 일반 이동
                    }
                }
            }
        }
    }

    public void OnGetMousePosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }

    bool isReservating;
    public void OnReservationOrder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isReservating = true;
        }

        if (context.canceled)
        {
            isReservating = false;
        }
    }
}

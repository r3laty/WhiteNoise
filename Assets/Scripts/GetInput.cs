using UnityEngine;
using UnityEngine.InputSystem;

public class GetInput : MonoBehaviour
{
    [SerializeField] private Movement characterController;
    [SerializeField] private FPSCamera cameraController;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        characterController.GetWalk(ctx.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            characterController.GetJump(true);
        }
        else if (ctx.canceled)
        {
            characterController.GetJump(false);
        }
    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        cameraController.GetLook(ctx.ReadValue<Vector2>());
    }
}

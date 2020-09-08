
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputAction inputActions;

    private void Awake() => inputActions = new PlayerInputAction();

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


}

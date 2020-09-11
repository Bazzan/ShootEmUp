using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputAction inputActions;
    public delegate void ShootDelegate();
    public static ShootDelegate shootDelegate;

    private void Awake()
    {
        inputActions = new PlayerInputAction();


    }

    private void OnEnable()
    {

        
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    private void Update()
    {

        //Debug.Log(inputActions.Player.Fire.ReadValue<float>());
        if (inputActions.Player.Fire.ReadValue<float>() == 0) return;

        

        shootDelegate();

    }


}

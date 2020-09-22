using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputAction inputActions;
    public delegate void ShootDelegate();
    public static ShootDelegate shootDelegate;

    public WeaponSwitcher weaponSwitcher;
    public Shield shield;
    
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        //shield = GameManager.instance.PlayerTransform.GetComponentInChildren<Shield>();
    }

    private void OnEnable()
    {

        inputActions.Player.WeaponSwitchers.performed += OnSwitchWeapon;
        inputActions.Player.Shield.performed += OnShieldActivate;
        inputActions.Player.Pause.performed += OnPauseGame;

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

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            weaponSwitcher.SwitchWeapon(1);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            weaponSwitcher.SwitchWeapon(2);
        }

    }

    public void OnShieldActivate(InputAction.CallbackContext context)
    {
        StartCoroutine(shield.ActivateShield());
           
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        GameManager.instance.OnPausGame();
    }


}

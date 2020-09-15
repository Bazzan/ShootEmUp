using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputAction inputActions;
    public delegate void ShootDelegate();
    public static ShootDelegate shootDelegate;

    public WeaponSwitcher weaponSwitcher;
    private void Awake()
    {
        inputActions = new PlayerInputAction();

    }

    private void OnEnable()
    {

        inputActions.Player.WeaponSwitchers.performed += OnSwitchWeapon;
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




}

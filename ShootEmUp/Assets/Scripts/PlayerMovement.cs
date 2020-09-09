using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 wasdInput;
    private Rigidbody playerBody;

    private Vector3 lookDirection;
    private float degreesToRotate;

    private Camera playerCamera;


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        playerRotate();
        Move();
    }

    private void playerRotate()
    {

        lookDirection = PlayerInputManager.inputActions.Player.Look.ReadValue<Vector2>();
        lookDirection = lookDirection - playerCamera.WorldToScreenPoint(transform.position);

        degreesToRotate = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        playerBody.MoveRotation(Quaternion.Euler(0, degreesToRotate ,0));
        
    }

    private void Move()
    {
        wasdInput = PlayerInputManager.inputActions.Player.Move.ReadValue<Vector2>();

        playerBody.velocity = new Vector3(wasdInput.x * speed,0, wasdInput.y * speed);
        playerBody.AddForce(wasdInput * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }


}

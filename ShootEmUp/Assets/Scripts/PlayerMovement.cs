using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 wasdInput;
    private Vector2 mouseInput;
    private Rigidbody2D playerBody;

    private Vector3 lookDirection;
    private float degreesToRotate;



    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        playerRotate();
        Move();
    }

    private void playerRotate()
    {

        lookDirection = PlayerInputManager.inputActions.Player.Look.ReadValue<Vector2>();
        lookDirection = lookDirection - Camera.main.WorldToScreenPoint(transform.position);

        degreesToRotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        playerBody.MoveRotation(degreesToRotate - 90);

    }

    private void Move()
    {
        wasdInput = PlayerInputManager.inputActions.Player.Move.ReadValue<Vector2>();

        playerBody.velocity = new Vector2(wasdInput.x * speed, wasdInput.y * speed);
        playerBody.AddForce(wasdInput * speed * Time.fixedDeltaTime, ForceMode2D.Force);
    }


}

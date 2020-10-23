using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IStagger
{
    public float speed;
    public float maxSpeed;
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

        Vector3 direction = new Vector3(wasdInput.x , 0, wasdInput.y);

        if(playerBody.velocity.magnitude < maxSpeed)
        {
        playerBody.AddForce(direction * speed, ForceMode.Acceleration);
        }
        
    }

    
    public void PlayerIsHit(Vector3 direction)
    {
        playerBody.AddForce(direction * 5f, ForceMode.Impulse);
    }

    public void Stagger(StaggerType staggerType, Vector3 force)
    {
        if (staggerType == StaggerType.Stagger)
            playerBody.velocity = Vector3.zero;
        else if (staggerType == StaggerType.Pushback)
            playerBody.AddForce(force, ForceMode.Impulse);
    }
}

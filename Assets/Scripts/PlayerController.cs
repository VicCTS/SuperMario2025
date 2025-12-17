using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 startPosition;

    public float movementSpeed = 5f;
    public float jumpForce = 10;
    public int direction = 1;

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    private InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;

    public Rigidbody2D rBody2D;
    private SpriteRenderer render;
    private GroundSensor sensor;

    void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        sensor = GetComponentInChildren<GroundSensor>();

        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        transform.position = startPosition;  
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        //transform.position = new Vector3(transform.position.x + direction * movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //transform.Translate(new Vector3(direction * movementSpeed * Time.deltaTime, 0, 0));
        
        //transform.position = Vector2.MoveTowards(transform.position, finalPosition, movementSpeed * Time.deltaTime);

        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + direction, transform.position.y), movementSpeed * Time.deltaTime);

    
        //transform.position = new Vector3(transform.position.x + moveDirection.x * movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if(moveDirection.x > 0)
        {
            render.flipX = false;
        }
        else if(moveDirection.x < 0)
        {
            render.flipX = true;
        }
        

        if(jumpAction.WasPressedThisFrame() && sensor.isGrounded)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }
}

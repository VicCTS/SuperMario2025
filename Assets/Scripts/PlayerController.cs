using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 startPosition;

    public float movementSpeed = 5f;
    public float jumpForce = 10;
    public float bounceForce = 10;
    public int direction = 1;

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    private InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;
    private InputAction _pauseAction;
    private InputAction _attackAction;

    public Rigidbody2D rBody2D;
    private SpriteRenderer render;
    private GroundSensor sensor;
    private Animator animator;
    private GameManager _gameManager;
    public ParticleSystem _walkParticles;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    bool _canShoot = false;
    float _powerUpDuration = 10;
    float _powerUpTimer;

    public GameObject attackHitBox;

    void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        sensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //_walkParticles = GetComponentInChildren<ParticleSystem>();

        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        _pauseAction = InputSystem.actions["Pause"];
        _attackAction = InputSystem.actions["Attack"];
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
        if(_pauseAction.WasPressedThisFrame())
        {
            _gameManager.Pause();
        }

        if(_gameManager._pause == true)
        {
            return;
        }

        moveDirection = moveAction.ReadValue<Vector2>();

        //transform.position = new Vector3(transform.position.x + direction * movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //transform.Translate(new Vector3(direction * movementSpeed * Time.deltaTime, 0, 0));
        
        //transform.position = Vector2.MoveTowards(transform.position, finalPosition, movementSpeed * Time.deltaTime);

        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + direction, transform.position.y), movementSpeed * Time.deltaTime);

    
        //transform.position = new Vector3(transform.position.x + moveDirection.x * movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if(moveDirection.x > 0)
        {
            //render.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("IsRunning", true);
            if(!_walkParticles.isPlaying && sensor.isGrounded)
            {
                _walkParticles.Play();
            }
        }
        else if(moveDirection.x < 0)
        {
            //render.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //transform.eulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("IsRunning", true);
            if(!_walkParticles.isPlaying && sensor.isGrounded)
            {
                _walkParticles.Play();
            }
            
        }
        else
        {
            animator.SetBool("IsRunning", false);
            if(_walkParticles.isPlaying)
            {
                _walkParticles.Stop();
            }
            
        }
        

        if(jumpAction.WasPressedThisFrame() && sensor.isGrounded)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //_walkParticles.Stop();
        }

        if(!sensor.isGrounded && _walkParticles.isPlaying)
        {
            _walkParticles.Stop();
        }

        if(_attackAction.WasPressedThisFrame() && _canShoot)
        {
            Shoot();
            //Attack();
            //animator.SetTrigger("Attack");
        }   

        if(_canShoot)
        {
            ShootPowerUp();
        }     

        animator.SetBool("IsJumping", !sensor.isGrounded);
    }

    void ShootPowerUp()
    {
        _powerUpTimer += Time.deltaTime;

        if(_powerUpTimer >= _powerUpDuration)
        {
            _canShoot = false;
        }
    }

    void FixedUpdate()
    {
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }

    public void Bounce()
    {
        rBody2D.linearVelocity = new Vector2(rBody2D.linearVelocity.x, 0);
        rBody2D.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    void Attack()
    {
        if(attackHitBox.activeInHierarchy)
        {
            attackHitBox.SetActive(false);
        }
        else
        {
            attackHitBox.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PowerUp"))
        {
            _powerUpTimer = 0;
            _canShoot = true;
        }
    }
}

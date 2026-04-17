using UnityEngine;
using UnityEngine.UI;

public class Goomba : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;
    private GameManager _gameManager;

    public AudioClip deathSFX;

    public float movementSpeed = 4;
    public int direction = 1;
    private int _goombaHealth = 3;
    private Slider _healthSlider;

    public Transform[] patrolPoints;
    public int patrolIndex = 0;

    private Transform playerPosition;
    public float detectionRange = 5;
    public float attackRange = 1;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _healthSlider = GetComponentInChildren<Slider>();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthSlider.maxValue = _goombaHealth;
        _healthSlider.value = _goombaHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);

        if(distanceToPlayer > detectionRange)
        {
            Patrol();
        }
        else if(distanceToPlayer < detectionRange && distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
        else if(distanceToPlayer < attackRange)
        {
            Attack();
        }
        
    }

    void Patrol()
    {
        float distanceToPoint = Vector3.Distance(transform.position, patrolPoints[patrolIndex].position);

        if(distanceToPoint < 0.5f)
        {
            if(patrolIndex == 0)
            {
                patrolIndex = 1;
            }
            else
            {
                patrolIndex = 0;
            }
        }

        Vector3 moveDirection = patrolPoints[patrolIndex].position - transform.position;      

        Movement(moveDirection);
    }

    void FollowPlayer()
    {
        Vector3 moveDirection = playerPosition.position - transform.position;

        Movement(moveDirection);
    }

    void Movement(Vector3 moveDirection)
    {
        if(moveDirection.x < 0)
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveDirection.x > 0)
        {
            direction = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _rigidBody2D.linearVelocity = new Vector2(direction * movementSpeed, _rigidBody2D.linearVelocity.y);
    }

    void Attack()
    {
        direction = 0;

        Debug.Log("Atacando");
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tuberias") || collision.gameObject.layer == 7)
        {
            //Esto hacec lo mismo que la linea de abajo pero de forma mas intuitiva
            //direction = direction * -1;
            direction *= -1;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }*/

    public void TakeDamage(int damage, Vector3 impactDirection, float impactForce)
    {
        _goombaHealth -= damage;
        _healthSlider.value = _goombaHealth;

        _rigidBody2D.AddForce(impactDirection * impactForce, ForceMode2D.Impulse);

        if(_goombaHealth <= 0)
        {
            GoombaDeath();
        }
    }

    public void GoombaDeath()
    {
        _gameManager.AddKill();
        
        _audioSource.PlayOneShot(deathSFX);

        movementSpeed = 0;

        _boxCollider.enabled = false;

        Destroy(gameObject, 1);

        //_audioSource.clip = deathSFX;
        //_audioSource.Play();   
    }
}

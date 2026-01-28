using UnityEngine;

public class Goomba : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;
    private GameManager _gameManager;

    public AudioClip deathSFX;

    public float movementSpeed = 4;
    public int direction = 1;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _rigidBody2D.linearVelocity = new Vector2(direction * movementSpeed, _rigidBody2D.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
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

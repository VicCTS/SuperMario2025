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

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _healthSlider = GetComponentInChildren<Slider>();
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

    public void TakeDamage()
    {
        _goombaHealth--;
        _healthSlider.value = _goombaHealth;

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

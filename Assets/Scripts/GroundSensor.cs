using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    PlayerController _playerScript;
    public ParticleSystem _jumpParticles;

    public int jumpAttackDamage = 3;

    void Awake()
    {
        _playerScript = GetComponentInParent<PlayerController>();
        //_jumpParticles = GetComponentInChildren<ParticleSystem>();
    }

    public bool isGrounded;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
            _jumpParticles.Play();
        }

        /*if(collision.gameObject.layer == 7)
        {
            //Destroy(collision.gameObject);
            Goomba _enemyScript  = collision.gameObject.GetComponent<Goomba>(); 
            _enemyScript.TakeDamage(jumpAttackDamage, Vector3.zero, 0);

            _playerScript.Bounce();
        }*/
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = false;
        }
    }
}

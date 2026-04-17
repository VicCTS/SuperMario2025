using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rBody;

    public float bulletSpeed = 10;
    public int bulletDamage = 1;
    public float bulletImpactForce = 10;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("tag de moneda"))
        {
            return;
        }

        if(collision.gameObject.layer == 7)
        {
            //Destroy(collision.gameObject);
            Goomba _enemyScript  = collision.gameObject.GetComponent<Goomba>(); 
            _enemyScript.TakeDamage(bulletDamage, transform.right, bulletImpactForce);
        }

        Destroy(gameObject);
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rBody;

    public float bulletSpeed = 10;

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
        Destroy(gameObject);
    }
}

using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public int attackDamage = 3;
    public float attackImpactForce = 30;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 7)
        {
            Goomba enemyScript = collider.gameObject.GetComponent<Goomba>();
            enemyScript.TakeDamage(attackDamage, transform.right, attackImpactForce);
        }
    }
}

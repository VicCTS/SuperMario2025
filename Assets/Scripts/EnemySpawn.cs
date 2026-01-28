using Unity.Mathematics;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPosition;


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private BoxCollider2D _boxCollider;

    public GameObject[] enemyPrefab;

    public Transform[] spawnPoints;

    public int enemiesToSpawn = 5;

    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }


    /*void SpawnEnemy()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
        }
    }*/

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            //Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
            //Instantiate(enemyPrefab, spawnPosition2.position, Quaternion.identity);
            //Instantiate(enemyPrefab, spawnPoints[0].position, Quaternion.identity);
            foreach (Transform item in spawnPoints)
            {
                Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], item.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _boxCollider.enabled = false;
            StartCoroutine(SpawnEnemy());
            //StartCoroutine("SpawnEnemy");
        }
    }
}

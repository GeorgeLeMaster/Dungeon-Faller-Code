using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public int spawnAmnt;
    public int health;

    private bool spawnedNew;

    public GameObject obsticlePrefab;
    public GameObject enemyPrefab;
    public GameObject chestPrefab;

    public GameObject chunkPrefab;
    public GameObject plinkoPrefab;

    public Transform centerPoint;
    public Transform player;
    public Transform spawnPoint;

    public LayerMask spawnMask;

    // Start is called before the first frame update
    void Start()
    {
        chunkPrefab = GameObject.Find("ChunkPrefab");

        player = GameObject.Find("Player").transform;

        spawnedNew = false;

        for (int i  = 0; i < spawnAmnt; i ++)
        {
            SpawnObstical();
        }

        if (spawnAmnt >= 5)
        {
            for (int i = 0; i < 6; i++)
            {
                SpawnEnemy();
            }
        }

        if (spawnAmnt >= 6)
        {

            SpawnChest();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        health = player.gameObject.GetComponent<PlayerCollision>().health;

        if (spawnedNew == false && player.transform.position.y <= centerPoint.transform.position.y && Vector3.Distance(player.transform.position, this.transform.position) <= 10f)
        {

            if (health <= 0)
            {
                Plinko();
            }
            else
            {
                if (this.spawnAmnt  < 7)
                {
                    chunkPrefab.GetComponent<ChunkGenerator>().spawnAmnt = this.spawnAmnt + 1;
                }
                else
                {
                    chunkPrefab.GetComponent<ChunkGenerator>().spawnAmnt = this.spawnAmnt;
                }

                SpawnChunk();
            }

        }
    }

    void SpawnEnemy()
    {
        float spawnChance = Random.Range(0, 11);
        Vector3 spawnPos = new Vector3(centerPoint.position.x + Random.Range(-7f, 7f), centerPoint.position.y + Random.Range(-5f, 5f), 0);


        if (spawnChance >= 8 && !Physics2D.OverlapCircle(spawnPos, 1f, spawnMask))
        {
            Instantiate(enemyPrefab, spawnPos, gameObject.transform.rotation, this.gameObject.transform);
            return;
        }
    }

    void SpawnObstical()
    {
        Vector3 spawnPos = new Vector3(centerPoint.position.x + Random.Range(-8f, 8f), centerPoint.position.y + Random.Range(-5f, 5f), 0);

        if (!Physics2D.OverlapCircle(spawnPos, 1.5f, spawnMask))
        {
            Instantiate(obsticlePrefab, spawnPos, new Quaternion(0, 0, Random.Range(0,360), 0), this.gameObject.transform);
        }
    }

    void SpawnChest()
    {
        float spawnChance = Random.Range(0, 1000);
        Vector3 spawnPos = new Vector3(centerPoint.position.x + Random.Range(-7f, 7f), centerPoint.position.y + Random.Range(-5f, 5f), 0);


        if (spawnChance + Mathf.RoundToInt(Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, 0))) >= 950f && !Physics2D.OverlapCircle(spawnPos, 1f, spawnMask))
        {
            Instantiate(chestPrefab, spawnPos, gameObject.transform.rotation, this.gameObject.transform);
            return;
        }
    }

    void SpawnChunk()
    {

        spawnedNew = true;
        
        Instantiate(chunkPrefab, this.spawnPoint.position, spawnPoint.rotation);
        
    }

    void Plinko()
    {
        spawnedNew = true;

        Instantiate(plinkoPrefab, this.spawnPoint.position, spawnPoint.rotation);
    }
}

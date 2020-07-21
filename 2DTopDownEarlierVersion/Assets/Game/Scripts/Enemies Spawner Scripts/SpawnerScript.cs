using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;
    public int maxEnemies;

    private int enemiesSpawned = 0;

    [SerializeField] Vector3 SpawnValues;
    [SerializeField] float SpawnWait;
    [SerializeField] float SpawnMostWait;
    [SerializeField] float SpawnLeastWait;
    [SerializeField] int StartWait;
    [SerializeField] bool Stop;
    [SerializeField] float MaxDifrenceBetweenSpawning;

    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        SpawnWait = Random.Range(SpawnLeastWait, SpawnMostWait);


        if (maxEnemies <= enemiesSpawned)
        {
            Stop = true;
        }
        else
        {
            Stop = false;
        }
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(StartWait);

        while (!Stop)// w czasie jak nie ma stopu
        {
            int randEnemy = Random.Range(0, 3);

            Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), 0, Random.Range(-SpawnValues.z, SpawnValues.z));

            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
           // enemies[randEnemy].SetActive(true);

            yield return new WaitForSeconds(SpawnWait);
            enemiesSpawned++;
        }
    }
}

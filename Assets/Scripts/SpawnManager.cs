using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawnning = false;
    [SerializeField]
    private GameObject[] _powerups;


    public void startSpawnning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f); // Waiting for 3 sec after asteroid is exploded to start spwanning enemies and powerups
        while (_stopSpawnning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnTripleShotPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f); // Waiting for 3 sec after asteroid is exploded to start spwanning enemies and powerups
        //Spawning Triple shot power up in rseconds to 7 seconds range.
        while (_stopSpawnning == false)
        {
            int randomPowerUp = Random.Range(0, 3); //Random.Range excludes the outer range. This gives values 0,1,2 but not 3.
            GameObject newTripleShotPowerUp = Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawnning = true;
    }
}

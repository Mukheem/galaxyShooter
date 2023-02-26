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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawnning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnTripleShotPowerupRoutine()
    {
        //Spawning Triple shot power up in rseconds to 7 seconds range.
        while(_stopSpawnning == false)
        {
            int randomPowerUp = Random.Range(0, 2);
            GameObject newTripleShotPowerUp = Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawnning = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _TripleShotPowerUP;
    [SerializeField]
    private GameObject _PowerUPContainer;
    [SerializeField]
    private GameObject[] _PowerUPs;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUPRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn objects roughly every 5s, create IEnumerator that allows ***yielding***
    //using while loops
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2);
        //While loops in IENumerators can be infinite as yield allows this.
        while (_stopSpawning == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-10.0f, 10.0f), 7f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        } 
    }

    IEnumerator SpawnPowerUPRoutine()
    {
        yield return new WaitForSeconds(4);
        while (_stopSpawning == false)
        {
            Vector3 _randomSpawn = new Vector3(Random.Range(-10.0f, 10.0f), 7f, 0f);
            GameObject newPowerup = Instantiate(_PowerUPs[Random.Range(0, 3)], _randomSpawn, Quaternion.identity);
            newPowerup.transform.parent = _PowerUPContainer.transform;
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    [SerializeField]
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, 0f, 1 * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.tag == "Projectile")
        {           
            _spawnManager.StartSpawning();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(otherObj.gameObject);
            Destroy(this.gameObject, 0.5f);

        }
    }
}

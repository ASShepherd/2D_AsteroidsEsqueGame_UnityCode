using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    private bool _isEnemyLaser = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            PlayerLaser();
        }
        else if (_isEnemyLaser == true)
        {
            EnemyLaser();
        }

    }

    void PlayerLaser()
    {

            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (transform.position.y >= 8f)
            {
                //check if object has a parent
                //if obj has parent destroy parent too
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            
            }
    }

    void EnemyLaser()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4.5f)
        {
            //check if object has a parent
            //if obj has parent destroy parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);

        }
    }


    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }


    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if (collisionObj.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = collisionObj.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

    }

}

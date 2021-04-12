using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private bool _enemyDead = false;

    private Player _player;
    //handle to animator component
    private Animator _enemyAnimator;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        _enemyAnimator = gameObject.GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Enemy Script: Player not found!");
        }
        if (_enemyAnimator == null)
        {
            Debug.Log("Enemy Script: Animator not found!");
        }
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement();
        enemyFire();

    }
    private void OnTriggerEnter2D(Collider2D otherObj)
    {   //if other = player, damage player THEN destroy this
        //if other = laser, destroy laser and THEN this
        if (otherObj.gameObject.tag == "Player")
        {
            Player player = otherObj.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                _enemyAnimator.SetTrigger("OnEnemyDeath");
                _audioSource.Play();
                //Debug.Log("The enemy hit the player, damage dealt!");
                _speed = 0.5f;
                _enemyDead = true;
                Destroy(GetComponent<EdgeCollider2D>());
                Destroy(this.gameObject, 2.35f);
            }
        }

        else if (otherObj.gameObject.tag == "Projectile")
        {
            Destroy(otherObj.gameObject);
            if(_player != null)
            {
                _player.AddScore(Random.Range(5, 13));
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            //+10 score and communicate it to the UI
            _speed = 0.5f;
            Destroy(GetComponent<EdgeCollider2D>());
            Destroy(this.gameObject, 2.35f);
        }

    }

    void enemyMovement()
    {
        /*Random.Range(-2f, 2f)    possible x values if smoothed*/
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0f);
            _speed += 0.5f;
        }
    }

    void enemyFire()
    {
        if (Time.time > _canFire && _enemyDead == false)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject _currentlaserCont = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            //Debug.Break();
            Laser[] _CurrentLasers = _currentlaserCont.GetComponentsInChildren<Laser>();
            foreach (Laser laser in _CurrentLasers)
            {
                laser.AssignEnemyLaser();
            }
        }
    }

}

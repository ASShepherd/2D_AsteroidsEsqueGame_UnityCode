using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate;
    private float nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _SpawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _tripleShotActive = false;
    [SerializeField]
    private float _SpeedBoostValue = 2;
    private bool _ShieldsActive = false;
    [SerializeField]
    private GameObject _ShieldVisualiser;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightDamage, _leftDamage;

    private AudioSource _LaserSFX;



    // Start is called before the first frame update
    void Start()
    {
        _ShieldVisualiser.SetActive(false);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _LaserSFX = GameObject.Find("Audio_Manager").transform.Find("Laser_SFX").GetComponent<AudioSource>(); //can also use serialise and assign in inspector
        if (_SpawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL!");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL!");
        }
        if (_LaserSFX == null)
        {
            Debug.LogError("Laser SFX on Player Script is NULL!");
        }
        //take position transform and set to a spawn location

        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            playerFire();
        }
    }

    void playerMovement()  //Constrains and allows player movement
    {

        //allows player movement using unity inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        Vector3 playerVector = new Vector3(horizontalInput, verticallInput, 0);
        transform.Translate(playerVector * _speed * Time.deltaTime);


        //limits the player on the y axis
        if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0f);
        }
        else if (transform.position.y <= -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-4.0f, 6.0f), 0);

        //wraps the player on the x axis
        if (transform.position.x >= 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.5)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }
    }


    void playerFire()
    {
        //if hit space, spawn the laser gameobject and move it up
        //firerate is a fixed variable added to time passed to only allow shot after certain time delay
            nextFire = Time.time + _fireRate;
        if(_tripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if (_tripleShotActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity);
        }
        _LaserSFX.Play();
    }

    public void Damage()
    {

        //if shields are up, do nothing, then disable shields and return
        if (_ShieldsActive == true)
        {
            _ShieldsActive = false;
            _ShieldVisualiser.SetActive(false);
            return;
        }
        else if (_ShieldsActive == false)
        {
            _lives--;
            if (_lives == 2)
            {
                _rightDamage.SetActive(true);
            }
            else if(_lives == 1)
            {
                _leftDamage.SetActive(true);
            }

            _uiManager.UpdateLives(_lives);
        }

        //check if dead, if dead, destroy player
        if (_lives <= 0)
        {
            _SpawnManager.OnPlayerDeath();
            //Tell SpawnManager to stop spawning enemies
            Destroy(this.gameObject);
        }
    }

    public void enableTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(DisableTripleShot());
    }

    IEnumerator DisableTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }


    public void EnableSpeedBoost()
    {
        _speed = _speed * _SpeedBoostValue;
        StartCoroutine(DisableSpeedBoost());
    }

    public void EnableShields()
    {
        _ShieldsActive = true;
        _ShieldVisualiser.SetActive(true);
    }

    IEnumerator DisableSpeedBoost()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = _speed/_SpeedBoostValue;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

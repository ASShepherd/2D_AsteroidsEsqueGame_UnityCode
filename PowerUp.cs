using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private Player _PlayerRef;
    //ID for powerups, 0= tripshot, 1 = speed, 2 = shields
    [SerializeField]
    private int PowerUPID;
    [SerializeField]
    //private AudioSource _PowerUPSFX;
    private AudioClip _PowerUPSFX;

    // Start is called before the first frame update
    void Start()
    {
       // _PowerUPSFX = GameObject.Find("Audio_Manager").transform.Find("PowerUP_SFX").GetComponent<AudioSource>(); //Solution1

    }

    // Update is called once per frame
    void Update()
    {
        powerUPMovement();

    }

    void powerUPMovement() 
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if (collisionObj.tag == "Player")
        {
            pickUpPowerUP();
            AudioSource.PlayClipAtPoint(_PowerUPSFX, transform.position);
            //_PowerUPSFX.Play();
        }
    }

    void pickUpPowerUP()
    {   

        _PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        if (_PlayerRef != null)
        {
            switch (PowerUPID)
            {
                case 0:
                    {
                        _PlayerRef.enableTripleShot();
                        break;
                    }
                case 1:
                    {
                        _PlayerRef.EnableSpeedBoost();
                        break;
                    }
                case 2:
                    {
                        _PlayerRef.EnableShields();
                        break;
                    }
                 default :
                    {
                        Debug.Log("Default Powerup Value!");
                        break;
                    }

            }
        }
        Destroy(this.gameObject);
    }
    
}

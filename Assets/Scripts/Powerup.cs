using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3f;
    [SerializeField]
    private int _powerup_id; // 1 = Triple Shot; 2 = Speed; 3=Shield
    
    
    [SerializeField]
    private AudioClip _audioClipPowerUp;

    
    // Update is called once per frame
    void Update()
    {
       
        // Moving down at rate of _powerupSpeed
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);

        if(transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }

    // Parameter is renamed to Other because it stores the information of the other objected that is collided with Powerup.
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                AudioSource.PlayClipAtPoint(_audioClipPowerUp, transform.position);
                switch (_powerup_id){
                    case 0:
                        player.ChangeTripShotActive(true);
                        
                        Debug.Log("Triple Shot Powerup Activated");
                        break;
                    case 1:
                        player.ChangePlayerSpeed(15f);
                        
                        Debug.Log("Speed Powerup Activated");
                        break;
                    case 2:
                        player.ChangeSheidstatus(true);
                        
                        Debug.Log("Shield Powerup Activated");
                        break;
                    default:
                        player.ChangeTripShotActive(true);
                        Debug.Log("Default - Triple Shot Powerup Activated");
                        break;
                }
                Destroy(this.gameObject);
            }
            
        }
    }

}

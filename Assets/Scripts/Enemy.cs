using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 2f;
    private Player _player;
    private Animator _enemyExplode_anim;

    [SerializeField]
    private AudioClip _explosionAudioClip;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefabForEnemy;
    private float _enemyLaserFireRate = 3.0f;
    private float _canEnemyLaserFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        ////Take the current postion = new postion(0,0,0)
        transform.position = new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is not Initialised. Still NULL...");
        }
        _enemyExplode_anim = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null for asteroid explosion...");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Time.time > _canEnemyLaserFire && _player.getLevel() >=3 )
        {
            _enemyLaserFireRate = Random.Range(3.0f, 9.0f);
            _canEnemyLaserFire = Time.time + _enemyLaserFireRate;
            GameObject enemyLaser = Instantiate(_laserPrefabForEnemy, transform.position, Quaternion.identity);
            Laser[] lasers= enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].assignIsEnemyLaser();
            }
        }
    }
    void CalculateMovement()
    {
        // Moving enemy down
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        
        // Respawning enemy at the top if it readched to the bottom
        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8.48f, 8.48f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            
            _enemyExplode_anim.SetTrigger("OnEnemyDeath");// Setting trigger to On so that animation plays
            _enemySpeed = 0; // Freezing enemy as soon as it is hit so that the player doesnot loose life
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
        }

        if (other.tag == "Laser" && other.tag != "Enemy_Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _enemyExplode_anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>()); // Destroying collider would prevent production of sound even if the object is not destroyed.
            _canEnemyLaserFire = Time.time+10f; // This is to stop the enemy fire after explosion and before destruction.
            Destroy(this.gameObject,2.8f);
        }
    }
   
}

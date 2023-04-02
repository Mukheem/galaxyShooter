using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    private Player _player;
    private Animator _enemyExplode_anim;

    [SerializeField]
    private AudioClip _explosionAudioClip;
    private AudioSource _audioSource;

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

        if (other.tag == "Laser")
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
            Destroy(this.gameObject,2.8f);
        }
    }
}

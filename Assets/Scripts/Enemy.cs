using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        ////Take the current postion = new postion(0,0,0)
        transform.position = new Vector3(Random.Range(-8.48f, 8.48f), 7f, 0);
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
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

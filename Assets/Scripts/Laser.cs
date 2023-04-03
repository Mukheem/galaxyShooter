using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9.0f;
    private bool _isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == true)
        {
            MoveDown();
            
        }
        else
        {
            MoveUp();
        }
    }

    void MoveUp()
    {
        // firing laser
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // destroying fired laser
        if (transform.position.y >= 7f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    void MoveDown()
    {
        // firing laser
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // destroying fired laser
        if (transform.position.y <= -7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void assignIsEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }
}

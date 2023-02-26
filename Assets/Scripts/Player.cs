using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //Take the current postion = new postion(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manger is not Initialised. Still NULL...");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    // Customized method to define player movements and restrictions
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Vector3.right == new Vector3(5,0,0) * 5 * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //  OR ( optimized code) 
        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //  OR ( optimized code)
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Restricting player Bounds - Y axis
        //if(transform.position.y >= 5.7f)
        //{
        //    transform.position = new Vector3(transform.position.x, 5.7f, 0);
        //}
        //else if(transform.position.y <= -3.8)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //}
        // OR (Optimzed code by clamping method)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.7f), 0);

        // Restricting player Bounds - X axis
        if (transform.position.x >= 10.18f)
        {
            transform.position = new Vector3(-10.17f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.17f)
        {
            transform.position = new Vector3(10.17f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            //Debug.Log("Space key is pressed");    
        }


        _canFire = Time.time + _fireRate; // Update _canFire so that the next fire happens at _fireRate
    }

    public void Damage()
    {
        _lives--;  //_lives -= 1;  //_lives = _lives - 1;
        
        if(_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        Debug.Log(_lives + " lives left");
    }

    public void ChangeTripShotActive(bool TripleShotActiveValue)
    {
        _isTripleShotActive = TripleShotActiveValue;
        // Starting CoRoutine for PowerUp
        if (TripleShotActiveValue)
        {
            StartCoroutine(TimeDownPowerUp());
        }
        

    }
    IEnumerator TimeDownPowerUp()
    {
            yield return new WaitForSeconds(5.0f);
            ChangeTripShotActive(false);
            Debug.Log("Triple Shot Powerup De-Activated");

    }
    public void ChangePlayerSpeed(float PlayerSpeedValue)
    {
        _speed = PlayerSpeedValue;
        // Starting CoRoutine for PowerUp
        if (PlayerSpeedValue > 5f)
        {
            StartCoroutine(PlayerSpeedUp());
        }
     }
    IEnumerator PlayerSpeedUp()
    {
        yield return new WaitForSeconds(5.0f);
        ChangePlayerSpeed(5f);
        Debug.Log("Speed Powerup De-Activated");
    }
}

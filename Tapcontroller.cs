using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class Tapcontroller : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public static event PlayerDelegate OnPause;
    public float Speed = 0.0006f;
    public float tiltSmooth = 5;
    public Vector3 startPos;
    private Rigidbody2D rb;
    Quaternion downRotation;
    Quaternion upRotation;
    Quaternion nullRotation;
    public GameObject Player;
    private float ScreenHeight;
    GameManager game;
    void Start()
    {
        ScreenHeight = Screen.height;
        rb = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, 110);
        upRotation = Quaternion.Euler(0,0 , 70);
        nullRotation = Quaternion.Euler(0, 0, 90);
        game = GameManager.Instance;
        

    }
    private void OnEnable()
    {
        GameManager.OnGamePaused += OnGamePaused;
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameManager.OnGamePaused -= OnGamePaused;
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }
    void OnGameStarted()
    {
        rb.simulated = true;
    }
    void OnGamePaused()
    {
        transform.rotation = nullRotation;
        rb.simulated = false;
        rb.velocity = Vector3.zero;
    }
    void OnGameOver()
    {
        rb.simulated = false;
        rb.velocity = Vector3.zero;
        transform.position = startPos;
        transform.rotation = nullRotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (game.GameOver) return;
        
        
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    if (Input.touches[0].position.y > ScreenHeight / 2)
                    {
                        rb.AddForce(Vector2.up * Speed, ForceMode2D.Force);
                        transform.rotation = upRotation;


                    }

                    else
                    {
                        rb.AddForce(Vector2.down * Speed, ForceMode2D.Force);
                        transform.rotation = downRotation;

                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    
                    transform.rotation = nullRotation;
                }
            }

        

        

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
        }
        if (col.gameObject.tag == "DeadZone")
        {

            rb.simulated = false;
            OnPlayerDied();
            
        }
    }
    


}

      

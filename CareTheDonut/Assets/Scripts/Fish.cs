using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] int _jumpForce;
    [SerializeField] Score scoreObj;
    public GameManager gameManager;
    public ObstacleSpawner obstacleSpawner;
    [SerializeField] private AudioSource swim, hit1, hit2, point;
    private bool died;
    int angle;
    int maxAngle = 20;
    int minAngle = -60;
    public Score score;
    bool touchedGround;
    public bool isPaused;
    bool _canScore;
    bool _canDie;

    void Start()
    {
        _canScore = true;
        _canDie = true;
        died = false;
        if (GameManager.isFirstStart == false)
        {
            fishHopOnRestart();
        }
        
    }
    
    void Update()
    {
        if (GameManager.gameStarted == false)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        fishSwim();
    }

    private void FixedUpdate()
    {
        
        fishRotation();
    }

    public void fishSwim()
    {         //(Input.GetKeyDown("w")
        if (Input.touchCount>0 && GameManager.gameOver == false && isPaused == false)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began && touch.position.y < 1200)
            {
                swim.Play();
                if (GameManager.gameStarted == false)
                {
                    _rb.gravityScale = 3f;
                    _rb.velocity = Vector2.zero;
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    obstacleSpawner.InstantiateObstacle();
                    gameManager.GameHasStarted();
                }
                else
                {
                    _rb.velocity = Vector2.zero;
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                }
            }
        }
    }

    void fishHopOnRestart()
    {
        _rb.velocity = Vector2.zero;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        gameManager.GameHasStarted();
    }

    void fishRotation()
    {
        if (_rb.velocity.y > 0)
        {
            if (angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if (_rb.velocity.y < -1.2)
        {
            if (angle > minAngle)
            {
                angle = angle - 2;
            }
        }

        if(touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && _canScore == true)
        {
            _canScore = false;
            score.Scored();
            point.Play();
            StartCoroutine(WaitForScore());
        }
        else if (collision.CompareTag("Column") && GameManager.gameOver == false && _canDie == true)
        {
            //game over
            DiesEffect();
            scoreObj.GameOver();
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                //game over
                DiesEffect();
                scoreObj.GameOver();
                gameManager.GameOver();
                GameOver();
            }
        }
    }
    void DiesEffect()
    {
        if(died == false)
        {
            hit1.Play();
            hit2.Play();
            died = true;
        }
    }

    void GameOver()
    {
        touchedGround = true;
        //transform.rotation = Quaternion.Euler(0, 0, -180);  yere çarpýnca karkakteri ters çeviriyordu ama reklam ile yeniden doðmak için iptal ettim
    }

    public void Rewarded()
    {
        died = false;
        fishHopOnRestart();
        _canDie = false;
        StartCoroutine(WaitForReward());
        touchedGround = false;
    }

    public void OnFirstStart()
    {
        _rb.gravityScale = 0;
    }

    IEnumerator WaitForScore()
    {
        yield return new WaitForSeconds(1.5f);
        _canScore = true;
    }

    IEnumerator WaitForReward()
    {
        yield return new WaitForSeconds(3.5f);
        _canDie = true;
    }




}

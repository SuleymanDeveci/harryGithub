using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;

    [SerializeField] private float maxTime;
    private float timer;

    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    private float randomY;

    void Start()
    {
        if(GameManager.isFirstStart == false)
        {
            InstantiateObstacle();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime && GameManager.gameStarted == true)
            {
                randomY = Random.Range(minY, maxY);
                InstantiateObstacle();
                timer = 0;
            }
        }
        
    }

    public void InstantiateObstacle()
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = new Vector2(transform.position.x, randomY);
        timer = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    BoxCollider2D box;
    float groundWith;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        groundWith = box.size.x * 0.35f;

    }

    void Update()
    {
        if (GameManager.gameOver == false)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        

        if(transform.position.x <= -groundWith)
        {
            transform.position = new Vector2(transform.position.x + 2f * groundWith, transform.position.y);
        }
    }
}

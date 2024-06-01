using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerrb;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private Vector2 move;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    private void movement() 
    {
        x = Input.GetAxis("X");
        y = Input.GetAxis("Y");
        move = new Vector2(x, y);
        move = move.normalized;
        playerrb.velocity = move * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //movement
    [SerializeField] private Rigidbody2D playerrb;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private Vector2 move;
    public float FreeSpeed;
    public float PickUpSpeed;

    //rock mechanics
    public GameObject rock;
    public GameObject rockCarry;
    public bool RockPickedUp=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Rock();
    }
    private void movement() 
    {
        x = Input.GetAxis("X");
        y = Input.GetAxis("Y");
        move = new Vector2(x, y);
        move = move.normalized;
        playerrb.velocity = move * (RockPickedUp==true?PickUpSpeed:FreeSpeed);
    }
    private void Rock() 
    {
        if (RockPickedUp) 
        {
            rock.transform.position=rockCarry.transform.position;
            rock.transform.rotation=rockCarry.transform.rotation;
        }
        if (Input.GetButtonDown("drop")) 
        {
            RockPickedUp = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("rock")) 
        {
            RockPickedUp = true;
        }
    }
}

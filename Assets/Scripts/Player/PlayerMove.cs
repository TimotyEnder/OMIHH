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

    //rock 
    public GameObject rock;
    public Rigidbody2D rockRb;
    public GameObject rockCarry;
    public bool RockPickedUp=false;
    public GameObject rollPosPivot;
    private Vector3 diff;
    public GameObject rockRollPos;
    public float rollForce;
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
        //rock mechanics
        if (RockPickedUp) 
        {
            rock.transform.position=rockCarry.transform.position;
            rock.transform.rotation=rockCarry.transform.rotation;
        }
        if (Input.GetButtonDown("drop")) 
        {
            RockPickedUp = false;
        }
        if (Input.GetButtonDown("Roll")&& RockPickedUp) 
        {
            RockPickedUp = false;
            rock.transform.position = rockRollPos.transform.position;
            rock.transform.rotation = rockRollPos.transform.rotation;

            // Calculate the direction the rock is facing
            Vector2 rollDirection = rockRollPos.transform.right; // Assuming right is the direction the rock faces

            // Apply the force to the rock in the roll direction
            rockRb.velocity = rollDirection * rollForce;

        }
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rollPosPivot.transform.position;
        //normalize difference  
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        rollPosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("rock")) 
        {
            RockPickedUp = true;
        }
    }
}

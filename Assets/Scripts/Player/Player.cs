using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    //pickup delay
    private float timevar;
    public float pickUpDelay;
    //roll delay movement
    private bool rolling;

    //stats
    public float RockBounce;
    public float rollForce;
    public float rollAtkSpeed;
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
        playerrb.velocity = move * (RockPickedUp == true ? PickUpSpeed : FreeSpeed) * (rolling == true ? 0:1) ;
    }
    private void Rock() 
    {
        //rock mechanics
        CircleCollider2D rockColl = rock.GetComponent<CircleCollider2D>();
        rockColl.attachedRigidbody.sharedMaterial.bounciness = RockBounce;
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
            StartCoroutine(RollCoroutine());
            RockPickedUp = false;
            rock.transform.position = rockRollPos.transform.position;

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
        if (collision.gameObject.tag.Equals("rock")&& timevar < Time.time) 
        {
            timevar = Time.time + pickUpDelay;
            RockPickedUp = true;
        }
    }
    private IEnumerator RollCoroutine() 
    {
        rolling = true;
        yield return new WaitForSeconds(rollAtkSpeed);
        rolling=false;
    }
}

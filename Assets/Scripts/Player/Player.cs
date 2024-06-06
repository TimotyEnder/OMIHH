using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    private float pickuptimevar;
    public float pickUpDelay;
    //roll delay movement
    private bool rolling;
    //roll charging
    private float rollchargetimevar;
    [SerializeField] private float rollForce;
    //charge bar
    [SerializeField] private Slider chargebar;
    [SerializeField] private GameObject chargeBarGO;

    //stats
    public float RockBounce;
    public float rollForceMax;
    public float rollAtkSpeed;
    public float rollChargeSpeed;
    public float dashSpeed;
    public float dashCoolDown;
    // rock dash
    public bool ethereal;
    public SpriteRenderer sprite;
    private float dashtimevar;
    private Vector2 distance;
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Rock();
        Dash();
    }
    private void movement() 
    {
        x = Input.GetAxis("X");
        y = Input.GetAxis("Y");
        move = new Vector2(x, y);
        move = move.normalized;
        playerrb.velocity = move * (RockPickedUp == true ? PickUpSpeed : FreeSpeed) * (rolling ? 0.2f:1) * (ethereal?0:1);
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
        if(Input.GetButton("Roll") && RockPickedUp) 
        {
            chargebar.gameObject.SetActive(true);
            chargebar.value = rollForce / rollForceMax;
            rolling = true;
            if (rollForce < rollForceMax && rollchargetimevar < Time.time) 
            {
                rollchargetimevar = Time.time + rollChargeSpeed;
                rollForce++;
            }
        }
        if (Input.GetButtonUp("Roll")&& RockPickedUp) 
        {
            chargebar.gameObject.SetActive(false);
            rolling =false;
            RockPickedUp = false;
            rock.transform.position = rockRollPos.transform.position;

            // Calculate the direction the rock is facing
            Vector2 rollDirection = rockRollPos.transform.right; // Assuming right is the direction the rock faces

            // Apply the force to the rock in the roll direction
            rockRb.velocity = rollDirection * rollForce;
            rollForce = 0;

        }
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rollPosPivot.transform.position;
        //normalize difference  
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        rollPosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

    }
    private void Dash() 
    {
        if (ethereal) 
        {
            distance = this.transform.position - rock.transform.position;
            float step=dashSpeed * Time.deltaTime * distance.magnitude;
            playerrb.MovePosition(Vector2.MoveTowards(this.transform.position, rock.transform.position, step)); 
            sprite.color = Color.blue;
        }
        else 
        {
            sprite.color = Color.white;
        }
        if (Input.GetButtonDown("Dash") && !RockPickedUp && dashtimevar<Time.time) 
        {
            dashtimevar = Time.time + dashCoolDown;
            ethereal = true;
        }
        if (RockPickedUp) 
        {
            ethereal = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("rock")&& pickuptimevar < Time.time) 
        {
            pickuptimevar = Time.time + pickUpDelay;
            RockPickedUp = true;
        }
    }
}

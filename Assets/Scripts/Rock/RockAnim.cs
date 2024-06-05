using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAnim : MonoBehaviour
{
    public Animator rockanim;
    public SpriteRenderer rockSpr;
    public Rigidbody2D rockRb;
    public Player player;
    public float rockAnimSpeed;
    void Update()
    {
        if (rockRb.velocity.x < 0 && !player.RockPickedUp) 
        {
            rockSpr.flipX = true;
        }
        else if(rockRb.velocity.x > 0 && !player.RockPickedUp)
        {
            rockSpr.flipX = false;
        }
        if (rockRb.velocity.y < 0 && !player.RockPickedUp)
        {
            rockSpr.flipY = true;
        }
        else if (rockRb.velocity.y > 0 && !player.RockPickedUp)
        {
            rockSpr.flipY = false;
        }
        if (rockRb.velocity.magnitude > 0.1f && !player.RockPickedUp) 
        {
            if(rockRb.velocity.x * rockRb.velocity.x > rockRb.velocity.y * rockRb.velocity.y) 
            {
                rockanim.SetBool("SIDEROLL", true);
            }
            else 
            {
                rockanim.SetBool("ROLLFOR", true);
            }
        }
        else 
        {
            rockanim.SetBool("SIDEROLL", false);
            rockanim.SetBool("ROLLFOR", false);
        }
        rockanim.speed = rockRb.velocity.magnitude*(1/rockAnimSpeed);
    }
}

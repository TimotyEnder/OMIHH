using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAnim : MonoBehaviour
{
    public Animator rockanim;
    public SpriteRenderer rockSpr;
    public Rigidbody2D rockRb;
    public Player player;
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
        if (rockRb.velocity.magnitude > 0.1f && !player.RockPickedUp) 
        {
            rockanim.SetBool("SIDEROLL", true);
        }
        else 
        {
            rockanim.SetBool("SIDEROLL", false);
        }
    }
}

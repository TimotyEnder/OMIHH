using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj: MonoBehaviour
{
    public int Damage;
    public bool DestOnTouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DestOnTouch && !collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("rock")) 
        {
           Destroy(this.gameObject);
        }
    }
}

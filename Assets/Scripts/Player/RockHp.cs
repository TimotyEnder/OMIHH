using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHp : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;
    public Player playerscript;
    public SpriteRenderer rockspr;
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthVisuals();
    }
   public void HealthVisuals() 
    {
        if (curHealth <= 0)
        {
            rockspr.color = Color.gray;
            int Ephlayer = LayerMask.NameToLayer("Ephemeral");
            this.gameObject.layer = Ephlayer;
        }
        else 
        {
            rockspr.color = Color.white;
            int DefaultLay = LayerMask.NameToLayer("Default");
            this.gameObject.layer = DefaultLay;
        }
    }
    public void Damage(int damage)
    {
        curHealth -= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("proj")) 
        {
            Proj projsc= collision.gameObject.GetComponent<Proj>();
            Damage(projsc.Damage);
            Destroy(collision.gameObject);
        }
    }
}

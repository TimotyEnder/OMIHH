using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 4;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteFull;
    public Sprite spriteHigh;
    public Player playerscript;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < 0)
        {
            this.gameObject.SetActive(false);
        }
       // ChangeSprite(); work on player damage script
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
    }

    public void Heal(int healing){
        curHealth += healing;
    }

    private void ChangeSprite(){
        Sprite sprite = spriteFull;
        //This has to be changed when we decide on the hp
        if(curHealth < maxHealth) sprite = spriteHigh;

        spriteRenderer.sprite = sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("proj"))
        {
            if (!playerscript.ethereal)
            {
                Proj proj = collision.gameObject.GetComponent<Proj>();
                Damage(proj.Damage);
            }
            Destroy(collision.gameObject);
        }
    }
}

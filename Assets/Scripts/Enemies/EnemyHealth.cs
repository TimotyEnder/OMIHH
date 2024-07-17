using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;
    public Player playerscript;
    public int SquishedTime;
    BoxCollider2D enemcol;
    SpriteRenderer enemspr;
    public RangedEnemyLogic enemyRangedscript;
    private bool squishing;
    private Rigidbody2D enemrb;
    // Start is called before the first frame update
    void Start()
    {
        squishing = true;
        curHealth = maxHealth;
        enemcol = this.gameObject.GetComponent<BoxCollider2D>();
        enemspr= this.gameObject.GetComponent<SpriteRenderer>();
        enemyRangedscript= this.gameObject.GetComponent<RangedEnemyLogic>();
        enemrb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    public void Damage(float damage)
    {
        curHealth -= damage;
    }

    public void Heal(float healing)
    {
        curHealth += healing;
    }
    // Update is called once per frame
    void Update()
    {
        if (curHealth < 0) 
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("rock")) 
        {
            Rigidbody2D rockrb= collision.gameObject.GetComponent<Rigidbody2D>();
            RockDmg dmg = collision.gameObject.GetComponent<RockDmg>();
           if(rockrb.velocity.magnitude>1f)
           { 
                Damage(dmg.Damage()*(rockrb.velocity.magnitude / playerscript.rollForceMax));
                StartCoroutine(SquishedCoroutine());
           }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        squishing = true;
    }
    private IEnumerator SquishedCoroutine() 
    {
       if(squishing)
        {
            squishing = false;
            enemcol.enabled = false;
            enemspr.color = Color.grey;
            enemyRangedscript.enabled = false;
            enemrb.drag = 99;
            yield return new WaitForSeconds(SquishedTime);
            enemrb.drag = 0;
            enemspr.color = Color.white;
            enemcol.enabled = true;
            enemyRangedscript.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHp : MonoBehaviour
{
    [SerializeField] private int curHealth = 0;
    [SerializeField] private int maxHealth = 4;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteFull;
    public Sprite spriteHigh;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        changeSprite();
    }

    public void DamageRock(int damage)
    {
        curHealth -= damage;
    }

    public void HealRock(int healing){
        curHealth += healing;
    }

    private void changeSprite(){
        Sprite sprite = spriteFull;
        //This has to be changed when we decide on the hp
        if(curHealth < maxHealth) sprite = spriteHigh;

        spriteRenderer.sprite = sprite;
    }
}

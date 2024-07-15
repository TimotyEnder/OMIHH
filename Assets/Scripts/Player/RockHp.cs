using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHp : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;
    public Player playerscript;
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(int damage)
    {
        curHealth -= damage;
    }
}

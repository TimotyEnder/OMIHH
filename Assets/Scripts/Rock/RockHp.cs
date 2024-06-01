using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHp : MonoBehaviour
{
    [SerializeField] private int curHealth = 0;
    [SerializeField] private int maxHealth = 4;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //DamageRock(1);
    }

    public void DamageRock(int damage)
    {
        curHealth -= damage;
    }

    public void HealRock(int healing){
        curHealth += healing;
    }
}

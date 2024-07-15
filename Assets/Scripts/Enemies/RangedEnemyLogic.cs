using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyLogic : MonoBehaviour
{
    [SerializeField] private GameObject projectilePre;
    private float ShootTimeVar;
    private int enemyDestTime;
    [SerializeField] private GameObject shootPos;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] Rigidbody2D enemRb;
    [SerializeField] private float distance;
    //enemy  stats
    public float distMax;
    public float distMin;
    public float speed;
    public float ProjSpeed;
    public float ShootCD;

    // Start is called before the first frame update
    void Start()
    {
        enemyDestTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot(); 
        AdjustPosition();
    }

    private void AdjustPosition(){
        Vector3 diff = player.transform.position - enemy.transform.position;
         Vector3 diffNorm= diff.normalized;  
        distance = diff.magnitude;
        if (distance < distMin)
        {
            enemRb.velocity = diffNorm * -1f * speed;
        }
        else if(distance > distMin && distance < distMax)
        {
            enemRb.velocity = diffNorm * 0f;
        }
        else if(distance > distMax)
        {
            enemRb.velocity = diffNorm * speed;
        }
    }
    private void Shoot() 
    {
        if (Time.time>ShootTimeVar && enemRb.velocity.magnitude<0.1f)
        {
            ShootTimeVar = Time.time + ShootCD;
            GameObject projectile = Instantiate(projectilePre, shootPos.transform.position, Quaternion.identity);
            StartCoroutine(projDestroyCoroutine(projectile));
            Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
            ProjDamage damage = body.GetComponent<ProjDamage>();
            damage.Damage = 1;
            body.velocity = shootPos.transform.right * ProjSpeed;
        }
    }

    private IEnumerator projDestroyCoroutine(GameObject target) 
    {
        yield return new WaitForSeconds(enemyDestTime);
        Destroy(target);
    }
}
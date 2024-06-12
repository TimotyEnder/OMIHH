using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePre;
    public int delay;
    private float speed;
    private int enemyDestTime;
    [SerializeField] private GameObject shootPos;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0;
        speed = 3f;
        enemyDestTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        delay++;
        if(delay % 50 == 0){
            GameObject projectile = Instantiate(projectilePre, shootPos.transform.position, Quaternion.identity);
            StartCoroutine(projDestroyCoroutine(projectile));
            Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
            body.velocity = shootPos.transform.right * speed;
        }

        AdjustPosition();
    }

    private void AdjustPosition(){
        Vector3 diff = player.transform.position - enemy.transform.position;
        Rigidbody2D r = enemy.GetComponent<Rigidbody2D>();
        float distance = diff.sqrMagnitude;
        if(distance < 10f){
            r.velocity = diff * -1f;
        }
        else if(distance > 10.5f && distance < 13f){
            r.velocity = diff * 0f;
        }
        else if(distance > 13f){
            r.velocity = diff;
        }
    }

    private IEnumerator projDestroyCoroutine(GameObject target) 
    {
        yield return new WaitForSeconds(enemyDestTime);
        Destroy(target);
    }
}
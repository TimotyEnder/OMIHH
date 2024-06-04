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
    }
    private IEnumerator projDestroyCoroutine(GameObject target) 
    {
        yield return new WaitForSeconds(enemyDestTime);
        Destroy(target);
    }
}
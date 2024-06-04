using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePre;
    public int delay;
    private float speed;
    [SerializeField] private GameObject shootPos;
    // Start is called before the first frame update
    void Start()
    {
        delay = 0;
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        delay++;
        if(delay % 50 == 0){
            GameObject projectile = Instantiate(projectilePre, shootPos.transform.position, Quaternion.identity);
            Rigidbody2D body = projectile.GetComponent<Rigidbody2D>();
            body.velocity = shootPos.transform.right * speed;
        }
    }
}

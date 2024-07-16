using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayerFollow : MonoBehaviour
{
    public GameObject player;
    public float CamSpeedMult;
    private float CamSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        CamSpeed = CamSpeedMult * Mathf.Abs((this.transform.position - targetPos).magnitude) * Mathf.Abs((this.transform.position - targetPos).magnitude);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, CamSpeed* Time.deltaTime);
    }
}

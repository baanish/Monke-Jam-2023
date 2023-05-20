using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    [SerializeField]
    public LayerMask punchLayer;
    [SerializeField]
    public float aliveTime = 0.2f;

    [SerializeField]
    public float launchSpeed = 50f;
    // on entering a trigger with layer mask punchLayer (punch plane) launch them and destroy the person after aliveTime
    void OnTriggerEnter(Collider other)
    {
        if (punchLayer == (punchLayer | (1 << other.gameObject.layer)))
        {
            // launch the person
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(launchSpeed*2, launchSpeed*0.5f, 0);
            // destroy the person after aliveTime
            Destroy(gameObject, aliveTime);
        }
    }
}

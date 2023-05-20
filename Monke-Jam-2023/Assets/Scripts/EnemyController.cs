using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public LayerMask punchLayer;
    void OnTriggerEnter(Collider other)
    {
        if (punchLayer == (punchLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}

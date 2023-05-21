using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public Vector3 offset = new Vector3(0, 3, -12);

    // Update is called once per frame
    void Update()
    {
        // track the player
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
    }
}

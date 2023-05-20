using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMover : MonoBehaviour
{
    [SerializeField]
    public float movementScale = 1e-03f;

    [SerializeField]
    public float movementSpeed = 3f;
    // Update is called once per frame
    void Update()
    {
        // Make the banana move up and down in a sine wave pattern
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * movementSpeed) * movementScale), transform.position.z);
    }
}

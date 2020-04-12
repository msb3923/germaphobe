using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Joe").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //current camera's position
        Vector3 temp = transform.position;

        //set camera's x position to player's x position
        temp.x = playerTransform.position.x;

        //set camera's position to temp
        transform.position = temp;
    }
}

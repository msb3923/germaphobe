using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fighter : MonoBehaviour
{
    public Transform mytransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            mytransform.position = new Vector3(mytransform.position.x,mytransform.position.y+.05f,mytransform.position.z);
        }
        if (Input.GetKey("s"))
        {
            mytransform.position = new Vector3(mytransform.position.x, mytransform.position.y - .05f, mytransform.position.z);
        }
        if (Input.GetKey("a"))
        {
            mytransform.position = new Vector3(mytransform.position.x-.05f, mytransform.position.y, mytransform.position.z);
        }
        if (Input.GetKey("d"))
        {
            mytransform.position = new Vector3(mytransform.position.x + .05f, mytransform.position.y, mytransform.position.z);
        }

        if (Vector2.Distance(mytransform.position,GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position) < .5)
        {
            SceneManager.LoadScene(0);
        }


    }
}

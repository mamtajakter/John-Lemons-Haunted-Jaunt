using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	float h = Input.GetAxis ("Horizontal") * speed;
        float v = Input.GetAxis ("Vertical") * speed;

        // transform.Translate = (new Vector3(h, 0.0f, v));
        transform.localScale += (new Vector3(h, 0.0f, v));

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlying : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("plane the Gabbiano added to:" + gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10.0f;

        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    void ProcessUpdate () {
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up);
        }else if (Input.GetKey(KeyCode.A)){
            print("Left Pressed");
        }else if (Input.GetKey(KeyCode.D)){
            print("Right pressed.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessUpdate();
    }
}

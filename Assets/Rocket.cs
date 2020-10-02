using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Rotate () {
        float rotationSpeed = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = false;
        if (Input.GetKey(KeyCode.A)){
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D)){
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
    }

    void OnCollisionEnter(Collision collision) {
        print("collided");
    }

    void Thrust () {
        float thrustThisFrame = thrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audioSource.isPlaying){
                audioSource.Play();
            }  
        }else{
                audioSource.Stop();
            }
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
}

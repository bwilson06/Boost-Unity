using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] int level = 0;

    [SerializeField] float gravityMultiplier = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    public enum State { Alive, Dying, Transcending }

    public State state = State.Alive;


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
        if (state != State.Alive){
            return ;
        }
        switch (collision.gameObject.tag){
            case "Friendly":
                // do nothing
                break;
            case "Win":
            audioSource.PlayOneShot(success);
            successParticles.Play();
            state =  State.Transcending;
            Invoke("LoadNextScene", 1f);
                break;
            default:
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticles.Play();
            state = State.Dying;
            Invoke("LoadFirstScene", .6f);
                break;
        }

    }

    void LoadNextScene () {
        level = level + 1;
        if (level < 2){
        SceneManager.LoadScene(level);
        }else{
        SceneManager.LoadScene(1); 
        }
    }

    void LoadFirstScene () {
        SceneManager.LoadScene(0);
    }

    void Thrust () {
        rigidBody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
                thrustParticles.Play();
            }  
        }else{
                audioSource.Stop();
                thrustParticles.Stop();
            }
    }

    void deBug() {
        if (Input.GetKey(KeyCode.L)){
            if (level == 0){
                level = level + 1;
                SceneManager.LoadScene(level);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive){
        Thrust();
        Rotate();
        deBug();
        }
    }
}

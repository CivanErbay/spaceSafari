using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;

    float currentSpeed = 0f;
    float maxSpeed = 100f;
    bool launched = false;
    public float movementSpeed = 15.0f;
    public float accelerationTime = 60;     
    private float minSpeed ;
    private float time ;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        minSpeed = currentSpeed; 
        time = 0 ;
    }

    private void FixedUpdate() {
        body.AddForce(Physics.gravity*body.mass);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(launched == true) {
        anim.Play("ExplosionAnim");
        Destroy(gameObject, 0.5f);
        }
    }
    
    private void Update() {
        float verticalInput = Input.GetAxisRaw("Vertical");
    
       currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime );
  
        time += Time.deltaTime ;
       // Debug.Log("currentSpeed = " + currentSpeed);
      //  Debug.Log("Player Posi y Axis = " + body.transform.position.y);

        if (verticalInput == 1) {
            body.velocity = new Vector2(body.velocity.x, verticalInput * currentSpeed ); 
        } 
        
        if (verticalInput == 1 && body.transform.position.y > 100) {
            launched = true;
        }


       /*  if(Input.GetKey(KeyCode.Space)) {
            body.velocity = new Vector2(body.velocity.x, speed);
            launch = true;
            } */      
    }
}

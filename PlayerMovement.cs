using UnityEngine;
using System;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    bool launch = false;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        float horizontalInput = Input.GetAxis("Vertical");
      
        body.velocity = new Vector2(body.velocity.x, horizontalInput * speed);


         if(Input.GetKey(KeyCode.Space)) {
            body.velocity = new Vector2(body.velocity.x, speed);
            launch = true;
            }
       
         Debug.Log("launch = " + launch);
        //Turn Rocket
        if(horizontalInput < 0.01f && launch ) {
            Debug.Log(launch);
            transform.localScale = new Vector2(-1, -1);
            }

       
    }
}

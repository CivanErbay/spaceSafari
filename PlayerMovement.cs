using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    public GameObject rocket;
    public GameObject floor;

    float currentSpeed = 0f;
    float maxSpeed = 100f;
    bool launched = false;
    public float movementSpeed = 15.0f;
    public float accelerationTime = 60;     
    private float minSpeed ;
    private float time ;
    private float distanceHeight;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rocket = GameObject.Find("Player");
        floor = GameObject.Find("Ground");
        minSpeed = currentSpeed; 
        time = 0 ;
    }

    private void Start() {
         PlayerPrefs.SetInt ("PlayerHeight", 0);
    }

    private void FixedUpdate() {
        body.AddForce(Physics.gravity*body.mass);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(launched == true) {
        anim.Play("ExplosionAnim");
        Destroy(gameObject, 4.5f);
        }
    }
    
    private void Update() {
        float verticalInput = Input.GetAxisRaw("Vertical");
        time += Time.deltaTime ;


        if (verticalInput == 1) {
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, accelerationTime / time );
            body.velocity = new Vector2(body.velocity.x, verticalInput * currentSpeed  ); 
            anim.Play("accelerationAnim");
        } else if (verticalInput == 0) {
            time = 0;
            currentSpeed = 0f;
            anim.Play("IdleAnim");
        }
   
        if (verticalInput == 1 && body.transform.position.y > 100) {
            launched = true;
        }     

        if  (rocket && floor) {
            Vector3 delta = rocket.transform.position - floor.transform.position;
            distanceHeight = delta.y;
        }

        Debug.Log(body.velocity.y);

        // Set current height and velocity to display them on screen
        PlayerPrefs.SetInt("PlayerHeight", (int) distanceHeight);
        PlayerPrefs.SetInt("PlayerVelocity", (int) currentSpeed);
    }
}

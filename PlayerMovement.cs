using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D body;
    private Animator anim;
    public GameObject rocket;
    public GameObject floor;

    float currentSpeed = 0.01f;
    float maxSpeed = 100f;
    public bool launched = false;
    public float movementSpeed = 15.0f;
    public float accelerationTime = 60;
    private float minSpeed;
    private float time;
    private float distanceHeight;

    //Custom Functions
    private void handleVertical(
        float verticalInput,
        float currentSpeed,
        float maxSpeed,
        Rigidbody2D body,
        Animator anim
    )
    {
        if (verticalInput == 1)
        {
            if (currentSpeed <= maxSpeed)
            {
                this.currentSpeed = currentSpeed + 0.1f;
            }

            Debug.Log(currentSpeed);

            body.velocity = new Vector2(body.velocity.x, verticalInput * currentSpeed);
            anim.Play("accelerationAnim");
        }
        else if (verticalInput == 0)
        {
            if (currentSpeed >= 0)
            {
                this.currentSpeed = currentSpeed - 0.1f;
            }
            anim.Play("IdleAnim");
        }
        if (verticalInput == 1 && body.transform.position.y > 100)
        {
            launched = true;
        }
    }

    private void handleHeight(GameObject rocket, GameObject floor)
    {
        if (rocket && floor)
        {
            Vector3 distanceVector = rocket.transform.position - floor.transform.position;
            this.distanceHeight = distanceVector.y;
        }
    }

    //BuiltIn-Functions
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rocket = GameObject.Find("Player");
        floor = GameObject.Find("Ground");
        minSpeed = currentSpeed;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("PlayerHeight", 0);
    }

    private void FixedUpdate()
    {
        body.AddForce(Physics.gravity * body.mass);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (launched == true)
        {
            anim.Play("ExplosionAnim");
            Destroy(gameObject, 4.5f);
        }
    }

    private void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //time += Time.deltaTime ;
        handleVertical(verticalInput, currentSpeed, maxSpeed, body, anim);
        handleHeight(rocket, floor);

        // Set current height and velocity to display them on screen
        PlayerPrefs.SetInt("PlayerHeight", (int)distanceHeight);
        PlayerPrefs.SetInt("PlayerVelocity", (int)currentSpeed);
    }
}

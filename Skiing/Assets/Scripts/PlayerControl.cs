using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    
    private InputAction move;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Vector3 obstacleKnockback;
    
    private bool canMove = true;
    
    Rigidbody rb;
    
    private void OnEnable()
    {
        Obstacle.OnObstacleHit += OnCollision;
    }
    
    private void OnDisable()
    {
        Obstacle.OnObstacleHit -= OnCollision;
    }

    private void OnCollision()
    {
        Debug.Log("Hit stone");
        rb.AddForce(obstacleKnockback, ForceMode.Impulse);
        canMove = false;
        Invoke("AllowMove", 2);
    }

    private void AllowMove()
    {
        canMove = true;
    }


    private void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove) return;
        bool isGrounded = Physics.Linecast(transform.position, transform.position -Vector3.up, ground);
        Debug.DrawLine(transform.position, transform.position + Vector3.up, isGrounded ? Color.red : Color.blue);
        if (isGrounded)
        {
            Vector2 moveVector = move. ReadValue<Vector2>();
            Debug.Log("move x:" + moveVector.x + "move y:" + moveVector.y);
            float rotateSpeed= -moveVector.x* turnSpeed* Time.fixedDeltaTime; 
            rb.AddTorque(new Vector3(0, rotateSpeed, 0));
        }
        
        float speedMultiplier = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
        rb.AddForce(transform.forward * (speed * speedMultiplier * Time.fixedDeltaTime));
    }
}

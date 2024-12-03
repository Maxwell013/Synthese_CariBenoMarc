using UnityEngine;

public class Player : Entity
{
    [Header("Player")]
    [SerializeField] private Vector2 m_initalPosition = default;
    [SerializeField] private float m_speed = default;
    [SerializeField] private float m_fireRate = 0.5f;
    [SerializeField] private float m_angularSpeed = default;

    
    private float m_fireCooldown = -1.0f; // Changer avec animation
    private Rigidbody2D m_rb = default;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        transform.position = m_initalPosition; // a modifier pour spawn player
    }

    private void Update()
    {

        if (Input.GetButton("Fire1") && Time.time > m_fireCooldown)
        {
            Fire();
        }

        // Dash();
    }

     private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

   
    private void Fire()
    {
        m_fireCooldown = Time.time + m_fireRate;
        // Instantiate(_laserPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity); // Changer nom prefab en fonction du nom
    }
    
    private void Movement()
    {
        float positionX = Input.GetAxis("Horizontal");
        float positionY = Input.GetAxis("Vertical");

        Vector2 direction = new(positionX, positionY);

        m_rb.velocity = m_speed * Time.fixedDeltaTime * direction.normalized;
    }

    private void Dash()
    {
       //TODO
    }

    private void Rotation()
    {
        Quaternion target = Quaternion.LookRotation(Vector3.forward, m_rb.velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_angularSpeed * Time.fixedDeltaTime);
    }
}

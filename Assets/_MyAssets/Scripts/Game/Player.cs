using UnityEngine;

public class Player : Entity
{
    [Header("Player")]
    [SerializeField] private Vector2 m_initalPosition = default;
    [SerializeField] private float m_speed = default;
    [SerializeField] private float m_fireRate = 0.5f;
    [SerializeField] private float m_angularSpeed = default;
    [SerializeField] private GameObject m_projectilePrefab = default;


    private float m_fireCooldown = -1.0f; // Changer avec animation
    private Rigidbody2D m_rb = default;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        //gameObject.SetActive(false);
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
        Instantiate(m_projectilePrefab, (transform.position + (Vector3) m_rb.velocity.normalized * 1.5f), transform.rotation); //fix position spawn bullet
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
        // if (m_rb.velocity.magnitude >= 0.1)
        {
            Quaternion target = Quaternion.LookRotation(Vector3.forward, m_rb.velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_angularSpeed * Time.fixedDeltaTime);
        }
    }

    public void Spawn()
    {
        transform.position = m_initalPosition;
        gameObject.SetActive(false);
        
    }
}

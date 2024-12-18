using UnityEngine;

public class Player : Entity
{
    [Header("Player")]
    [SerializeField] private Vector2 m_initalPosition = default;
    [SerializeField] private float m_speed = default;
    [SerializeField] private float m_fireRate = 0.5f;
    [SerializeField] private float m_angularSpeed = default;
    [SerializeField] private GameObject m_projectilePrefab = default;

    [Header("Sound")]
    [SerializeField] private AudioSource m_shootSound = default;


    private float m_fireCooldown = -1.0f; // Changer avec animation
    private bool m_enabled = false;
    private Rigidbody2D m_rb = default;
    private Animator m_animator = default;

        private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    new protected void Update()
    {

        if (Input.GetButton("Fire1") && Time.time > m_fireCooldown)
        {
            m_shootSound.Play();
            Fire();
        }

        // Dash();
    }

    private void FixedUpdate()
    {
        if (m_enabled)
        {
            Movement();
            Rotation();
        }
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

        if(Mathf.Abs(m_rb.velocity.magnitude) >= 0.02f)
        {
            m_animator.SetBool("isMoving", true);
        }
        else
        {
            m_animator.SetBool("isMoving", false);
        }
    }

    private void Dash()
    {
        //TODO
    }

    private void Rotation()
    {
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

    // Set methods
    public void Enable() { m_enabled = true; }
}

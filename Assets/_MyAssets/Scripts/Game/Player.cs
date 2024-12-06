using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : Entity
{
    [Header("Player")]
    [SerializeField] private Vector2 m_initalPosition = default;
    [SerializeField] private float m_speed = default;
    [SerializeField] private float m_angularSpeed = default;
    [SerializeField] private float m_dashSpeed = default;
    [SerializeField] private float m_dashDuration = default;
    [SerializeField] private float m_fireRate = default;
    [SerializeField] private float m_dashRate = default;
    [SerializeField] private float m_burstFireCount = default;
    [SerializeField] private GameObject m_tmp = default;


    private float m_fireCooldown = -1.0f; // Changer avec animation
    private float m_dashCooldown = -1.0f; // Changer avec animation
    private Rigidbody2D m_rb = default;
    private bool m_isDashing = false;
    private bool m_canBurst = true;


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
            if (m_canBurst) { Burst(); }
            else { Fire(); }
        }
        if (Input.GetButton("Fire2") && Time.time > m_dashCooldown)
        {
            Dash();
        }
        
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

        Vector2 direction;
        float speed;

        if (m_isDashing)
        {
            direction = transform.up;
            speed = m_dashSpeed;
        } 
        else
        {
            float positionX = Input.GetAxis("Horizontal");
            float positionY = Input.GetAxis("Vertical");
            
            direction = new(positionX, positionY);
            speed = m_speed;
        }
        m_rb.velocity = speed * Time.fixedDeltaTime * direction.normalized;
    }

    private void Dash()
    {
        m_dashCooldown = Time.time + m_dashRate;

        StartCoroutine(DashCoroutine());
    }

    private void Rotation()
    {
        if (m_rb.velocity.sqrMagnitude > 0.0f)
        {
            Quaternion target = Quaternion.LookRotation(Vector3.forward, m_rb.velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_angularSpeed * Time.fixedDeltaTime);
        }
        
    }

    public IEnumerator DashCoroutine()
    {
        m_isDashing = true;

        yield return new WaitForSeconds(m_dashDuration);

        m_isDashing = false;
    }

    private void Burst()
    {
        m_canBurst = false; 
        m_fireCooldown = Time.time + m_fireRate;

        float gap = 360.0f / m_burstFireCount;

        for (float angle = 0.0f; angle < 360.0f; angle += gap)
        {
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle);

            Instantiate(m_tmp, transform.position + rotation * Vector2.up, rotation);
        }
    }
}

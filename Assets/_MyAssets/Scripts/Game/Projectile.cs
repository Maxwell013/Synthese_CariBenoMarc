using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : Entity
{
    [Header("Projectile")]
    [SerializeField] private float m_speed;
    
    private Rigidbody2D m_rb;
    protected void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = transform.rotation * Vector2.up; // tmp
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Inital rotation is handeled by the enemy spawner
        Vector3 direction = m_rb.velocity.normalized;

        m_rb.velocity = m_speed * Time.fixedDeltaTime * direction;
    }
}
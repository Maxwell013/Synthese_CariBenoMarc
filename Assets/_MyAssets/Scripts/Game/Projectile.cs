using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : Entity
{
    [Header("Projectile")]
    [SerializeField] private float m_speed = default;
    [SerializeField] private float m_angularSpeed = default;

    private Rigidbody2D m_rb = default;
    private Animator m_animator = default;

    protected void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_rb.velocity = transform.rotation * Vector2.up; // tmp
        m_animator.Play("BulletMove_anim");
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }


    private void Movement()
    {
        Vector3 direction = m_rb.velocity.normalized;

        m_rb.velocity = m_speed * Time.fixedDeltaTime * direction;
    }

    private void Rotation()
    {
        Quaternion target = Quaternion.LookRotation(Vector3.forward, m_rb.velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, m_angularSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D p_collision)
    {
        Destroy(gameObject);
    }
}
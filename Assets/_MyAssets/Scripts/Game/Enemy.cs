using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D m_rb;
    private float m_speed;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        // m_speed = GameManager.Instance.GetSomething();
        m_speed = 100.0f; // tmp
    }

    private void Movement()
    {
        // Inital rotation is handeled by the enemy spawner
        Vector3 direction = transform.rotation * Vector3.up;

        m_rb.AddForce(m_speed * Time.fixedDeltaTime * direction.normalized);
    }
}

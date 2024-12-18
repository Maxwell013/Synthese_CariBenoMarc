using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D m_rb = default;
    private float m_speed = default;

    protected void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_speed = GameManager.Instance.GetEnemySpeed();
        m_rb.velocity = 2.0f * Vector2.left;
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

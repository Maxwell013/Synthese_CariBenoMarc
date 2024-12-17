using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D m_rb = default;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = transform.rotation * Vector2.left;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Inital rotation is handeled by the enemy spawner
        Vector3 direction = m_rb.velocity.normalized;
        m_rb.velocity = GameManager.Instance.GetEnemySpeed() * Time.fixedDeltaTime * direction;
    }
}

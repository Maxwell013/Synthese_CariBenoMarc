using UnityEngine;

public class Enemy : Entity
{
    private Rigidbody2D m_rb = default;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = GameObject.Find("Player").transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = m_rb.velocity.normalized;
        m_rb.velocity = GameManager.Instance.GetEnemySpeed() * Time.fixedDeltaTime * direction;
    }
}

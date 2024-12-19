using UnityEngine;

public class Enemy : Entity
{
    [Header("Enemy")]
    [SerializeField] private int m_points = default;

    private Rigidbody2D m_rb = default;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        if (player == null) {m_rb.velocity = new Vector3(0.0f, 0.0f) - transform.position; }
        else { m_rb.velocity = player.transform.position - transform.position; }
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

    private void OnTriggerEnter2D(Collider2D p_collider)
    {
        // Disable collisions while dammage is processed
        GetComponent<Collider2D>().enabled = false;

        if (!p_collider.CompareTag("SpawnTester"))
            Dammage(p_collider.CompareTag("PlayerBullet"));

        GetComponent<Collider2D>().enabled = true;
    }

    override protected void Kill ()
    {
        GameManager.Instance.IncrementPoints(m_points);
    }
}

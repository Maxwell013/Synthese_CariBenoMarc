using System.Collections;
using System;
using UnityEngine;
using Unity.Burst;
using Unity.VisualScripting;

public class LaserMine : Enemy
{
    [Header("LaserMine")]
    [SerializeField] private bool m_isLarge = default;
    [SerializeField] private GameObject m_lasers = default;
    [SerializeField] private float m_laserRate = default;
    [SerializeField] private float m_laserDuration = default;
    [SerializeField] private Sprite m_defaultSprite = default;

    private float m_laserCooldown;

    private const float m_maxX = 9.0f;
    private const float m_maxY = 5.0f;

    private Animator m_animator = default;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        if (m_laserRate <= m_laserDuration + 1.3f)
            Debug.Log("Warning laser rates will not work!");

        m_laserCooldown = Time.time + 0.1f; // + 0.5f * m_laserRate; tmp

        m_lasers.SetActive(false);

        GetComponent<Rigidbody2D>().angularVelocity = 20.0f;
    }

    new protected void Update()
    {
        base.Update();
        float x = transform.position.x;
        float y = transform.position.y;
        if (Time.time > m_laserCooldown && MathF.Abs(x) <= m_maxX && MathF.Abs(y) <= m_maxY)
        FireLasers();
    }

    private void FireLasers()
    {
        m_laserCooldown = Time.time + m_laserRate;
        StartCoroutine(LaserCoroutine());
    }

    private IEnumerator LaserCoroutine()
    {
        m_animator.enabled = true;

        String animName = (m_isLarge) ? "LaserMineLarge_anim" : "LaserMineSmall_anim";
        m_animator.Play(animName, -1, 0.0f);

        yield return new WaitForSeconds(1.3f);

        m_lasers.gameObject.SetActive(true);

        yield return new WaitForSeconds(m_laserDuration);

        m_animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = m_defaultSprite;

        m_lasers.gameObject.SetActive(false);
    }

    override protected void Kill()
    {
        base.Kill();
        GameObject.Find("Player").GetComponent<Player>().CanBurst();
    }
}

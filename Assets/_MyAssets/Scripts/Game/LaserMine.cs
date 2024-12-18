using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Burst;
using Unity.VisualScripting;

public class LaserMine : Enemy
{
    [Header("LaserMine")]
    [SerializeField] private List<GameObject> m_lasers = default; // List of laser groups
    [SerializeField] private float m_laserRate = default;
    [SerializeField] private float m_laserDuration = default;

    private float m_laserCooldown;

    private const float m_maxX = 9.0f;
    private const float m_maxY = 5.0f;

    new protected void Awake()
    {
        base.Awake();

        m_laserCooldown = Time.time + 1.5f * m_laserRate;

        foreach (GameObject group in m_lasers)
            group.SetActive(false);

        GetComponent<Rigidbody2D>().angularVelocity = 20.0f;
    }

    private void Update()
    {
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
        int index = UnityEngine.Random.Range(0, m_lasers.Count);

        m_lasers[index].gameObject.SetActive(true);

        yield return new WaitForSeconds(m_laserDuration);

        m_lasers[index].gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D p_collision)
    {
        Dammage();
    }

    Player player;
    private void OnDestroy()
    {
        Debug.Log("détruit)");
        GameObject.Find("Player").GetComponent<Player>().CanBurst();
    }


}

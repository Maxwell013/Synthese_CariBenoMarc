using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMine : Enemy
{
    [Header("LaserMine")]
    [SerializeField] private List<GameObject> m_lasers = default; // List of laser groups
    [SerializeField] private float m_laserRate = default;
    [SerializeField] private float m_laserDuration = default;

    private float m_laserCooldown;

    new protected void Awake()
    {
        base.Awake();

        m_laserCooldown = Time.time + 2.0f * m_laserRate;

        foreach (GameObject group in m_lasers)
            group.SetActive(false);

        GetComponent<Rigidbody2D>().angularVelocity = 10.0f;
    }

    private void Update()
    {
        if (Time.time > m_laserCooldown)
            FireLasers();
    }

    private void FireLasers()
    {
        m_laserCooldown = Time.time + m_laserRate;
        StartCoroutine(LaserCoroutine());
    }

    private IEnumerator LaserCoroutine()
    {
        int index = Random.Range(0, m_lasers.Count);

        m_lasers[index].gameObject.SetActive(true);

        yield return new WaitForSeconds(m_laserDuration);

        m_lasers[index].gameObject.SetActive(false);
    }
}

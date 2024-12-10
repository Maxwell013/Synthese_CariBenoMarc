using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Entity : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] private int m_hp = 1;
    [SerializeField] private bool m_isInvincible = false;

    private int m_kills = 0;

    private const float m_maxX = 12.0f;
    private const float m_maxY = 9.0f;

    private void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (MathF.Abs(x) >= m_maxX || MathF.Abs(y) >= m_maxY)
            Destroy(gameObject);
    }

    public void Dammage(int p_amount = 1)
    {
        if (!m_isInvincible)
            m_hp -= p_amount;

        if (m_hp <= 0)
            Kills();
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // GameManager.Instance.DoSomething(); // increment point
    }


    private void Kills()
    {
        if (gameObject.CompareTag("Player") && gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.AddKills();
        }
    }
    
    
}

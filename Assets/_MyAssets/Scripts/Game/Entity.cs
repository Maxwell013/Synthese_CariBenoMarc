using System;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] protected int m_hp = 1; // tmp
    [SerializeField] private bool m_isInvincible = false;

    private const float m_maxX = 12.0f;
    private const float m_maxY = 9.0f;

    protected void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (MathF.Abs(x) >= m_maxX || MathF.Abs(y) >= m_maxY)
            Destroy(gameObject);
    }

    public void Dammage(bool p_sourceIsPlayer)
    {
        if (!m_isInvincible)
            m_hp--;

        if (m_hp <= 0)
        {
            if (p_sourceIsPlayer)
                Kill();
            Destroy(gameObject);
        }
    }

    abstract protected void Kill();
}

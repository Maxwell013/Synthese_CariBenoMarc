using System.Collections.Generic;
using UnityEngine;

public class FragRock : Enemy
{
    [Header("Frag Rock")]
    [SerializeField] private List<GameObject> m_fragments = default;

    private void OnTriggerEnter2D(Collider2D p_collision)
    {
        Dammage();
    }

    private void OnDestroy()
    {
        Quaternion r = transform.rotation;

        Instantiate(m_fragments[0], transform.position + r * new Vector3(-0.5f, -0.5f, 0.0f), r);
        Instantiate(m_fragments[1], transform.position + r * new Vector3( 0.5f, -0.5f, 0.0f), r);
        Instantiate(m_fragments[2], transform.position + r * new Vector3(-0.4f,  0.5f, 0.0f), r);
    }
}

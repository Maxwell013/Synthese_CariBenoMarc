using System.Collections.Generic;
using UnityEngine;

public class FragRock : Enemy
{
    [Header("Frag Rock")]
    [SerializeField] private List<GameObject> m_fragments = default;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dammage();
    }

    private void OnDestroy()
    {
        foreach (GameObject fragment in m_fragments)
        {
            Vector3 position = new(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f); // tmp
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); // tmp

            Instantiate(fragment, position + transform.position, rotation);
        }
    }
}

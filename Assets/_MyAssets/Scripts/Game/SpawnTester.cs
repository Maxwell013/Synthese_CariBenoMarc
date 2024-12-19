using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    private bool m_valid = true;

    private void OnTriggerEnter2D(Collider2D p_collision) { m_valid = false; }

    // Get methods
    public bool IsValid() { return m_valid; }
}

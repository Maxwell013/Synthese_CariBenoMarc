using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] private int m_hp = 1;
    
    public void Dammage(int p_amount = 1)
    {
        m_hp -= p_amount;
        if (m_hp <= 0)
            Destroy(this);
    }

    private void OnDestroy()
    {
        // GameManager.Instance.DoSomething();
    }
}

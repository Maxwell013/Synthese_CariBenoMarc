using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("SpawnManager")]
    [SerializeField] private GameObject m_enemyContainer = default;
    [SerializeField] private float m_initialMinSpawnCooldown = default;
    [SerializeField] private float m_initialMaxSpawnCooldown = default;
    [SerializeField] private List<Enemy> m_rockPrefabs = default;
    [SerializeField] private List<FragRock> m_fragRockprfab = default;
    [SerializeField] private List<LaserMine> m_laserMines = default;

    private float m_minSpawnCooldown = default;
    private float m_maxSpawnCooldown = default;

    private void Awake()
    {
        m_minSpawnCooldown = m_initialMinSpawnCooldown;
        m_maxSpawnCooldown = m_initialMaxSpawnCooldown;
    }

    private void Start()
    {
        // StartCoroutine(null);
    }

    private void UpdateSpawnCooldown()
    {

    }

    // Coroutines
    IEnumerator EnemySpawnCoroutines()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(m_minSpawnCooldown, m_maxSpawnCooldown));
        }
    }

}

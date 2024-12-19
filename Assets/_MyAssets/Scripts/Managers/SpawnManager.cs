using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [Header("SpawnManager")]
    [SerializeField] private GameObject m_enemyContainer = default;
    [SerializeField] private SpawnTester m_spawnTester = default;
    [SerializeField] private float m_initialMinSpawnCooldown = default;
    [SerializeField] private float m_initialMaxSpawnCooldown = default;
    [SerializeField] private List<GameObject> m_rockPrefabs = default;
    [SerializeField] private List<GameObject> m_fragRockPrefabs = default;
    [SerializeField] private List<GameObject> m_laserMinePrefabs = default;

    private float m_minSpawnCooldown = default;
    private float m_maxSpawnCooldown = default;
    private bool m_doEnemySpawning = false;
    private List<GameObject> m_spawnableEnemies = default;

    private void Awake()
    {
        m_spawnableEnemies = new List<GameObject>();
        m_minSpawnCooldown = m_initialMinSpawnCooldown;
        m_maxSpawnCooldown = m_initialMaxSpawnCooldown;
    }

    public void StartSpawning()
    {
        m_doEnemySpawning = true;
        StartCoroutine(EnemySpawnCoroutine());
    }

    public void UpdateSpawnRules()
    {
        float time = GameManager.Instance.GetTime();

        List<GameObject> all = m_rockPrefabs.Union(m_fragRockPrefabs).Union(m_laserMinePrefabs).ToList();

        int start = (int) Mathf.Clamp(Mathf.Floor(time/40 - 2), 0, all.Count - 1);
        int count = (int) Mathf.Clamp(Mathf.Floor(time/30 + 1) - start, 1, all.Count - start);

        m_spawnableEnemies = all.GetRange(start, count);
    }

    public void StopSpawning()
    {
        m_doEnemySpawning = false;
    }

    // Coroutines
    IEnumerator EnemySpawnCoroutine()
    {
        while (m_doEnemySpawning)
        {
            UpdateSpawnRules();
            
            GameObject enemy = m_spawnableEnemies[Random.Range(0, m_spawnableEnemies.Count)];

            for (int i_attempts = 0; i_attempts < 3; i_attempts++)
            {
                Vector3 position = GetSpawnPosition();
                
                SpawnTester tester = Instantiate(m_spawnTester, position, Quaternion.identity);

                // Wait for collisions to happen
                yield return new WaitForSeconds(0.1f);

                bool valid = tester.IsValid();

                Destroy(tester.gameObject);

                if (valid)
                {
                    Instantiate(enemy, position, Quaternion.identity);
                    break;
                }
            }

            yield return new WaitForSeconds(Random.Range(m_minSpawnCooldown, m_maxSpawnCooldown));
        }
    }


    private Vector3 GetSpawnPosition()
    {
        float r = Random.Range(-1.0f, 1.0f);
        switch (Random.Range(0, 4))
        {
        case 0: // up
            return new Vector3(r * 11.0f,  8.0f, 0.0f);
        case 1: // down
            return new Vector3(r * 11.0f, -8.0f, 0.0f);
        case 2: // left
            return new Vector3(-11.0f, r * 8.0f, 0.0f);
        case 3: // right
            return new Vector3( 11.0f, r * 8.0f, 0.0f);
        }
        return new Vector3(0.0f, 0.0f, 0.0f); // Fix compiler warnings
    }
}

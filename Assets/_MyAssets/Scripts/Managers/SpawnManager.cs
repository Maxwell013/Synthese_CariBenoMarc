using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("SpawnManager")]
    [SerializeField] private GameObject m_enemyContainer = default;
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

    private void Start() { StartSpawning(); } //  TMP

    public void StartSpawning()
    {
        UpdateSpawnRules();
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
            Vector3 position = default;
            float r = Random.Range(-1.0f, 1.0f);

            switch (Random.Range(0, 4))
            {
            case 0: // up
                position = new Vector3(r * 11.0f,  8.0f, 0.0f);
                break;
            case 1: // down
                position = new Vector3(r * 11.0f, -8.0f, 0.0f);
                break;
            case 2: // left
                position = new Vector3(-11.0f, r * 8.0f, 0.0f);
                break;
            case 3: // right
                position = new Vector3( 11.0f, r * 8.0f, 0.0f);
                break;
            }

            UpdateSpawnRules();

            GameObject enemy = m_spawnableEnemies[Random.Range(0, m_spawnableEnemies.Count)];

            Instantiate(enemy, position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(m_minSpawnCooldown, m_maxSpawnCooldown));
        }
    }

}

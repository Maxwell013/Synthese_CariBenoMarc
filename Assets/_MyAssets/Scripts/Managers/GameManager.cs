using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game settings")]
    [SerializeField] private float m_initalEnemySpeed = default;
    [SerializeField] private float m_enemySpeedScale = default;

    private float m_startTime;
    private int m_points;
    private bool m_isMuted = false;

    public static GameManager Instance = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        StartCoroutine(PlayerSpawnCoroutine());
        m_startTime = Time.time;
        m_points = 0;
    }

    public void EndGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("End"));
    }

    // Set functions
    public void SetMuted(bool p_state) { m_isMuted = p_state; }

    // Get functions
    public bool IsMuted() { return m_isMuted; }
    public float GetTime() { return Time.time - m_startTime; }
    public float GetPoints() { return m_points; }
    public float GetEnemySpeed() { return m_initalEnemySpeed + m_enemySpeedScale * GetTime(); }

    // Coroutines
    IEnumerator PlayerSpawnCoroutine()
    {
        yield return null;
    }
}

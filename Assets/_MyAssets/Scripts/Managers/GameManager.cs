using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game settings")]
    [SerializeField] private float m_initalEnemySpeed = default;
    [SerializeField] private float m_enemySpeedScale = default;

    private float m_startTime = default;
    private int m_points = default;
    private bool m_isMuted = false;

    private GameObject m_startAnimator = default;
    private GameObject m_player = default;

    public static GameManager Instance = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("Start");
        } else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame() { StartCoroutine(StartGameCoroutine()); } // Cotoutine is required for scene loading wait

    IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene("Game");

        // Wait for scene to load
        yield return new WaitForSeconds(0.1f);

        m_player = GameObject.Find("Player");
        m_startAnimator = GameObject.Find("StartAnimation");

        Animator animator = m_startAnimator.GetComponent<Animator>();

        m_player.SetActive(false);
        m_startAnimator.SetActive(true);

        animator.enabled = true;
        animator.Play("Start_anim");

        // Wait for animation end
        yield return new WaitForSeconds(1.0f);

        m_startAnimator.SetActive(false);
        animator.enabled = false;

        // Start player input
        m_player.GetComponent<Player>().Spawn();
        m_startTime = Time.time;
        m_points = 0;

        yield return null;
    }

    public void EndGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("End"));
    }

    // Set methods
    public void SetMuted(bool p_state) { m_isMuted = p_state; }

    // Get methods
    public bool IsMuted() { return m_isMuted; }

    public float GetTime() { return Time.time - m_startTime; }

    public int GetPoints() { return m_points; }

    public float GetEnemySpeed() { return m_initalEnemySpeed + m_enemySpeedScale * GetTime(); }
}

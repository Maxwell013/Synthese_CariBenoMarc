using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game settings")]
    [SerializeField] private float m_initalEnemySpeed = default;
    [SerializeField] private float m_enemySpeedScale = default;
    [SerializeField] private GameObject m_BGMusic = default;

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
            GameObject bgm = Instantiate(m_BGMusic, new Vector3(0.0f, 0.0f, -10.0f), Quaternion.identity);
            bgm.GetComponent<AudioSource>().mute = m_isMuted;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame() { StartCoroutine(StartGameCoroutine()); } // Cotoutine is required for scene loading wait

    IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene("Game");
        GameObject bgm = Instantiate(m_BGMusic, new Vector3(0.0f, 0.0f, -10.0f), Quaternion.identity);
        bgm.GetComponent<AudioSource>().mute = m_isMuted;

        // Wait for scene to load
        yield return new WaitForSeconds(0.5f);

        m_player = GameObject.FindWithTag("Player");
        m_startAnimator = GameObject.Find("Canvas/StartAnimation");

        Animator animator = m_startAnimator.GetComponent<Animator>();

        m_startAnimator.SetActive(true);

        animator.enabled = true;
        animator.Play("Start_anim");

        // Wait for animation end
        yield return new WaitForSeconds(0.2f);

        m_player.GetComponent<Player>().Spawn();

        // Start player input
        m_startAnimator.SetActive(false);
        m_startTime = Time.time;
        m_points = 0;

        animator.enabled = false;
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StartSpawning();

        yield return null;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
        GameObject bgm = Instantiate(m_BGMusic, new Vector3(0.0f, 0.0f, -10.0f), Quaternion.identity);
        bgm.GetComponent<AudioSource>().mute = m_isMuted;
    }

    public void IncrementPoints(int p_amout) {
        m_points += p_amout;
        GameObject.Find("Canvas").GetComponent<UIManagerOverlay>().UpdatePoints();

    }

    // Set methods
    public void SetMuted(bool p_state)
    {
        m_isMuted = p_state;
        GameObject.Find("BGMusic").GetComponent<AudioSource>().mute = m_isMuted;
    }

    // Get methods
    public bool IsMuted() { return m_isMuted; }

    public float GetTime() { return Time.time - m_startTime; }

    public int GetPoints() { return m_points; }

    public float GetEnemySpeed() { return m_initalEnemySpeed + m_enemySpeedScale * GetTime(); }

    public float GetFinalScore() { return GetTime() + GetPoints(); }
}

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
        } else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        m_player = GameObject.Find("Player");
        m_startAnimator = GameObject.Find("StartAnimation");

        Debug.Log("got stuff");

        m_player.SetActive(false);

        m_startAnimator.gameObject.SetActive(true);
        Animator animator = m_startAnimator.GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("Start_anim");

        Debug.Log("started animation");
        StartCoroutine(PlayerSpawnCoroutine());
        m_startTime = Time.time;
        m_points = 0;
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

    public float GetPoints() { return m_points; }
    
    public float GetEnemySpeed() { return m_initalEnemySpeed + m_enemySpeedScale * GetTime(); }

    // Coroutines
    IEnumerator PlayerSpawnCoroutine()
    {
        Debug.Log("dslkag");

        yield return new WaitForSeconds(1.5f);

        m_player.SetActive(true);
        m_player.GetComponent<Player>().Enable();

        m_startAnimator.GetComponent<Animator>().enabled = false;
        m_startAnimator.SetActive(false);
    }
}

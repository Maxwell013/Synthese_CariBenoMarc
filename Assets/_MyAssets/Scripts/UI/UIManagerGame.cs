using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;

public class UIManagerGame : MonoBehaviour
{

    public static UIManagerGame Instance;

    private bool m_pauseOn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TMP_Text m_txtScore = default;
    [SerializeField] private Image m_livesDisplayImage = default;
    [SerializeField] private Sprite[] m_liveSprites = default;
    [SerializeField] private GameObject m_pausePanel = default;
    [SerializeField] private GameObject m_resumeButton = default;

    private void Start()
    {
        m_pauseOn = false;
        Time.timeScale = 1;
        ChangeLivesDisplayImage(3);
        UpdateScore(0);
    }

    private void Update()
    {


        // Permet la gestion du panneau de pause (marche/arrêt)
        if ((Input.GetButtonDown("Pause") && !m_pauseOn))
        {
            m_pausePanel.SetActive(true);
            Time.timeScale = 0;
            m_pauseOn = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_resumeButton);
        }
        else if ((Input.GetButtonDown("Pause") && m_pauseOn))
        {
            ResumeGame();
        }

    }

    // Méthode qui change le pointage sur le UI
    public void UpdateScore(int score)
    {
        m_txtScore.text = "Pointage : " + score.ToString();
    }

    // Méthode qui permet de changer l'image des vies restantes en fonction de la vie du joueur
    public void ChangeLivesDisplayImage(int noImage)
    {
    
    }

    IEnumerator FinPartie()
    {
        yield return new WaitForSeconds(2f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }

    // Méthode qui relance la partie après une pause
    public void ResumeGame()
    {
        m_pausePanel.SetActive(false);
        Time.timeScale = 1;
        m_pauseOn = false;
    }

    public void OnReprendreClick()
    {
        ResumeGame();
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitterClick()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        System.Diagnostics.Process.Start(Application.dataPath + "/../../Portail/Portail.exe");
        Application.Quit()
    #endif
    }


}

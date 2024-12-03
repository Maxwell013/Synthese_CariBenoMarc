using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIStartEnd : MonoBehaviour
{
    [Header("Variables pour fin de partie")]
    [SerializeField] private TextMeshProUGUI m_txtGameOver = default;
    [SerializeField] private TextMeshProUGUI m_txtFinalScore = default;
    [SerializeField] private Button m_buttonMenu = default;
    [SerializeField] private Button m_buttonQuit = default;

    [Header("Variables pour scène départ")]
    [SerializeField] private GameObject m_startPanel = default;
    [SerializeField] private GameObject m_bestScoresPanel = default;
    [SerializeField] private GameObject m_startButton = default;
    [SerializeField] private GameObject m_returnButton = default;
    [SerializeField] private TMP_Text m_txtCounter = default;


    private void Start()
    {
        //Initialise le UI si scène de départ
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_startButton);

            //Utilise un playerPrefs pour compteur le nombre de parties joué
            //sur cet ordinateur
            if (PlayerPrefs.HasKey("Compteur"))
            {
                //Le compteur est augmenter quand le joueur clique sur Démarrer une nouvelle partie
                m_txtCounter.text = "Nombres de parties : " + PlayerPrefs.GetInt("Compteur").ToString();
            }
            else
            {
                m_txtCounter.text = "Nombres de parties : 0";
            }
        }

        //Initialise le UI si scène de fin
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            m_buttonMenu.onClick.AddListener(OnMenuClick);
            m_buttonQuit.onClick.AddListener(OnQuitterClick);
            m_txtFinalScore.text = "Votre pointage : " + GameManager.Instance.Score.ToString();
            GameOverSequence();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_buttonMenu.gameObject);
        }
    }

    public void OnQuitterClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnDemarrerClick()
    {
        //Augemte le players pour le compteur de parties
        if (PlayerPrefs.HasKey("Compteur"))
        {
            PlayerPrefs.SetInt("Compteur", PlayerPrefs.GetInt("Compteur") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Compteur", 1);
        }

        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnBestScoresClick()
    {
        m_startPanel.SetActive(false);
        m_bestScoresPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_returnButton.gameObject);

        //Coroutine qui ramène le panneau de départ automatiquement après 60 secondes
        StartCoroutine(DelaiRetourDebut());
    }

    IEnumerator DelaiRetourDebut()
    {
        yield return new WaitForSeconds(60f);
        OnRetourClick();
    }

    public void OnRetourClick()
    {
        m_startPanel.SetActive(true);
        m_bestScoresPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_startButton.gameObject);
    }

    // Méthode qui affiche la fin de la partie et lance la coroutine d'animation
    private void GameOverSequence()
    {
        m_txtGameOver.gameObject.SetActive(true);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine()
    {
        while (true)
        {
            m_txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            m_txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }

}

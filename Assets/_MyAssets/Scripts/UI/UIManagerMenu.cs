using UnityEngine;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManagerMenu : MonoBehaviour
{
    enum MenuType
    {
        MainMenu,
        EndMenu
    }

    [Header("UIManagerMenu")]
    [SerializeField] private MenuType m_type = MenuType.MainMenu;

    [Header("End Menu")]
    [SerializeField] private TextMeshProUGUI m_finalScoreText = default;
    [SerializeField] private Button m_mainMenuButton = default;
    [SerializeField] private Button m_quitButton = default;

    [Header("Main Menu")]
    [SerializeField] private GameObject m_startPanel = default;
    [SerializeField] private GameObject m_bestScoresPanel = default;
    [SerializeField] private GameObject m_startButton = default;
    [SerializeField] private GameObject m_returnButton = default;
    [SerializeField] private TMP_Text m_gamesPlayeddText = default;


    private void Start()
    {
        if (m_type == MenuType.MainMenu)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_startButton);

            if (PlayerPrefs.HasKey("GamesPlayed"))
            {
                m_gamesPlayeddText.text = "Nombres de parties : " + PlayerPrefs.GetInt("GamesPlayed").ToString();
            }
            else
            {
                m_gamesPlayeddText.text = "Nombres de parties : 0";
            }
        }

        if (m_type == MenuType.EndMenu)
        {
            m_mainMenuButton.onClick.AddListener(OnMainMenuClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
            m_finalScoreText.text = "Votre pointage : "; //  + GameManager.Instance.Score.ToString();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_mainMenuButton.gameObject);
        }
    }

    public void OnQuitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        System.Diagnostics.Process.Start(Application.dataPath + "/../../Portail/Portail.exe");
        Application.Quit();
#endif
    }

    public void OnStartClick()
    {
        if (PlayerPrefs.HasKey("GamesPlayed"))
        {
            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }
        // GameManager.Instance.StartGame();
    }

    public void OnBestScoresClick()
    {
        m_startPanel.SetActive(false);
        m_bestScoresPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_returnButton.gameObject);

        StartCoroutine(AutoMainMenuCoroutine());
    }

    IEnumerator AutoMainMenuCoroutine()
    {
        yield return new WaitForSeconds(60f);
        OnMainMenuClick();
    }

    public void OnMainMenuClick()
    {
        // GameManager.Instance.idk;
    }
}

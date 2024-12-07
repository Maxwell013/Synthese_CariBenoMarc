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
    [SerializeField] private Image m_muteImage = default;
    [SerializeField] private Sprite m_muteActiveSprite = default;
    [SerializeField] private Sprite m_muteInactiveSprite = default;
    [SerializeField] private Animator m_quitAnimator = default;
    [SerializeField] private GameObject m_quitLabel = default;

    [Header("End Menu")]

    [SerializeField] private TextMeshProUGUI m_finalScoreText = default;
    [SerializeField] private Button m_mainMenuButton = default;

    [Header("Main Menu")]
    [SerializeField] private Animator m_startAnimator = default;
    [SerializeField] private GameObject m_startLabel = default;

    [SerializeField] private GameObject m_startPanel = default;
    [SerializeField] private GameObject m_bestScoresPanel = default;
    [SerializeField] private GameObject m_returnButton = default;
    [SerializeField] private TMP_Text m_gamesPlayeddText = default;

    private void Start()
    {
        m_muteImage.sprite = m_muteInactiveSprite;
        m_quitLabel.SetActive(true);
        m_startLabel.SetActive(true);

        if (m_type == MenuType.MainMenu)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_startLabel.GetComponentInParent<GameObject>());

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
            m_finalScoreText.text = "Votre pointage : "; //  + GameManager.Instance.Score.ToString();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_mainMenuButton.gameObject);
        }

    }

    public void OnMuteClick()
    {
        if (GameManager.Instance.IsMuted())
        {
            m_muteImage.sprite = m_muteInactiveSprite;
            GameManager.Instance.SetMuted(false);
        } else
        {
            m_muteImage.sprite = m_muteActiveSprite;
            GameManager.Instance.SetMuted(true);
        }
    }

    public void OnQuitClick()
    {
        m_quitLabel.SetActive(false);
        m_quitAnimator.Play("Ship_anim");
        StartCoroutine(QuitAfterAnimationCoroutine());
    }

    private IEnumerator QuitAfterAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        System.Diagnostics.Process.Start(Application.dataPath + "/../../Portail/Portail.exe");
        Application.Quit();
#endif
    }

    public void OnStartClick()
    {
        m_startLabel.SetActive(false);
        m_startAnimator.Play("Ship_anim");

        if (PlayerPrefs.HasKey("GamesPlayed"))
        {
            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }

        StartCoroutine(StartAfterAnimationCoroutine());
    }

    private IEnumerator StartAfterAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.StartGame();
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

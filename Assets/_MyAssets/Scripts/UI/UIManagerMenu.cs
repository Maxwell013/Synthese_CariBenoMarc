using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManagerMenu : MonoBehaviour
{
    enum MenuType { MainMenu, EndMenu }

    [Header("UIManagerMenu")]
    [SerializeField] private MenuType m_type = MenuType.MainMenu;
    [SerializeField] private GameObject m_buttonPanel = default;
    [SerializeField] private GameObject m_muteButton = default;
    [SerializeField] private Sprite m_muteActiveSprite = default;
    [SerializeField] private Sprite m_muteInactiveSprite = default;
    [SerializeField] private Sprite m_shipButtonSprite = default;
    [SerializeField] private GameObject m_quitButton = default;
    [SerializeField] private GameObject m_returnButton = default;

    [Header("Main Menu")]
    [SerializeField] private GameObject m_startButton = default;
    [SerializeField] private GameObject m_infoButton = default;
    [SerializeField] private GameObject m_infoPanel = default;

    [SerializeField] private GameObject m_bestScoresPanel = default;
    [SerializeField] private TMP_Text m_gamesPlayeddText = default;

    [Header("End Menu")]

    [SerializeField] private TextMeshProUGUI m_finalScoreText = default;
    [SerializeField] private Button m_mainMenuButton = default;

    private Image m_muteImage = default;

    private void Awake() // Get children components to reduce serialize fields
    {
        m_muteImage = m_muteButton.transform.Find("Image").GetComponent<Image>();
        AddListener(m_muteButton, OnMuteClick);
        AddListener(m_quitButton, OnQuitClick);

        if (m_type == MenuType.MainMenu)
        {
            AddListener(m_startButton, OnStartClick);
            AddListener(m_infoButton, OnInfoClick);
            AddListener(m_returnButton, OnInfoReturnClick);

        } else if (m_type == MenuType.EndMenu)
        {
            //todo 
        }

    }

    private void ResetButton(GameObject p_button)
    {
        p_button.transform.Find("Label").gameObject.SetActive(true);
        p_button.transform.Find("Image").GetComponent<Animator>().enabled = false;
        p_button.transform.Find("Image").GetComponent<Image>().sprite = m_shipButtonSprite;
    }

    static private void StartAnimation(GameObject p_button)
    {
        p_button.transform.Find("Label").gameObject.SetActive(false);
        p_button.transform.Find("Image").GetComponent<Animator>().enabled = true;
        p_button.transform.Find("Image").GetComponent<Animator>().Play("Ship_anim");
    }

    static private void SetSelectedButton(GameObject p_button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(p_button.gameObject);
    }

    static private void AddListener(GameObject p_button, UnityEngine.Events.UnityAction p_callback)
    {
        p_button.GetComponent<Button>().onClick.AddListener(p_callback);
    }

    private void Start()
    {
        m_muteImage.sprite = m_muteInactiveSprite;
        ResetButton(m_quitButton);

        if (m_type == MenuType.MainMenu)
        {
            ResetButton(m_startButton);
            ResetButton(m_infoButton);

            SetSelectedButton(m_startButton);

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
        StartAnimation(m_quitButton);
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
        StartAnimation(m_startButton);

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

    public void OnInfoClick()
    {
        StartAnimation(m_infoButton);
        StartCoroutine(InfoAfterAnimationCoroutine());
    }

    private IEnumerator InfoAfterAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        m_buttonPanel.SetActive(false);
        m_infoPanel.SetActive(true);
        SetSelectedButton(m_returnButton);
    }

    public void OnInfoReturnClick()
    {
        m_buttonPanel.SetActive(true);
        m_infoPanel.SetActive(false);
        SetSelectedButton(m_startButton);
        ResetButton(m_infoButton);
    }

    public void OnBestScoresClick()
    {
        m_buttonPanel.SetActive(false);
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

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManagerOverlay : MonoBehaviour
{

    [Header("UIMangerOverlay")]
    [SerializeField] private TMP_Text m_pointsText = default;
    [SerializeField] private TMP_Text m_timerText = default;
    [SerializeField] private Image m_hpBar = default;
    

    private int m_hpBarWidth = default;
    private RectTransform m_hpBarRt = default;

    public static UIManagerOverlay Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_hpBarRt = m_hpBar.GetComponent<RectTransform>();
            m_hpBarWidth = (int) m_hpBarRt.rect.width;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        m_timerText.text = "Temps : " + GameManager.Instance.GetTime().ToString("f2") + "s" ;
        if (Input.GetButtonDown("Jump"))
            GameManager.Instance.SetMuted(!GameManager.Instance.IsMuted());
    }

    public void UpdatePoints()
    {
        m_pointsText.text = "Pointage : " + GameManager.Instance.GetPoints().ToString();
    }

    public void UpdateHPBar()
    {
        m_hpBar.fillAmount = GameObject.Find("Player").GetComponent<Entity>().GetHP() / 3.0f;
    }


}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerOverlay : MonoBehaviour
{

    [Header("UIMangerOverlay")]
    [SerializeField] private TMP_Text m_pointsText = default;
    [SerializeField] private TMP_Text m_timerText = default;
    [SerializeField] private Image m_hpBar = default;

    public static UIManagerOverlay Instance;

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

    private void Update()
    {
        m_timerText.text = "Temps : "; // + GameManager.Instance.getTime();
    }

    public void UpdatePoints(int p_score)
    {
        m_pointsText.text = "Pointage : " + p_score.ToString();
    }

    public void UpdateHPBar(int p_hp)
    {
       m_hpBar.fillAmount = p_hp / 3;
    }


}

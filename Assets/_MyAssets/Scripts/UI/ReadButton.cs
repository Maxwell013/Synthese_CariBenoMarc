using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadButton : MonoBehaviour
{
    [SerializeField] private HighScoreTable m_highScoreTable;

    private Button m_button;

    private void Start()
    {
        m_highScoreTable = FindObjectOfType<HighScoreTable>();
        m_button = this.GetComponent<Button>();
        m_button.onClick.AddListener(Read);
    }

    public void Read()
    {
        if (m_highScoreTable != null)
        {
            m_highScoreTable.AjouterLettre(this.GetComponentInChildren<TMP_Text>().text);
        }
    }
}

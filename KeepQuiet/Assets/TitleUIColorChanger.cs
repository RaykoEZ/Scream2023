using TMPro;
using UnityEngine;
// Sets text colour for different background changes
public class TitleUIColorChanger: MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI m_title = default;
    [SerializeField] TextMeshProUGUI m_newGame = default;
    [SerializeField] TextMeshProUGUI m_continue = default;
    [SerializeField] TextMeshProUGUI m_exit = default;
    [SerializeField] TextMeshProUGUI m_clearCache = default;

    [SerializeField] Color m_defaultText = default;
    [SerializeField] Color m_darkText = default;
    [SerializeField] Color m_brightText = default;

    void ChangeTextColour(Color col) 
    {
        m_title.color = col;
        m_newGame.color = col;
        m_continue.color = col;
        m_exit.color = col;
        m_clearCache.color = col;
    }
    public void OnDefaultBackground()
    {
        ChangeTextColour(m_defaultText);
    }
    public void OnWhiteBackground() 
    {
        ChangeTextColour(m_darkText);
    }
    public void OnDarkBackground() 
    {
        ChangeTextColour(m_brightText);
    }
}

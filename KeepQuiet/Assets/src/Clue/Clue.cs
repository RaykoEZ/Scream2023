using Curry.Explore;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Switches and handling for a Clue UI
public class Clue : MonoBehaviour
{
    [SerializeField] Button m_button = default;
    [SerializeField] Image m_uiImage = default;
    [SerializeField] TextMeshProUGUI m_hoverLabel = default;
    [SerializeField] HideableUI m_clueImage = default;
    public HideableUI ClueImage => m_clueImage;
    public TextMeshProUGUI HoverLabel => m_hoverLabel;
    public virtual InspectionDisplay GetInspectionDisplay(SaveData state) 
    {
        return null;
    }
    public void Show()
    {
        ClueImage?.Show();
        m_uiImage.enabled = true;
        m_button.interactable = true;
    }
    public void Hide() 
    {
        ClueImage?.Hide();    
        m_uiImage.enabled = false;
        m_button.interactable = false;
    }
}

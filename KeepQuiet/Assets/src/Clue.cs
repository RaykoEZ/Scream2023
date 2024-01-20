using Curry.Explore;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnClueInspect(Clue toInspect);
// Switches and handling for a Clue UI
public class Clue : MonoBehaviour
{
    [SerializeField] Button m_button = default;
    [SerializeField] Image m_uiImage = default;
    [SerializeField] HideableUI m_clueImage = default;
    public HideableUI ClueImage { get => m_clueImage; set => m_clueImage = value; }

    public event OnClueInspect OnInspect;
    public virtual InspectionDisplay GetInspectionDisplay(GameStateSaveData state) 
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

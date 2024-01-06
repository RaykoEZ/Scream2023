using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnClueInspect(Clue toInspect);
// Switches and handling for a Clue UI
public class Clue : MonoBehaviour
{
    [SerializeField] Button m_button = default;
    [SerializeField] Image m_uiImage = default;
    public event OnClueInspect OnInspect;
    public void Show() 
    {
        m_uiImage.enabled = true;
        m_button.interactable = true;
    }
    public void Hide() 
    {
        m_uiImage.enabled = false;
        m_button.interactable = false;
    }
    public void InspectClue()
    {
        BeginInspectContent();
        OnInspect?.Invoke(this);
    }
    protected virtual void BeginInspectContent() 
    { 
    
    }
}

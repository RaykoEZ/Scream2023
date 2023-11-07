using UnityEngine;
using TMPro;

public class DialHandler : MonoBehaviour 
{
    [SerializeField] int m_characterLimit = default;
    [SerializeField] TextMeshProUGUI m_display = default;  
    public void Dial(int number) 
    { 
        // check for characterLimit and input validation
        if (number > 9 
            || number < 0 
            || m_display.text.Length >= m_characterLimit) 
        { 
            return; 
        }
        m_display.text += number.ToString();
    }
    
    public void DialHash() 
    {
        m_display.text += "#";

    }
    public void DialStar() 
    {
        m_display.text += "*";
    }
    // Start Call, check puzzle phone book for results 
    public void ConfirmInput()
    {
        
    }
    public void ClearDial() 
    {
        m_display.text = "";
    }
}

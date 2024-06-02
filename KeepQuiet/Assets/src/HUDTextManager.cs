using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
public class HUDTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_dialogueDisplay = default;
    [SerializeField] Animator m_anim = default;
    Queue<string> m_dialogue;
    string m_currentLine;
    Coroutine m_displayingText;
    public event OnDialogueEnd OnEnd;
    public bool InProgress { get; protected set; } = false;
    public void DisplaySingle(string text) 
    {
        m_dialogue?.Clear();
        m_currentLine = "";
        m_dialogueDisplay.text = text;
        m_anim.SetBool("Show", true);
    }
    public void OpenDialogue(List<string> dialogue) 
    {
        InProgress = true;
        m_dialogue?.Clear();
        m_currentLine = "";
        m_dialogueDisplay.text = "";
        m_dialogue = new Queue<string>(dialogue);
        m_anim.SetBool("Show", true);
        NextPage();
    }

    public void NextPage() 
    {
        if(m_dialogue == null) { return; }

        m_dialogueDisplay.text = "";
        if(m_dialogue.Count == 0) 
        {
            EndDialogue();
        }
        else if (m_displayingText != null) 
        {
            // Skip letter by letter display
            // when player clicks [next] during display coroutine
            StopCoroutine(m_displayingText);
            // Display entire page immediately
            m_dialogueDisplay.text = m_currentLine;
            m_displayingText = null;
        }
        else 
        {
            // start lett-by-letter
            m_currentLine = m_dialogue.Dequeue();
            m_displayingText = StartCoroutine(OnShowDialogue(m_currentLine));
        }
    }

    IEnumerator OnShowDialogue(string dialogue) 
    {
        char[] letters = dialogue.ToCharArray();
        foreach(char letter in letters) 
        {
            // Show text letter-by-letter
            m_dialogueDisplay.text += letter;
            yield return null;
        }
        m_displayingText = null;
    }

    void EndDialogue()
    {
        // end
        m_anim.SetBool("Show", false);
        InProgress = false;
        m_dialogue = null;
        OnEnd?.Invoke();
    }
}


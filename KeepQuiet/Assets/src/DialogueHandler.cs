using System.Collections.Generic;
using TMPro;
using UnityEngine;
public delegate void OnPromptDialogueOption(List<DialogueNode> options);
public delegate void OnDialogueEnd();
// Need a node-network for dialogue states & decisions
public class DialogueHandler : MonoBehaviour
{
    [SerializeField] Transform m_dialogueBoxParent = default;
    [SerializeField] TextMeshProUGUI m_title = default;
    [SerializeField] DialogueOptionPrompter m_optionPrompt = default;
    [SerializeField] DialogueDisplay m_dialogueBoxToSpawn = default;
    Dictionary<string, DialogueDisplay> m_spawnedDialogueBoxes = new Dictionary<string, DialogueDisplay>();
    // all dialogues displayed before ending dialogue
    DialogueDisplay m_currentDisplay;
    public event OnDialogueEnd OnEnd;
    private void Start()
    {
        m_optionPrompt.OnChosen += OnPlayerChosen;
    }
    private void OnDestroy()
    {
        Shutdown();
    }
    // Instantiate all chat UIs, call at start of chat
    public void PrepareDialogueBoxes(Dictionary<string, ChatHistory> histories) 
    { 
        foreach (var kvp in histories) 
        {
            DialogueDisplay instance = Instantiate(m_dialogueBoxToSpawn, m_dialogueBoxParent);
            instance.Init(kvp.Value);
            instance.OnPrompt += OnPromptChoice;
            instance.OnEnd += EndDialogue;
            m_spawnedDialogueBoxes.Add(kvp.Key, instance);
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnPlayerChosen;
        // shutdown all message boxes
        foreach (var kvp in m_spawnedDialogueBoxes) 
        {
            kvp.Value.OnPrompt -= OnPromptChoice;
            kvp.Value.OnEnd -= EndDialogue;
            Destroy(kvp.Value);
            m_spawnedDialogueBoxes.Remove(kvp.Key);
        }
    }
    public void StartDialogue(string chattingWith) 
    {
        if (!m_spawnedDialogueBoxes.TryGetValue(chattingWith, out DialogueDisplay result)) 
        {
            OnEnd?.Invoke();
            return; 
        }
        m_title.text = chattingWith;
        foreach (var kvp in m_spawnedDialogueBoxes) 
        {
            kvp.Value.Hide();
        }
        m_currentDisplay = result;
        // Display the preloaded ui for chatting with this NPC
        m_currentDisplay.Show();
    }
    void EndDialogue() 
    {
        OnEnd?.Invoke();
    }
    void OnPromptChoice(List<DialogueNode> options)
    {
        m_optionPrompt.PromptOption(options);
    }
    void OnPlayerChosen(DialogueNode chosen) 
    {
        m_currentDisplay.UpdateDialogueNode(chosen);
        m_currentDisplay.ResumeChat();
    }
}

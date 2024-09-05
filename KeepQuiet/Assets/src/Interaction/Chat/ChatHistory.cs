using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Stores previous an NPC's conversatons in the chat room
[Serializable]
public class ChatHistory
{
    // whose chat history to assign this to
    [SerializeField] string m_username = default;
    // reference to each dialogue scriptableobject, used to save and load game state
    [SerializeField] List<AssetReference> m_chatLogAssets = default;
    // loaded chat dialogue nodes, loaded from chat log asset  references
    [NonSerialized] AddressableContainer<DialogueNode> m_currentChatLog = 
        new AddressableContainer<DialogueNode>();
    public string Username => m_username;
    public List<DialogueNode> ChatLog => m_currentChatLog.LoadedAssets;
    public DialogueNode LastDialogue => m_currentChatLog.LoadedAssets.Last();
    public ChatHistory(string name, List<AssetReference> assetRefs)
    {
        m_username = name;
        m_chatLogAssets = assetRefs;
    }
    // Get asset reference from new dialogues added after initial load
    public void UpdateAssetReferences(DialogueAssetReferenceIndex index) 
    {
        List<AssetReference> refs = AddressableContainer<DialogueNode>.GetAssetReferenceList(index, m_currentChatLog);
        m_chatLogAssets = refs;
    }
    public void LoadDialogueAsync(bool overwrite = false, Action<List<DialogueNode>> onFinish = null) 
    {
        m_currentChatLog.LoadAssetAsync(m_chatLogAssets ,overwrite, onFinish);
    }
    // Clear log and overwrite all content
    public void OverwriteLog(DialogueNode lastDialogue) 
    {
        m_currentChatLog.Clear();
        Append(lastDialogue);
    }
    public void Append(DialogueNode append) 
    {
        m_currentChatLog.LoadedAssets.Add(append);
    }
}

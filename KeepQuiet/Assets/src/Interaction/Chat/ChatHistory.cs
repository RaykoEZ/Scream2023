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
    [NonSerialized] List<DialogueNode> m_currentChatHistory = new List<DialogueNode>();
    public string Username => m_username;
    public List<DialogueNode> ChatLog => m_currentChatHistory;
    public DialogueNode LastDialogue => m_currentChatHistory.Last();
    public ChatHistory(string name, List<AssetReference> assetRefs)
    {
        m_username = name;
        m_chatLogAssets = assetRefs;
    }
    public ChatHistory(string name, List<DialogueNode> nodes) 
    {
        m_username = name;
        m_currentChatHistory = nodes;
    }
    //Search asset reference index to update chat assets for saving game data
    public List<AssetReference> UpdateHistoryAssets(DialogueAssetReferenceIndex index) 
    {
        // Clear old list
        m_chatLogAssets.Clear();
        AssetReference assetRef;
        // go through list of current history and collect asset references
        foreach (var item in m_currentChatHistory)
        {
            assetRef = index.Find(item.name);
            if (assetRef == null) continue;
            m_chatLogAssets.Add(assetRef);
        }
        return m_chatLogAssets;
    }
    // Clear log and overwrite all content
    public void OverwriteLog(DialogueNode lastDialogue) 
    {
        m_currentChatHistory.Clear();
        Append(lastDialogue);
    }
    public void Append(DialogueNode append) 
    {
        m_currentChatHistory.Add(append);
    }
}

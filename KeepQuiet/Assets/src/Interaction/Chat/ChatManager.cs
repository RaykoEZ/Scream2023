using Curry.Explore;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] DialogueAssetReferenceIndex m_assetIndex = default;
    [SerializeField] ChatRoom m_chatRoom = default;
    public event OnChatUpdate OnEnd;
    // loaded chat dialogue nodes, loaded from chat log asset references
    Dictionary<string, AddressableContainer<DialogueNode>> m_currentChatLogs =
        new Dictionary<string,AddressableContainer<DialogueNode>>();
    private void OnDestroy()
    {
        Shutdown();
    }
    public void Init(SaveData save) 
    {
        m_currentChatLogs?.Clear();
        foreach (var item in save.ChatHistories)
        {
            m_currentChatLogs.Add(item.Username, new AddressableContainer<DialogueNode>());
        }
        LoadDialogueAsync(save, true);
    }
    public void UpdateSave()
    {
        UpdateAssetReferences(m_assetIndex);
    }
    public void Shutdown()
    {
        // shutdown chat room
        m_chatRoom?.Shutdown();
    }
    // load chat of the person in question
    public void BeginChat(string username)
    {
        ChatHistory result = FindHistory(username, m_save.Current.ChatHistories);
        if (result == null) return;
        //instantiate history logs and store them here for record keeping if needed
        if(m_currentChatLogs.TryGetValue(result.Username, out var log))
        {
            m_chatRoom.SetChatLog(log);
            m_chatRoom.Hide();
            StartCurrentChat();
            Show();
        }
    }
    // Redirect to ContactList
    public void OnNewMessage(DialogueNode newDialogue, string username)
    {
        ChatHistory result = FindHistory(username, m_save.Current.ChatHistories);
        if (result == null) return;
        if (m_currentChatLogs.TryGetValue(result.Username, out var log))
        {
            // instantiate history logs and store them here for record keeping if needed
            m_chatRoom.SetChatLog(log);
            m_chatRoom.Hide();
            m_chatRoom.NewDialogue(newDialogue);
        }
    }
    void StartCurrentChat()
    {
        // listen to dialogue update events
        m_chatRoom.OnEnd += EndDialogue;
        // Display the preloaded ui for chatting with this NPC
        m_chatRoom.Show();
        m_chatRoom.StartChat();
    }
    void EndDialogue()
    {
        // unlisten dialogue events
        m_chatRoom.OnEnd -= EndDialogue;
        OnEnd?.Invoke();
    }
    static ChatHistory FindHistory(string name, List<ChatHistory> list)
    {
        if (list == null || string.IsNullOrWhiteSpace(name))
        {
            return null;
        }
        var result = list.Find(x => x.Username == name);
        return result;
    }
    // Get asset reference from new dialogues added after initial load
    protected void UpdateAssetReferences(DialogueAssetReferenceIndex index)
    {
        List<ChatHistory> historiesRef = m_save.Current.ChatHistories;
        List<AssetReference> refs;
        foreach (var item in historiesRef)
        {
            refs = AddressableContainer<DialogueNode>.GetAssetReferenceList(index, m_currentChatLogs[item.Username]);
            item.ChatLogAssets = refs;
        }
    }
    protected void LoadDialogueAsync(SaveData save, bool overwrite = false, Action<List<DialogueNode>> onFinish = null)
    {
        ChatHistory history;
        foreach (var kvp in m_currentChatLogs)
        {
            history = FindHistory(kvp.Key, save.ChatHistories);
            kvp.Value.LoadAssetAsync(history.ChatLogAssets, overwrite, onFinish);
        }
    }
}

using Curry.Explore;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Notifies player when message comes
public class ChatManager : HideableUI 
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] List<DialogueAssetReferenceIndex> m_chapterAssets = default;
    [SerializeField] ChatRoom m_chatRoom = default;
    public event OnChatUpdate OnEnd;
    DialogueAssetReferenceIndex m_currentAssetIndex;
    AddressableContainer<DialogueNode> m_allLoadedAsset = new AddressableContainer<DialogueNode>();
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
        LoadChapterAssets(save.ChapterIndex);
    }
    // load all relevant dialogue assets for the chapter
    void LoadChapterAssets(int chapterCode) 
    {
        if (chapterCode >= m_chapterAssets.Count || chapterCode < 0) return;
        // setup chat dialogue node database for updating saves later
        m_currentAssetIndex = m_chapterAssets[chapterCode];
        m_allLoadedAsset?.LoadAssetAsync(m_currentAssetIndex.AssetReferences, OnChapterLoaded);
    }
    void OnChapterLoaded(List<DialogueNode> result) 
    {
        foreach (var item in m_save.Current.ChatHistories)
        {
            m_currentChatLogs.Add(item.Username, new AddressableContainer<DialogueNode>());
        }
        // load current chat history
        LoadDialogueAsync(m_save.Current);
    }
    protected void LoadDialogueAsync(SaveData save, Action<List<DialogueNode>> onFinish = null)
    {
        ChatHistory history;
        foreach (var kvp in m_currentChatLogs)
        {
            history = FindHistory(kvp.Key, save.ChatHistories);
            kvp.Value.LoadAssetAsync(history.ChatLogAssets, onFinish);
        }
    }
    public void UpdateSave()
    {
        UpdateAssetReferences(m_chapterAssets[m_save.Current.ChapterIndex]);
    }
    public void Shutdown()
    {
        // shutdown chat room
        m_chatRoom?.Shutdown();
        m_allLoadedAsset.Clear();
        foreach (var item in m_currentChatLogs)
        {
            item.Value?.Clear();
        }
        m_currentChatLogs.Clear();
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
            refs = AddressableContainer<DialogueNode>.GetAssetReferenceList(m_currentAssetIndex, m_currentChatLogs[item.Username]);
            item.ChatLogAssets = refs;
        }
    }
}

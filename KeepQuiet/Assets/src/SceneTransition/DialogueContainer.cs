using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DialogueContainer : MonoBehaviour
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] List<DialogueAssetReferenceIndex> m_chapterAssets = default;
    DialogueAssetReferenceIndex m_currentAssetIndex;
    AddressableContainer<DialogueNode> m_allLoadedAsset = new AddressableContainer<DialogueNode>();
    // loaded chat dialogue nodes, loaded from chat log asset references
    Dictionary<string, AddressableContainer<DialogueNode>> m_currentChatLogs =
        new Dictionary<string, AddressableContainer<DialogueNode>>();
    private bool m_loadFinished = false;
    public DialogueAssetReferenceIndex CurrentAssetIndex => m_currentAssetIndex;
    public Dictionary<string, AddressableContainer<DialogueNode>> CurrentChatLogs => m_currentChatLogs;
    public AddressableContainer<DialogueNode> AllLoadedAsset => m_allLoadedAsset;
    public bool LoadFinished { get => m_loadFinished; private set => m_loadFinished = value; }
    private void OnDestroy()
    {
        AllLoadedAsset.Clear();
        foreach (var item in CurrentChatLogs)
        {
            item.Value?.Clear();
        }
        CurrentChatLogs.Clear();
    }
    public void Init(SaveData save)
    {
        m_currentChatLogs?.Clear();
        LoadChapterAssets(save.ChapterIndex);
    }
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
        LoadDialogueAsync(m_save.Current, OnLoaded);
    }
    void OnLoaded(List<DialogueNode> _)
    {
        m_loadFinished = true;
    }
    protected void LoadDialogueAsync(SaveData save, Action<List<DialogueNode>> onFinish = null)
    {
        ChatHistory history;
        foreach (var kvp in m_currentChatLogs)
        {
            history = ChatManager.FindHistory(kvp.Key, save.ChatHistories);
            kvp.Value.LoadAssetAsync(history.ChatLogAssets, onFinish);
        }
    }
    public void UpdateSave()
    {
        UpdateAssetReferences();
    }
    // Get asset reference from new dialogues added after initial load
    protected void UpdateAssetReferences()
    {
        List<ChatHistory> historiesRef = m_save.Current.ChatHistories;
        List<AssetReference> refs;
        foreach (var item in historiesRef)
        {
            refs = AddressableContainer<DialogueNode>.GetAssetReferenceList(
                m_currentAssetIndex, m_currentChatLogs[item.Username]);
            item.ChatLogAssets = refs;
        }
    }
}

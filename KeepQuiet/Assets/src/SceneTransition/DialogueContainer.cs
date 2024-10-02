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
    private bool m_loading = false;
    public Dictionary<string, AddressableContainer<DialogueNode>> CurrentChatLogs => m_currentChatLogs;
    public AddressableContainer<DialogueNode> AllLoadedAsset => m_allLoadedAsset;
    public bool Loading { get => m_loading; private set => m_loading = value; }
    private void OnDestroy()
    {
        AllLoadedAsset.Clear();
        foreach (var item in CurrentChatLogs)
        {
            item.Value?.Clear();
        }
    }
    public void Init(SaveData save)
    {
        m_currentChatLogs?.Clear();
        LoadChapterAssets(save.ChapterIndex);
    }
    void LoadChapterAssets(int chapterCode)
    {
        if (m_loading || chapterCode >= m_chapterAssets.Count || chapterCode < 0) return;
        m_loading = true;
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
        m_loading = false;
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
        foreach (var item in m_save.Current.ChatHistories)
        {
            item.ChatLogAssets = CurrentChatLogs[item.Username].AssetRefs;
        }
    }
}

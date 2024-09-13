using Curry.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Holds states of held thoughts
public class ThoughtStateManager : MonoBehaviour
{
    [SerializeField] GameSaveSource m_gameState = default;
    [SerializeField] ThoughtAssetReferenceIndex m_assetIndex = default;
    [SerializeField] ThoughtSpawnManager m_spawn = default;
    [SerializeField] ToggleAnimationHandler m_toggleAnim = default;
    AddressableContainer<ThoughtDetail> m_allLoadedAsset = new AddressableContainer<ThoughtDetail>();
    AddressableContainer<ThoughtDetail> m_heldThoughts = new AddressableContainer<ThoughtDetail>();
    private void OnDestroy()
    {
        Shutdown();
    }
    public void Refresh(SaveData save)
    {
        Init(save?.HeldThoughts);
    }
    void Init(List<AssetReference> thoughtToLoad)
    {
        if (thoughtToLoad != null) 
        {
            // setup database for updating saves later
            m_allLoadedAsset.LoadAssetAsync(m_assetIndex.AssetReferences);
            // load held thoughts
            m_heldThoughts.LoadAssetAsync(m_gameState.Current.HeldThoughts, OnThoughtsLoaded);
        }
    }
    void Shutdown() 
    {
        m_allLoadedAsset?.Clear();
        m_heldThoughts?.Clear();
    }
    void OnThoughtsLoaded(List<ThoughtDetail> loaded) 
    {
        // Spawn currently held thoughts
        foreach (var item in loaded)
        {
            m_spawn?.SpawnThoughtBubble(item);
        }
    }
    public void UpdateSave()
    {
        List<AssetReference> refs = AddressableContainer<ThoughtDetail>.
            GetAssetReferenceList(m_assetIndex, m_heldThoughts);
        m_gameState.Current.HeldThoughts = refs;
    }
    void Add(ThoughtDetail newThought) 
    {
        if (newThought == null) return;
        m_heldThoughts.LoadedAssets.Add(newThought);
    }
    void Remove(ThoughtDetail toRemove) 
    {
        m_heldThoughts.LoadedAssets.Remove(toRemove);
        Addressables.Release(toRemove);
    }
    // When thoughts can be dropped into things
    public void OnThoughtPrompt(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("toDrop", out object result) &&
            result is List<ThoughtDetail> toDrop)
        {
            // Check if player has any held thoughts to drop
            HashSet<ThoughtDetail> check = new HashSet<ThoughtDetail>(m_heldThoughts.LoadedAssets);
            check.IntersectWith(toDrop);
            m_toggleAnim?.AnimateAlertIcon(check.Count > 0);
        }
    }
    public void OnConsume(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        if(info.Payload.TryGetValue("thought", out object result) && 
            result is ThoughtBubble bubble) 
        {
            Remove(bubble.DetailRef);
            bubble?.Shutdown();
            Destroy(bubble.gameObject);
        } 
    }
    public void OnObtain(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("thought", out object result) &&
            result is ThoughtDetail detail)
        {
            ObtainThought(detail);
        }
    }
    public void ObtainThought(ThoughtDetail toObtain) 
    {
        Add(toObtain);
        m_spawn?.SpawnThoughtBubble(toObtain);
        m_toggleAnim?.AnimateAlertIcon(true);
    }
}
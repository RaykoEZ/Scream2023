using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// List of DialogueNode AssetRefs for save/loading
[CreateAssetMenu(fileName = "DialogueAssetRefs_", menuName = "Chat/List of DialogueNode AssetRefs for save/loading")]
public class DialogueAssetReferenceIndex : ScriptableObject
{
    [AssetReferenceUILabelRestriction("dialogue")]
    [SerializeField] List<AssetReference> m_dialoguesAssetReferences = default;
    // user filename as id, unique name enforced
    public AssetReference Find(string name) 
    {
        return m_dialoguesAssetReferences.Find((t) => t.Asset.name == name);
    }
}

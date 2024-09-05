using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName = "DialogueAssetRefs_", menuName = "Chat/List of DialogueNode AssetRefs for save loading")]
public class DialogueAssetReferenceIndex : AssetReferenceIndex
{
    [AssetReferenceUILabelRestriction("dialogue")]
    [SerializeField] List<AssetReference> m_dialoguesAssetRefs = default;
    public override List<AssetReference> AssetReferences => m_dialoguesAssetRefs;
}

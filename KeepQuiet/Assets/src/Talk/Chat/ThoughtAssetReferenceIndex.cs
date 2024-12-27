using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// List of DialogueNode AssetRefs for save/loading
[CreateAssetMenu(fileName = "ThoughtAssetRefs_", menuName = "Thought/List of ThoughtDetail AssetRefs for save loading")]
public class ThoughtAssetReferenceIndex : AssetReferenceIndex
{
    [AssetReferenceUILabelRestriction("thought")]
    [SerializeField] List<AssetReference> m_thoughtAssetRefs = default;
    public override List<AssetReference> AssetReferences => m_thoughtAssetRefs;
}
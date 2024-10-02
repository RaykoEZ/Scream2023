using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// List of DialogueNode AssetRefs for save/loading
public abstract class AssetReferenceIndex : ScriptableObject
{
    public abstract List<AssetReference> AssetReferences { get; }
}

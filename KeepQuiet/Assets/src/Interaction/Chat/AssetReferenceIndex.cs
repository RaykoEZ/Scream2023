using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class AssetReferenceIndex : ScriptableObject
{
    public abstract List<AssetReference> AssetReferences { get; }
    // user filename as id, unique name enforced
    public virtual AssetReference Find(string name) 
    {
        return AssetReferences.Find((t) => t.Asset.name == name);
    }
}// List of DialogueNode AssetRefs for save/loading

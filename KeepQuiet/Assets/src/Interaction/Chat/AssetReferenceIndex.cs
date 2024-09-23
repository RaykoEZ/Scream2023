using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
// List of DialogueNode AssetRefs for save/loading
public abstract class AssetReferenceIndex : ScriptableObject
{
    public abstract List<AssetReference> AssetReferences { get; }
    // user filename as id, unique name enforced
    public virtual AssetReference FindByAssetName(string name) 
    {
        return AssetReferences.Find((t) => t.Asset.name == name);
    }
    public virtual AssetReference FindByAssetRefPath(AssetReference assetRef)
    {
        return AssetReferences.Find((t) => t.AssetGUID == assetRef.AssetGUID);
    }
}

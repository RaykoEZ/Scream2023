using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
// For loading a list of addressables from a list of asset references in save file
public class AddressableContainer<T> where T : UnityEngine.Object
{
    List<T> m_loaded = new List<T>();
    bool m_inProgress = false;
    bool m_overwriteOnLoad = false;
    Action<List<T>> onLoadedCallback;
    public List<T> LoadedAssets => m_loaded;
    public static List<AssetReference> GetAssetReferenceList(
        AssetReferenceIndex index, AddressableContainer<T> container)
    {
        List<AssetReference> ret = new List<AssetReference>();
        AssetReference i;
        // Clear old list
        // go through list of current history and collect asset references
        foreach (var item in container.LoadedAssets)
        {
            i = index.Find(item.name);
            if (i == null) continue;
            ret.Add(i);
        }
        return ret;
    }
    public void LoadAssetAsync(List<AssetReference> toLoad, bool overwrite = false, Action<List<T>> onFinish = null) 
    {
        if (m_inProgress) return;
        m_inProgress = true;
        m_overwriteOnLoad = overwrite;
        onLoadedCallback = onFinish;
        var op = Addressables.LoadResourceLocationsAsync(toLoad, Addressables.MergeMode.Union);
        op.Completed += OnLocationLoaded;
    }
    public void Clear() 
    {
        foreach (var item in m_loaded)
        {
            Addressables.Release(item);
        }
        m_loaded.Clear();
    }
    void OnLocationLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj) 
    {
        var locations = obj.Result;
        var op = Addressables.LoadAssetsAsync<T>(locations, null);
        op.Completed += OnAssetLoaded;
    }
    void OnAssetLoaded(AsyncOperationHandle<IList<T>> obj) 
    {
        var result = obj.Result.ToList();
        if (m_overwriteOnLoad) 
        {
            Clear();
            m_loaded = result;
        }
        else 
        {
            m_loaded.AddRange(result);
        }
        m_inProgress = false;
        onLoadedCallback?.Invoke(LoadedAssets);
        onLoadedCallback = null;
    }
}

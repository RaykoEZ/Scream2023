using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
// For loading a list of addressables from a list of asset references in save file
public class AddressableContainer<T> where T : UnityEngine.Object
{
    List<T> m_loaded = new List<T>();
    bool m_inProgress = false;
    int m_loadedCount;
    int m_numToLoad;
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
    public void LoadAssetAsync(List<AssetReference> toLoad, Action<List<T>> onFinish = null)
    {
        if (m_inProgress) return;
        m_inProgress = true;
        m_loadedCount = 0;
        m_numToLoad = toLoad.Count;
        onLoadedCallback = onFinish;
        foreach (var item in toLoad)
        {
            // if already loaded, skip
            var op = item.LoadAssetAsync<T>();
            op.Completed += OnAssetLoaded;
        }
    }
    public void Clear() 
    {
        foreach (var item in m_loaded)
        {
            Addressables.Release(item);
        }
        m_loaded.Clear();
    }
    void OnAssetLoaded(AsyncOperationHandle<T> obj) 
    {
        var result = obj.Result;
        m_loaded.Add(result);
        // increment in async
        Interlocked.Increment(ref m_loadedCount);
        // count to check if we finished loading or not
        if (m_loadedCount == m_numToLoad) 
        {
            m_inProgress = false;
            onLoadedCallback?.Invoke(LoadedAssets);
            onLoadedCallback = null;
        }
    }
}

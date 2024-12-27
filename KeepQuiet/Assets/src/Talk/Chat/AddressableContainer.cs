using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
// For loading a list of addressables from a list of asset references in save file
public class AddressableContainer<T> where T : UnityEngine.Object
{
    bool m_inProgress = false;
    int m_loadedCount;
    int m_numToLoad;
    Action<List<T>> onLoadedCallback;
    List<AssetReference> m_assetRefs = new List<AssetReference>();
    List<T> m_loaded = new List<T>();
    public List<T> LoadedAssets => m_loaded;
    public List<AssetReference> AssetRefs => m_assetRefs;
    public void LoadAssetAsync(List<AssetReference> toLoad, Action<List<T>> onFinish = null)
    {
        if (m_inProgress) return;
        m_inProgress = true;
        m_loadedCount = 0;
        m_assetRefs.AddRange(toLoad);
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
        foreach (var item in m_assetRefs)
        {
            item.ReleaseAsset();
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

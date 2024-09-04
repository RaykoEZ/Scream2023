using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
// For loading a list of addressables from a list of asset references in save file
public class AdressableAssetContainer<T> where T : UnityEngine.Object
{
    List<T> m_loaded = default;
    bool m_inProgress = false;
    Action<List<T>> onLoadedCallback;
    public List<T> LoadedAssets => m_loaded;
    public void LoadChatHistoryAsync(List<AssetReference> toLoad, Action<List<T>> onFinish = null) 
    {
        if (m_inProgress) return;
        m_inProgress = true;
        onLoadedCallback = onFinish;
        var op = Addressables.LoadResourceLocationsAsync(toLoad, Addressables.MergeMode.Union);
        op.Completed += OnLocationLoaded;
    }
    void OnLocationLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj) 
    {
        var locations = obj.Result;
        var op = Addressables.LoadAssetsAsync<T>(locations, null);
        op.Completed += OnAssetLoaded;
    }
    void OnAssetLoaded(AsyncOperationHandle<IList<T>> obj) 
    {
        m_loaded = obj.Result.ToList();
        m_inProgress = false;
        onLoadedCallback?.Invoke(LoadedAssets);
        onLoadedCallback = null;
    }
}

using B83.Win32;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ExternalFileDropInfo
{
    public string file;
    public FileInfo fileInfo;
    public Vector2 pos;
}
public delegate void ExternalFileDropped(ExternalFileDropInfo dropInfo);
public class ExternalFileEventReceiver : MonoBehaviour
{
    public event ExternalFileDropped FileDropped;
    private void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }
    private void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }
    void OnFiles(List<string> aFiles, Vector2 aPos)
    {
        string file = "";
        FileInfo fi = new FileInfo("");
        foreach (var f in aFiles)
        {
            fi = new FileInfo(f);
            string ext = fi.Extension.ToLower();
            // detect file extensions to respond to
            if (ext == ".txt" || ext == ".cogni" || ext == ".jpeg" || ext == ".png")
            {
                file = f;
                break;
            }
        }
        // If the user dropped a supported file, create a DropInfo and pass to other listeners
        if (file != "")
        {
            var info = new ExternalFileDropInfo
            {
                file = file,
                fileInfo = fi,
                pos = new Vector2(aPos.x, aPos.y)
            };
            FileDropped?.Invoke(info);
        }
    }
}
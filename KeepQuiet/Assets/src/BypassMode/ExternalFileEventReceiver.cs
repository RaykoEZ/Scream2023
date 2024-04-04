using B83.Win32;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ExternalFileDropInfo
{
    public string content;
    public FileInfo fileInfo;
    public Vector2 pos;
}
public delegate void ExternalFileDropped(ExternalFileDropInfo dropInfo);
public class ExternalFileEventReceiver : MonoBehaviour
{
    DefaultFileValidor m_isValid = new DefaultFileValidor();
    protected virtual IFileValidator Validator => m_isValid;
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
        FileInfo fi = null;
        foreach (var f in aFiles)
        {
            fi = new FileInfo(f);
            string ext = fi.Extension.ToLower();
            // detect file extensions to respond to
            if (ext == ".txt" || ext == ".cogni" || ext == ".jamm" || ext == ".key" || ext == ".jpeg" || ext == ".png")
            {
                file = f;
                break;
            }
        }
        // check if file dropped is what we want
        if (Validator.Validate(fi, file))
        {
            // If the user dropped a supported file, create a DropInfo and pass to other listeners
            var info = new ExternalFileDropInfo
            {
                content = file,
                fileInfo = fi,
                pos = new Vector2(aPos.x, aPos.y)
            };
            FileDropped?.Invoke(info);
        }
    }
}
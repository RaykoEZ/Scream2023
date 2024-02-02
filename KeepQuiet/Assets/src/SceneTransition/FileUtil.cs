using System;
using System.IO;
using System.Text;
using UnityEngine;

internal static class FileUtil 
{
    internal static string s_gamestateSavePath = Application.persistentDataPath;
    internal static string s_desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    // Uses UTF8 to encode string
    internal static void EncodeTextTo(string path, string folderName, string fileName, string content) 
    {
        if (string.IsNullOrWhiteSpace(fileName)) { return; }
        TryMakeFolder(path, folderName);
        byte[] encoded = Encoding.UTF8.GetBytes(content);
        string resultPath = $"{path}/{folderName}/{fileName}";
        File.WriteAllBytes(resultPath, encoded);
    }
    // Writes New raw text fil to desktop
    internal static void RawTextTo(string path, string folderName, string fileName, string[] content)
    {
        if (string.IsNullOrWhiteSpace(fileName)) { return; }

        TryMakeFolder(path, folderName);
        string resultPath = $"{path}/{folderName}/{fileName}";
        if (File.Exists(resultPath))
        {
            return;
        }
        StreamWriter sw = File.CreateText(resultPath);
        foreach (var line in content)
        {
            sw.WriteLine(line);
        }
        sw.Close();
    }
    internal static void ClearTextFile(string path) 
    {
        File.WriteAllText(path, string.Empty);
    }
    internal static void AppendToTextFile(string path, string[] content) 
    {
        using (StreamWriter sw = new StreamWriter(path)) 
        {
            foreach (var line in content)
            {
                sw.WriteLine(line);
            }
        }
    }
    internal static void PngImageTo(string path, string folderName, string fileName, Texture2D image)
    {
        if (string.IsNullOrWhiteSpace(fileName)) { return; }
        TryMakeFolder(path, folderName);
        string resultPath = $"{path}/{folderName}";
        byte[] bytes = image.EncodeToPNG();
        File.WriteAllBytes(resultPath + "/" + fileName + ".png", bytes);
    }
    internal static void TryMakeFolder(string path, string folderName) 
    {
        if (string.IsNullOrWhiteSpace(folderName)) 
        {
            return;
        }
        string resultPath = $"{path}/{folderName}";
        if (!Directory.Exists(resultPath))
        {
            Directory.CreateDirectory(resultPath);
        }
    }
}


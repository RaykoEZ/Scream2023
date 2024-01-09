using System;
using System.IO;
using System.Text;
using UnityEngine;

internal static class FilesUtil 
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    // Uses UTF8 to encode string
    internal static void EncodeTextToDesktop(string folderName, string fileName, string content) 
    {
        TryMakeDesktopFolder(folderName);
        byte[] encoded = Encoding.UTF8.GetBytes(content);
        string resultPath = $"{desktopPath}/{folderName}" + "/" + fileName + ".txt";
        File.WriteAllBytes(resultPath, encoded);
    }
    // Writes New raw text fil to desktop
    internal static void RawTextToDesktop(string folderName, string fileName, string[] content) 
    {
        TryMakeDesktopFolder(folderName);
        string resultPath = $"{desktopPath}/{folderName}" + "/" + fileName + ".txt";
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
    internal static void ImageToDesktop(string folderName, string filename, Texture2D image) 
    {
        TryMakeDesktopFolder(folderName);
        string resultPath = $"{desktopPath}/{folderName}";
        byte[] bytes = image.EncodeToPNG();
        File.WriteAllBytes(resultPath + "/" + filename + ".png", bytes);
    }
    internal static void TryMakeDesktopFolder(string folderName) 
    {
        string resultPath = $"{desktopPath}/{folderName}";
        if (!Directory.Exists(resultPath))
        {
            Directory.CreateDirectory(resultPath);
        }
    }
}


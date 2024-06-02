using UnityEngine;

public class FileWriter 
{
    public void WriteTextToDesktop(string filename, string folder, string content, bool encode = false) 
    {
        if (encode) 
        {
            FileUtil.UTF8EncodeTextTo(FileUtil.s_desktopPath, folder, filename, content);
        } 
        else 
        {
            FileUtil.RawTextTo(FileUtil.s_desktopPath, folder, filename, new string[] { content });
        }
    }

    public void WriteCogniToDesktop(string filename, string folder, string content) 
    {
        FileUtil.UTF8EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.cogni", content);
    }
    public void WriteKeyToDesktop(string filename, string folder, string content)
    {
        FileUtil.UTF8EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.key", content);
    }
    public void WriteJammerToDesktop(string filename, string folder, string content)
    {
        FileUtil.UTF8EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.jamm", content);
    }
    public void SendPngToDesktop(string filename, string foldername, Texture2D texture) 
    {
        FileUtil.PngImageTo(FileUtil.s_desktopPath, foldername, filename, texture);
    }
}
using System.IO;
public interface IFileValidator
{
    string AcceptedFileExtension { get; }
    public bool Validate(FileInfo info, string content);
}
public class DefaultFileValidor : IFileValidator
{
    public virtual string AcceptedFileExtension => "";
    public virtual bool Validate(FileInfo info, string content)
    {
        return info != null && content != "";
    }
}
public class KeyFileValidator : DefaultFileValidor, IFileValidator 
{
    public override string AcceptedFileExtension => ".key";
    public override bool Validate(FileInfo info, string content)
    {
        bool ret = base.Validate(info, content);
        bool nameCheck = 
            info.Name == "Recovery_E1C6_12011531.key" ||
            info.Name == "Recovery_E1C6_12011237.key" ||
            info.Name == "Recovery_E1C5_12031823.key" ||
            info.Name == "temp_bypass.key";
        ret &= nameCheck;
        return ret;
    }
}
public class WatchFileValidator : DefaultFileValidor, IFileValidator
{
    public override string AcceptedFileExtension => ".app";
    public override bool Validate(FileInfo info, string content)
    {
        bool ret = base.Validate(info, content);
        bool nameCheck =
            info.Name == "watch.app";
        ret &= nameCheck;
        return ret;
    }
}
public class JammerFileValidator : DefaultFileValidor, IFileValidator
{
    public override string AcceptedFileExtension => ".jamm";

    public override bool Validate(FileInfo info, string content)
    {
        bool ret = base.Validate(info, content);
        ret &= info.Name == "jam.jamm";
        return ret;
    }
}

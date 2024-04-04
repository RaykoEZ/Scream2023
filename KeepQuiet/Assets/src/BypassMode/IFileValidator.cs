using System.IO;

public interface IFileValidator
{
    public bool Validate(FileInfo info, string content);
}
public class DefaultFileValidor : IFileValidator
{
    public virtual bool Validate(FileInfo info, string content)
    {
        return info != null && content != "";
    }
}
public class JammerFileValidator : DefaultFileValidor, IFileValidator
{
    public override bool Validate(FileInfo info, string content)
    {
        bool ret = base.Validate(info, content);
        ret &= info.Name == "jam.jamm";
        return ret;
    }
}

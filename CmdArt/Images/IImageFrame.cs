namespace CmdArt.Images
{
    public interface IImageFrame
    {
        ISize TotalSize { get; }
        ImageBrush[,] GetRegionContents(Region region);
        object GetProperty(string propertyName);
    }
}

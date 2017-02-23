namespace CmdArt.Images
{
    public interface IImageFrame
    {
        Region TotalRegion { get; }
        ImageBrush[,] GetRegionContents(Region region);
        object GetProperty(string propertyName);
    }
}

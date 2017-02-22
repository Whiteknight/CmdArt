namespace CmdArt.Rendering.Images
{
    public interface IImageFrame
    {
        Region TotalRegion { get; }
        ConsolePixel[,] GetRegionContents(Region region);
        object GetProperty(string propertyName);
    }
}

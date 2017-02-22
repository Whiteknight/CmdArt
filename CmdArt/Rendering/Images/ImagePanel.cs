//using RichCmd.Screen;

//namespace RichCmd.Rendering.Images
//{
//    // An area on the console to render to.
//    public class ImagePanel : Panel
//    {
//        public IImageBuffer ImageBuffer { get; set; }

//        public int ImageTop { get; set; }

//        public int ImageLeft { get; set; }

//        public override void RenderToBuffer(IScreenBuffer buffer, Region region)
//        {
//            IImageFrame imageFrame = ImageBuffer.GetBuffer(0);
//            ConsolePixel[,] pixels = imageFrame.GetRegionContents(new Region(ImageLeft, ImageTop, region.Width, region.Height));
//            for (int j = 0; j < region.Height && j < imageFrame.TotalRegion.Height; j++)
//            {
//                for (int i = 0; i < region.Width && i < imageFrame.TotalRegion.Width; i++)
//                {
//                    buffer.Set(region.Left + i, region.Top + j, pixels[j, i].Palette, pixels[j, i].PrintableCharacter);
//                }
//            }
//        }

//    }
//}

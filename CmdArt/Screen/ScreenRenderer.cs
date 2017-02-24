namespace CmdArt.Screen
{
    public class ScreenRenderer
    {
        public void Render(IPixelBuffer buffer, IConsoleWrapper wrapper, bool force = false)
        {
            bool continues = false;
            byte currentColor = buffer.DefaultPalette.ByteValue;
            wrapper.SetColor(currentColor);
            for (int j = 0; j < buffer.Size.Height; j++)
            {
                for (int i = 0; i < buffer.Size.Width; i++)
                {
                    bool isUpdated = false;
                    if (force || buffer.IsUpdated(i, j))
                    {
                        isUpdated = true;
                        buffer.SetUpdated(i, j, false);
                    }
                    if (isUpdated && buffer.IsVisible(i, j))
                    {
                        if (!continues)
                            wrapper.SetCursorPosition(i, j);
                        var pixel = buffer.Get(i, j);
                        if (pixel.Color != currentColor)
                        {
                            currentColor = pixel.Color;
                            wrapper.SetColor(currentColor);
                        }
                        wrapper.Write(pixel.Character);
                        continues = true;
                    }
                    else
                        continues = false;
                }
                continues = false;
            }
        }

        //public void Render(ScreenSpace space, IConsoleWrapper wrapper)
        //{
        //    bool continues = false;
        //    byte currentColor = space.BaseLayer.Buffer.DefaultPalette.ByteValue;
        //    wrapper.SetColor(currentColor);
        //    for (int j = 0; j < space.Size.Height; j++)
        //    {
        //        for (int i = 0; i < space.Size.Width; i++)
        //        {
        //            for (int x = space.Count - 1; x >= 0; x--)
        //            {
        //                var buffer = space[x].Buffer;
        //                int left = i - space[x].Offset.Left;
        //                int top = j - space[x].Offset.Top;

        //                if (!buffer.IsVisible(left, top))
        //                {
        //                    continues = false;
        //                    continue;
        //                }

        //                if (buffer.IsUpdated(left, top))
        //                {
        //                    buffer.SetUpdated(left, top, false);
        //                    if (!continues)
        //                        wrapper.SetCursorPosition(i, j);

        //                    var pixel = buffer.Get(i, j);
        //                    byte color = pixel.Color;
        //                    if (color != currentColor)
        //                    {
        //                        currentColor = color;
        //                        wrapper.SetColor(currentColor);
        //                    }
        //                    wrapper.Write(pixel.Character);
        //                    continues = true;
        //                }
        //                else
        //                {
        //                    continues = false;
        //                }
        //                break;
        //            }
        //        }
        //        continues = false;
        //    }
        //}
    }
}

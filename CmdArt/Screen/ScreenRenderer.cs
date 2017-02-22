namespace CmdArt.Screen
{
    public class ScreenRenderer
    {
        public void Render(IPixelBuffer buffer, ConsoleScreen screen)
        {
            bool continues = false;
            byte currentColor = buffer.DefaultPalette.ByteValue;
            screen.SetColor(currentColor);
            for (int j = 0; j < buffer.Size.Height; j++)
            {
                for (int i = 0; i < buffer.Size.Width; i++)
                {
                    bool isUpdated = false;
                    if (buffer.IsUpdated(i, j))
                    {
                        isUpdated = true;
                        buffer.SetUpdated(i, j, false);
                    }
                    if (isUpdated && buffer.IsVisible(i, j))
                    {
                        if (!continues)
                            screen.SetCursorPosition(i, j);
                        if (buffer.GetColorByte(i, j) != currentColor)
                        {
                            currentColor = buffer.GetColorByte(i, j);
                            screen.SetColor(currentColor);
                        }
                        screen.Write(buffer.GetCharacter(i, j));
                        continues = true;
                    }
                    else
                        continues = false;
                }
                continues = false;
            }
        }

        public void Render(ScreenSpace space, ConsoleScreen screen)
        {
            bool continues = false;
            byte currentColor = space.BaseLayer.Buffer.DefaultPalette.ByteValue;
            screen.SetColor(currentColor);
            for (int j = 0; j < space.Size.Height; j++)
            {
                for (int i = 0; i < space.Size.Width; i++)
                {
                    for (int x = space.Count - 1; x >= 0; x--)
                    {
                        var buffer = space[x].Buffer;
                        int left = i - space[x].Offset.Left;
                        int top = j - space[x].Offset.Top;

                        if (!buffer.IsVisible(left, top))
                        {
                            continues = false;
                            continue;
                        }

                        if (buffer.IsUpdated(left, top))
                        {
                            buffer.SetUpdated(left, top, false);
                            if (!continues)
                                screen.SetCursorPosition(i, j);

                            byte color = buffer.GetColorByte(left, top);
                            if (color != currentColor)
                            {
                                currentColor = color;
                                screen.SetColor(currentColor);
                            }
                            screen.Write(buffer.GetCharacter(left, top));
                            continues = true;
                        }
                        else
                        {
                            continues = false;
                        }
                        break;
                    }
                }
                continues = false;
            }
        }
    }
}

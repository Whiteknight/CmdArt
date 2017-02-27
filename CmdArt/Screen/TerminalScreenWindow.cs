﻿namespace CmdArt.Screen
{
    public class TerminalScreenWindow
    {
        private readonly TerminalScreen _screen;

        public TerminalScreenWindow(TerminalScreen screen, Region screenRegion)
        {
            if (screen == null)
                throw new System.ArgumentNullException(nameof(screen));

            _screen = screen;
            Size = screenRegion.RegionSize;
            ScreenLocation = screenRegion.RegionLocation;

            SourceBuffer = screen.BufferFactory.Create(screenRegion.RegionSize);
            SourceLocation = Location.Origin;
        }

        public IPixelBuffer SourceBuffer { get; private set; }
        public ILocation SourceLocation { get; set; }
        public ILocation ScreenLocation { get; }
        public ISize Size { get; }
        public Region ScreenRegion => new Region(ScreenLocation, Size);
        public Region SourceRegion => new Region(SourceLocation, Size);

        public void SetSourceBuffer(IPixelBuffer sourceBuffer, ILocation sourceLocation = null)
        {
            if (sourceBuffer == null)
                throw new System.ArgumentNullException(nameof(sourceBuffer));

            SourceBuffer = sourceBuffer;
            SourceLocation = sourceLocation ?? Location.Origin;
            // TODO: Check to make sure that the region is completely within the bounds of the source buffer
        }

        public void Render()
        {
            if (SourceBuffer == null)
                return;

            for (int j = 0; j < Size.Height; j++)
            {
                int srcTop = SourceLocation.Top + j;
                int trgTop = ScreenLocation.Top + j;
                // TODO: Bounds check. Terminate early if we've gone beyond the bounds of either buffer

                for (int i = 0; i < Size.Width; i++)
                {
                    int srcLeft = SourceLocation.Left + i;
                    int trgLeft = ScreenLocation.Left + i;
                    // TODO: Bounds check. Terminate early if we've gone beyond the bounds of either buffer

                    if (!SourceBuffer.IsUpdated(i, j))
                        continue;

                    var pixel = SourceBuffer.Get(srcLeft, srcTop);
                    _screen.Buffer.Set(trgLeft, trgTop, pixel.Color, pixel.Character);
                }
            }
        }
    }
}
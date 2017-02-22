//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RichCmd.Screen;

//namespace RichCmd.Rendering
//{
//    public class RenderableState
//    {
//        private readonly IScreenBuffer _parentBuffer;

//        public RenderableState(IRenderable renderable, IScreenBuffer buffer, Palette defaultPalette, Region region)
//        {
//            Renderable = renderable;
//            _parentBuffer = buffer;
//            Buffer = buffer.GetRegionBuffer(region);
//            DefaultPalette = defaultPalette;
//            Region = region;
//        }

//        public IRenderable Renderable { get; private set; }
//        public IScreenBuffer Buffer { get; private set; }

//        public Palette DefaultPalette
//        {
//            get { return Buffer.DefaultPalette; }
//            set { Buffer.DefaultPalette = value; }
//        }

//        public Region Region { get; private set; }

//        public void MoveTo(Region newRegion)
//        {
//            var palette = Buffer.DefaultPalette;
//            Buffer = _parentBuffer.GetRegionBuffer(newRegion);
//            Region = newRegion;
//            Buffer.DefaultPalette = palette;
//        }

//        public void RenderToBuffer()
//        {
//            Renderable.Render(Buffer);
//        }
//    }
//}

//using CmdArt.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CmdArt.Screen
//{
//    // TODO: We don't want this, but some of this can be moved to the new Window abstraction
//    public class ScreenSpace
//    {
//        private const string BaseLayerName = "Base";
//        private readonly ISize _size;
//        private readonly List<ILayer> _layers;

//        public ScreenSpace()
//            : this(Region.Window)
//        {
//        }

//        public ScreenSpace(ISize size)
//        {
//            _size = size;
//            _layers = new List<ILayer> {
//                new BaseLayerClass(size)
//            };
//        }

//        public ILayer this[int index]
//        {
//            get { return _layers[index]; }
//        }

//        public ILayer this[string name]
//        {
//            get { return _layers.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
//        }

//        public ILayer BaseLayer
//        {
//            get { return _layers[0]; }
//        }

//        public ILayer TopLayer
//        {
//            get { return _layers[_layers.Count - 1]; }
//        }

//        public ISize Size
//        {
//            get { return _size; }
//        }

//        public ILayer CreateLayer(string name, ILocation offset, ISize size)
//        {
//            // TODO: Names should be unique
//            var buffer = new PixelBuffer(Size);
//            var layer = new Layer(name ?? DefaultLayerName(), offset, buffer);
//            _layers.Add(layer);
//            return layer;
//        }

//        private int _nextLayerIdx;

//        private string DefaultLayerName()
//        {
//            string name = string.Format("Layer " + _nextLayerIdx);
//            _nextLayerIdx++;
//            return name;
//        }

//        public int Count
//        {
//            get { return _layers.Count; }
//        }

//        //private void ForceRender(int i, int j)
//        //{
//        //    for (int x = _buffers.Count - 1; x >= 0; x--)
//        //    {
//        //        var buffer = _buffers[x];
//        //        if (buffer.IsVisible(i, j))
//        //        {
//        //            buffer.SetUpdated(i, j, true);
//        //            break;
//        //        }
//        //    }
//        //}

//        public IPixelBuffer Flatten()
//        {
//            var newBuffer = new PixelBuffer(Size);
//            for (int j = 0; j < Size.Height; j++)
//            {
//                for (int i = 0; i < Size.Width; i++)
//                {
//                    for (int x = _layers.Count - 1; x >= 0; x--)
//                    {
//                        int left = i - _layers[x].Offset.Left;
//                        int top = j - _layers[x].Offset.Top;
//                        var buffer = _layers[x].Buffer;
//                        if (!buffer.IsVisible(left, top))
//                            continue;

//                        var pixel = buffer.Get(left, top);
//                        newBuffer.Set(left, top, pixel.Color, pixel.Character);
//                        break;
//                    }
//                }
//            }
//            return newBuffer;
//        }

//        private class BaseLayerClass : ILayer
//        {
//            public BaseLayerClass(ISize size)
//            {
//                Name = BaseLayerName;
//                Offset = Location.Origin;
//                Buffer = new PixelBuffer(size);
//                Renderables = new List<IRenderable>();
//                LayerDecorations = new List<IDecoration>();
//            }

//            public string Name { get; private set; }
//            public ILocation Offset { get; private set; }
//            public ISize Size { get { return Buffer.Size; } }
//            public IPixelBuffer Buffer { get; private set; }
//            public void MoveTo(ILocation newLocation)
//            {
//            }

//            public IList<IRenderable> Renderables { get; private set; }
//            public IList<IDecoration> LayerDecorations { get; private set; }

//            public void Render()
//            {
//                foreach (var renderable in Renderables)
//                    renderable.RenderTo(Buffer);
//            }
//        }

//        private class Layer : ILayer
//        {
//            public Layer(string name, ILocation offset, IPixelBuffer buffer)
//            {
//                Buffer = buffer;
//                Offset = offset;
//                Name = name;
//                Renderables = new List<IRenderable>();
//            }

//            public string Name { get; private set; }
//            public ILocation Offset { get; private set; }
//            public ISize Size { get { return Buffer.Size; } }
//            public IPixelBuffer Buffer { get; private set; }
//            public void MoveTo(ILocation newLocation)
//            {
//                Offset = newLocation;
//                foreach (var location in Buffer.AllVisible())
//                    Buffer.SetUpdated(location.Left, location.Top, true);
//            }

//            public IList<IRenderable> Renderables { get; private set; }

//            public IList<IDecoration> LayerDecorations
//            {
//                get
//                {
//                    throw new NotImplementedException();
//                }
//            }

//            public void Render()
//            {
//                foreach (var renderable in Renderables)
//                    renderable.RenderTo(Buffer);
//            }
//        }
//    }
//}

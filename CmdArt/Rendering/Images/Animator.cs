//using System;
//using System.Diagnostics;
//using System.Threading;

//namespace CmdUi.ImageBuffer
//{
//    public class Animator
//    {
//        private readonly ImagePanel _region;
//        private readonly IImageBuffer _imageBuffer;
//        private int _frame;
//        private bool _alreadyDrawn;

//        public Animator(ImagePanel region, IImageBuffer ImageBuffer)
//        {
//            _region = region;
//            _imageBuffer = ImageBuffer;
//        }

//        public long Step()
//        {
//            if (_alreadyDrawn && _image.NumberOfBuffers == 1)
//                return -1;

//            IImageFrame buffer = _imageBuffer.GetBuffer(_frame);
//            _region.Draw(buffer);

//            _frame = (_frame + 1) % _imageBuffer.NumberOfBuffers;
//            _alreadyDrawn = true;
            
//            object delayObj = buffer.GetProperty(ImagePropertyConstants.GifFrameTimeMs);
//            if (delayObj == null)
//                return -1;
//            return (long)delayObj;
//        }

//        public void Animate(Func<bool> shouldStop)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            while (!shouldStop())
//            {
//                stopwatch.Start();
//                long frameMs = Step();
//                stopwatch.Stop();
//                long elapsed = stopwatch.ElapsedMilliseconds;
//                stopwatch.Reset();
//                if (elapsed < frameMs)
//                    Thread.Sleep((int)(frameMs - elapsed));
//            }
//        }

//        // TODO: Async/threaded methods that will continue to run in the background
//    }
//}

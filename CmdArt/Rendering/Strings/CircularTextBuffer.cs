using System.Collections;
using System.Collections.Generic;

namespace CmdArt.Rendering.Strings
{
    public class CircularTextBuffer : IEnumerable<string>
    {
        private readonly int _length;
        private readonly string[] _buffer;
        private int _idx;
        private int _startIdx;

        public CircularTextBuffer(int length)
        {
            _buffer = new string[length + 1];
            _idx = 0;
            _startIdx = 0;
            _length = length;
        }

        public void AddLine(string s)
        {
            _idx = (_idx + 1) % _buffer.Length;

            if (_idx == _startIdx)
            {
                _startIdx = (_startIdx + 1) % _length;
                _buffer[_startIdx] = null;
            }

            _buffer[_idx] = s ?? string.Empty;
        }

        private bool IsEmpty => _idx == _startIdx;

        public IEnumerator<string> GetEnumerator()
        {
            return new Enumerator(_buffer, _startIdx);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(_buffer, _startIdx);
        }

        private class Enumerator : IEnumerator<string>
        {
            private readonly string[] _buffer;
            private readonly int _startIdx;
            private int _idx;

            public Enumerator(string[] buffer, int startIdx)
            {
                _idx = startIdx;
                _buffer = buffer;
                _startIdx = startIdx % _buffer.Length;
            }

            public string Current => _buffer[_idx];

            object IEnumerator.Current => _buffer[_idx];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _idx = (_idx + 1) % _buffer.Length;
                if (_idx == _startIdx || _buffer[_idx] == null)
                    return false;
                return true;
            }

            public void Reset()
            {
                _idx = _startIdx;
            }
        }
    }
}

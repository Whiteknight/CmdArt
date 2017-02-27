using System.Collections;
using System.Collections.Generic;

namespace CmdArt.Rendering.Strings
{
    public class CircularTextBuffer : IEnumerable<string>
    {
        private readonly string[] _buffer;
        private int _idx;
        private int _length;

        public CircularTextBuffer(int length)
        {
            _buffer = new string[length];
            _idx = 0;
            _length = length;
        }

        public void AddLine(string s)
        {
            int idx = _idx;
            idx = (idx + 1) % _length;
            _idx = idx;
            _buffer[idx] = s ?? string.Empty;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new Enumerator(_buffer, (_idx + 1));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(_buffer, _idx);
        }

        private class Enumerator : IEnumerator<string>
        {
            private readonly string[] _buffer;
            private readonly int _startIdx;
            private int _idx;

            public Enumerator(string[] buffer, int startIdx)
            {
                _buffer = buffer;
                _startIdx = startIdx;
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

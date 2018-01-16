using System.Text;

namespace CmdArt.Screen
{
    public class PixelBufferSerializer
    {
        public int[,] Serialize(IPixelBuffer buffer)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            var array = new int[buffer.Size.Width, buffer.Size.Height];

            for (uint j = 0; j < buffer.Size.Height; j++)
            {
                for (uint i = 0; i < buffer.Size.Width; i++)
                {
                    var pixel = buffer.Get(i, j);
                    array[i, j] = pixel.Pack();
                }
            }
            return array;
        }

        public string SerializeToCSharpCode(IPixelBuffer buffer, string varName)
        {
            if (buffer == null)
                throw new System.ArgumentNullException(nameof(buffer));

            if (string.IsNullOrWhiteSpace(varName))
                throw new System.ArgumentNullException(nameof(varName));

            var builder = new StringBuilder();
            builder.AppendFormat("int[,] {0} = new int[{1},{2}];\n", varName, buffer.Size.Width, buffer.Size.Height);

            for (uint j = 0; j < buffer.Size.Height; j++)
            {
                for (uint i = 0; i < buffer.Size.Width; i++)
                {
                    var pixel = buffer.Get(i, j);
                    int packed = pixel.Pack();
                    builder.AppendFormat("{0}[{1},{2}] = {3};\n", varName, i, j, packed);
                }
            }
            return builder.ToString();
        }

        public IPixelBuffer Deserialize(int[,] array)
        {
            if (array == null)
                throw new System.ArgumentNullException(nameof(array));

            int width = array.GetLength(0);
            int height = array.GetLength(1);
            var size = new Size((uint)width, (uint)height);
            var buffer = new PixelBuffer(size);

            for (uint j = 0; j < height; j++)
            {
                for (uint i = 0; i < width; i++)
                {
                    int packed = array[i, j];
                    var pixel = ScreenPixel.FromPackedInt(packed);
                    buffer.Set(i, j, pixel.Color, pixel.Character);
                }
            }

            return buffer;
        }
    }
}

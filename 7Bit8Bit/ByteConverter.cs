using System.IO;
using System.Linq;

namespace Test_7bit8bit
{

  public static class ByteConverter
  {

    private static readonly int[] Up = new[] { 1, 3, 7, 15, 31, 63, 127 };

    private static readonly int[] Down = new[] { 254, 252, 248, 240, 224, 192, 128, 0 };


    public static byte[] To7Bit(byte[] data8Bit)
    {
      using (var output = new MemoryStream() { Position = 0 })
      {
        var carry = 0;

        foreach (var i in Enumerable.Range(0, data8Bit.Length))
        {
          var r = i % 7;

          if (r == 0 && i != 0)
          {
            output.WriteByte((byte)carry);
            carry = 0;
          }

          var integer = data8Bit[i];
          output.WriteByte((byte)(carry | ((integer & Down[r]) >> 1)));

          carry = integer & Up[r];
        }

        output.WriteByte((byte)carry);

        output.Position = 0;
        return output.ToArray();
      }
    }


    public static byte[] To8Bit(byte[] data7Bit)
    {
      using (var output = new MemoryStream() { Position = 0 })
      {
        var carry = 0;

        foreach (var i in Enumerable.Range(0, data7Bit.Length))
        {
          var r = i % 8;

          var integer = data7Bit[i];

          if (r != 0)
          {
            output.WriteByte((byte)(carry | (integer & Up[r - 1])));
          }

          carry = (integer << 1) & Down[r];
        }

        output.Position = 0;
        return output.ToArray();
      }
    }
  }
}

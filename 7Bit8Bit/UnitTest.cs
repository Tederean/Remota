using NUnit.Framework;
using System;

namespace Test_7bit8bit
{

  public class Tests
  {

    private byte[] GetTestData(int sizeInKb)
    {
      var bytes = new byte[sizeInKb * 1024];

      new Random().NextBytes(bytes);

      return bytes;
    }

    [Test]
    public void LegacyTest()
    {
      var reference = GetTestData(10);

      var bytes7bit = ByteConverter.To7Bit(reference);
      var bytes8bit = ByteConverter.To8Bit(bytes7bit);

      CollectionAssert.AreEqual(reference, bytes8bit);
    }
  }
}
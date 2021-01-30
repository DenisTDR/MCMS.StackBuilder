using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace MCMS.StackBuilder.Generators
{
    public class ArchiveHelper
    {
        public static void WriteGzippedTarStream(Stream dest, Dictionary<string, string> genResult)
        {
            using var targetStream = new GZipOutputStream(dest);
            WriteTarStream(targetStream, genResult);
            targetStream.IsStreamOwner = false;
            targetStream.Close();
        }

        public static void WriteTarStream(Stream dest, Dictionary<string, string> genResult)
        {
            var tarOutputStream = new TarOutputStream(dest, Encoding.UTF8);
            foreach (var (key, value) in genResult)
            {
                var entry = TarEntry.CreateTarEntry(key + ".cs");
                var bytes = Encoding.UTF8.GetBytes(value);
                entry.Size = bytes.Length;

                tarOutputStream.PutNextEntry(entry);
                tarOutputStream.Write(bytes);
                tarOutputStream.CloseEntry();
            }

            tarOutputStream.IsStreamOwner = false;
            tarOutputStream.Close();
        }
    }
}
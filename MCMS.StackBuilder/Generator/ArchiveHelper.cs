using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace MCMS.StackBuilder.Generator
{
    public class ArchiveHelper
    {
        public static void WriteGzippedTarStream(Stream dest, Dictionary<string, string> genResult, string dir)
        {
            using var targetStream = new GZipOutputStream(dest);
            WriteTarStream(targetStream, genResult, dir);
            targetStream.IsStreamOwner = false;
            targetStream.Close();
        }

        public static void WriteTarStream(Stream dest, Dictionary<string, string> genResult, string dir)
        {
            var tarOutputStream = new TarOutputStream(dest, Encoding.UTF8);
            foreach (var (key, value) in genResult)
            {
                var fileName = key + ".cs";
                if (!string.IsNullOrEmpty(dir))
                {
                    fileName = dir + "/" + fileName;
                }

                var entry = TarEntry.CreateTarEntry(fileName);
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
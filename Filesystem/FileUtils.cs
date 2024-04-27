using System.IO;

namespace MarkusSecundus.Utils.Filesystem
{
    public static class FileUtils
    {
        public static string GetUnoccupiedFilePathIncremental(string originalPath)
        {
            if (!File.Exists(originalPath))
                return originalPath;

            var extension = Path.GetExtension(originalPath);
            var pathWithoutExtension = GetPathWithoutExtension(originalPath);

            for(int i=0; i < int.MaxValue; ++i)
            {
                var candidate = $"{pathWithoutExtension}({i}){extension}";
                if (!File.Exists(candidate))
                    return candidate;
            }
            throw new IOException("Cannot create incremental file - depleted all numbers");
        }

        public static string GetPathWithoutExtension(string path)
            => Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
    }
}

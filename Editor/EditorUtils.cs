#if UNITY_EDITOR

using MarkusSecundus.Utils.Filesystem;
using System.IO;
using UnityEditor;

namespace MarkusSecundus.Utils.Editor
{
    public static class EditorUtils
    {
        public static void InstantiateBlobIntoCurrentDirectory(byte[] templateBlob, string requestedFileName)
        {
            if (Selection.assetGUIDs.Length < 1)
                throw new System.InvalidOperationException("Can only create new file if a folder is active!");
            string folderGUID = Selection.assetGUIDs[0];
            string projectFolderPath = AssetDatabase.GUIDToAssetPath(folderGUID);
            string folderDirectory = Path.GetFullPath(projectFolderPath);

            File.WriteAllBytes(FileUtils.GetUnoccupiedFilePathIncremental(Path.Combine(folderDirectory, requestedFileName)), templateBlob);

            AssetDatabase.Refresh();
        }
    }
}

#endif
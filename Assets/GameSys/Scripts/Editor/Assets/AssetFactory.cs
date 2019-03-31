using System.IO;
using UnityEditor;

namespace GameSystems.Editors.Assets
{
    public static class AssetFactory
    {
        private const string JSON_TEMPLATE = "{\n\n}";
        private const string JSON_FILE_NAME = "JsonFile";
        private const string JSON_FILE_EXTENSION = ".json";

        [MenuItem(itemName: "Assets/Create/Json", priority = 0)]
        private static void CreateJsonFile()
        {
            string path = Selection.activeObject == null ? "Assets" : AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());

            if (!Directory.Exists(path))
            {
                // A file was selected instead of a directory so parse the directory from the path.
                path = path.Substring(0, path.LastIndexOf("/"));
            }

            string fileName = string.Format("{0}{1}", JSON_FILE_NAME, JSON_FILE_EXTENSION);
            string filePath = string.Format("{0}/{1}", path, fileName);

            for (int i = 1; File.Exists(filePath); i++)
            {
                filePath = string.Format("{0}/{1}{2}{3}", path, JSON_FILE_NAME, i, JSON_FILE_EXTENSION);
            }

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(JSON_TEMPLATE);
            writer.Close();

            AssetDatabase.ImportAsset(filePath);
        }
    }
}

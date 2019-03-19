using GameSystems.Hexagons;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Editors.Gamepad
{
    [CustomPropertyDrawer(typeof(Hexagon))]
    public class HexagonPropertyDrawer : PropertyDrawer
    {
        private bool isCached = false;

        private SerializedProperty x;
        private SerializedProperty y;
        private SerializedProperty z;

        private Vector3Int coordinates;

        private string name;
        private string shortName;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CheckCache(property);

            string contentName = position.width <= 256f ? shortName : name;

            // TODO - Fix content dropping to new line if draw area is too small.

            coordinates.Set(x.intValue, y.intValue, z.intValue);
            coordinates = EditorGUI.Vector3IntField(position, contentName, coordinates);

            x.intValue = coordinates.x;
            y.intValue = coordinates.y;
            z.intValue = coordinates.z;
        }

        private bool CheckCache(SerializedProperty property)
        {
            if (!isCached)
            {
                name = property.name.ToUpper().Substring(0, 1) + property.name.Substring(1);
                shortName = name.Substring(0, 3) + "...";

                property.Next(true);
                x = property.Copy();

                property.Next(true);
                y = property.Copy();

                property.Next(true);
                z = property.Copy();

                isCached = true;
            }
            return isCached;
        }
    }
}

using UnityEngine;

namespace GameSystems.Hexagons.GridBuilders
{
    public abstract class GridBuilder : ScriptableObject,
        ISerializationCallbackReceiver
    {
        public virtual Hexagon[] Build()
        {
            throw new System.NotImplementedException("[GridBuilder] GridBuilder::Build has not been implemented.");
        }

        public virtual void OnBeforeSerialize() { }

        public virtual void OnAfterDeserialize() { }
    }
}

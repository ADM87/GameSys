using GameSystems.Hexagons;
using UnityEngine;

public class HexagonScene : MonoBehaviour
{
    [SerializeField]
    private Hexagon hexagon;

    [SerializeField]
    private HexagonMatrix matrix;

    private void Awake()
    {
        Debug.Log(matrix);
        Debug.Log(hexagon);
    }
}

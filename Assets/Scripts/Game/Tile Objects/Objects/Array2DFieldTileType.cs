using UnityEngine;
using Array2DEditor;

[System.Serializable]
[CreateAssetMenu(fileName = "Array2DFieldTileType", menuName = "Array2D/Array2DFieldTileType")]
public class Array2DFieldTileType : Array2D<FieldTileSpriteType>
{
    [SerializeField]
    CellRowExampleEnum[] cells = new CellRowExampleEnum[Consts.defaultGridSize];
    [SerializeField]
    public Sprite sprite;
    protected override CellRow<FieldTileSpriteType> GetCellRow(int idx)
    {
        return cells[idx];
    }
}

[System.Serializable]
public class CellRowExampleEnum : CellRow<FieldTileSpriteType> { }

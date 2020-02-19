using UnityEditor;
using Array2DEditor;

[CustomEditor(typeof(Array2DFieldTileType))]
public class Array2DFieldTileTypeEditor : Array2DEnumEditor<Array2DFieldTileType>
{
    // If your enum has long names, you can replace 70 by 150, for example.
    protected override int CellWidth { get { return 70; } }
    // For enums, the cell height will just change the vertical spacing. 
    protected override int CellHeight { get { return 16; } }
}

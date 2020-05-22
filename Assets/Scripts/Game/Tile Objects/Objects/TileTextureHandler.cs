using UnityEngine;
public class TileTextureHandler : MonoBehaviour {
    public Vector4 textureCellAndDimension; // bottom left corner is the start, x-hor,y-vert
    public Vector4 textureTilingAndOffset = new Vector4(1, 1, 0, 0);

    public TileTextureHandler(Vector4 cellAndDimension, Vector4 tilingAndOffset) {
        textureTilingAndOffset = tilingAndOffset;
        textureCellAndDimension = cellAndDimension;
    }
    public TileTextureHandler(Vector4 cellAndDimension) {
        textureCellAndDimension = cellAndDimension;
    }
    public TileTextureHandler() {
        textureTilingAndOffset = new Vector4(1, 1, 0, 0);
        textureCellAndDimension = new Vector4();
    }
    public void Setup(TileTextureHandler newTile) {
        textureTilingAndOffset = newTile.textureTilingAndOffset;
        textureCellAndDimension = newTile.textureCellAndDimension;
    }
    public void SetTilePosition(Vector4 cellAndDimension) {
        textureCellAndDimension = cellAndDimension;
    }
    public void UpdateTexture() {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetVector("_TextureCellDim", textureCellAndDimension);
        propertyBlock.SetVector("_TextureST", textureTilingAndOffset);
        GetComponent<SpriteRenderer>().SetPropertyBlock(propertyBlock);
    }
    void Start() {
        UpdateTexture();
    }
}
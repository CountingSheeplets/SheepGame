using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Array2DEditor;
using UnityEngine;
public class TileSpriteFactory : Singleton<TileSpriteFactory> {
    [SerializeField]
    private List<SpriteForLayout> layouts = new List<SpriteForLayout>();
    [SerializeField]
    private List<Vector2> mudSpriteAddresses = new List<Vector2>();
    [SerializeField]
    private List<SpriteForLayout> mudLayouts = new List<SpriteForLayout>();

    public Vector2 spriteSheetSize = new Vector2(8, 7);

    public static Vector4 GetTextureTile(SubFieldTile subFieldTile) {
        List<Vector2> eligibleSprites = new List<Vector2>();
        if (subFieldTile.GetParentType() != FieldTileSpriteType.mud) {
            foreach (SpriteForLayout item in Instance.layouts) {
                List<FieldTileSpriteType[, ]> typesList = item.layouts.Select(x => x.GetCells()).ToList();
                if (subFieldTile.ExportState().IsInListByMatching(typesList)) {
                    eligibleSprites.AddRange(item.spriteAddresses);
                    break;
                }
            }
            if (eligibleSprites.Count == 0)
                eligibleSprites.AddRange(Instance.mudSpriteAddresses);
        } else {
            foreach (SpriteForLayout item in Instance.mudLayouts) {
                List<FieldTileSpriteType[, ]> typesList = item.layouts.Select(x => x.GetCells()).ToList();
                if (subFieldTile.ExportState().IsInListByMatching(typesList)) {
                    eligibleSprites.AddRange(item.spriteAddresses);
                    break;
                }
            }
            if (eligibleSprites.Count == 0)
                eligibleSprites.AddRange(Instance.mudSpriteAddresses);
        }

        int randIndex = Random.Range(0, eligibleSprites.Count);

        Vector2 tileAddress = new Vector2(eligibleSprites[randIndex].x, eligibleSprites[randIndex].y);
        Vector4 textureCellAndDimension = new Vector4(tileAddress.x, tileAddress.y, Instance.spriteSheetSize.x, Instance.spriteSheetSize.y);

        return textureCellAndDimension;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class childTilemaps : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;

    public List<Vector2> coords;

    public void Awake()
    {
        coords = new List<Vector2>();

        for (int j = -8; j < 8; j++)
        {
            for (int k = -8; k < 8; k++)
            {
                TileBase currTileFloor = floorTilemap.GetTile(new Vector3Int(j, k, 0));
                TileBase currTileWall = wallTilemap.GetTile(new Vector3Int(j, k, 0));
                if (currTileFloor != null && currTileWall == null)
                {
                    coords.Add(new Vector2(j, k));
                }
            }
        }
    }
}

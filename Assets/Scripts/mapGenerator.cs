using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using UnityEngine.AI;

public class mapGenerator : MonoBehaviour
{
    public int length;
    public GameObject itemGenerator;

    public Tile leftWall;
    public Tile rightWall;

    public NavMeshSurface surface;
    public GameObject[] prefabs;

    public List<GameObject> rooms;
    public List<Vector2> coords;
    

    void Start()
    {
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < length; i++)
            {
                var location = new Vector2(18 * i, 18 * j);
                coords.Add(location);
            } 
        }

        foreach (Vector2 coordinate in coords)
        {
        int index = Random.Range(0, prefabs.Length);
            GameObject room = Instantiate(prefabs[index], coordinate, Quaternion.identity, gameObject.transform);
            rooms.Add(room);
        }

    //    Tilemap leftRoomWallsTilemap = rooms[0].transform.GetChild(0).gameObject.GetComponent<Tilemap>();
    //    Tilemap rightRoomWallsTilemap = rooms[length - 1].transform.GetChild(0).gameObject.GetComponent<Tilemap>();

    //     for (int i = -8; i < 8; i++)
    //     {
    //         leftRoomWallsTilemap.SetTile(new Vector3Int(-9, i, 0), leftWall);
    //         rightRoomWallsTilemap.SetTile(new Vector3Int(8, i, 0), rightWall);
    //     }

        surface.BuildNavMeshAsync();
        itemGenerator.SetActive(true);
    }
}

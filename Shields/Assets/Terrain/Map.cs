using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] TilePrefabs;
    public Tile[,] Tiles;
    public int Width;
    public int Length;

    public void GenerateMap()
    {
        this.Tiles = new Tile[this.Width, this.Length];

        for (int row = 0; row < this.Width; row++)
        {
            for (int col = 0; col < this.Length; col++)
            {
                Vector3 position = new Vector3(row + 0.5f, 0, col + 0.5f);

                GameObject emptyTile = Instantiate(TilePrefabs[0], position, Quaternion.identity);
                emptyTile.AddComponent<Tile>();
                emptyTile.transform.SetParent(this.gameObject.transform);

                this.Tiles[row, col] = emptyTile.GetComponent<Tile>();
            }
        }
    }

    public Vector3 GetMapCenter()
    {
        return new Vector3
            (
                x: this.Width / 2f,
                y: 0,
                z: this.Length / 2f
            );
    }
}

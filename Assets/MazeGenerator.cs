using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public Transform Player;
    Vector3 playerStart;

    [Header("Walls")]
    [Range(1, 5)] public int scale = 1;
    public Tilemap Tilemap;
    public TileBase Wall;
    public TileBase Floor;
    public int Width = 21;  // should be odd
    public int Height = 21; // should be odd
    Vector2Int start;
    public float secondChance = 0.5f;
    float currentChance;

    [Header("Coins")]
    public float baseCoinChance = 1f/20f;
    public float additiveNeighborChance = 1f/20f;
    public CoinPickup[] loot;
    float totalWeight;
    public Transform coinPrefab;
    public List<Transform> coins = new();

    [Header("Lava")]
    public Tilemap LavaTilemap;
    public TileBase Lava;
    public float perlinScale = 0.1f;
    [Range(0f, 1f)] public float lavaThreshold = 0.5f;

    private System.Random rand = new System.Random();
    private Vector2Int[] directions = new[] {
        Vector2Int.up * 2,
        Vector2Int.right * 2,
        Vector2Int.down * 2,
        Vector2Int.left * 2
    };

    private void Start() {
        // GenerateMaze();
    }

    void SortLootTable()
    {
        totalWeight = 0f;
        foreach (CoinPickup coin in loot)
        {
            totalWeight += coin.Weight;
        }

        // BUBBLE SORT -> GETS THE JOB DONE
        for (int i = 0; i < loot.Length; i++)
        {
            for (int j = 0; j < loot.Length - 1; j++)
            {
                if (loot[j].Value > loot[j + 1].Value)
                {
                    // swap
                    CoinPickup temp = loot[j];
                    loot[j] = loot[j + 1];
                    loot[j + 1] = temp;
                }
                else if (loot[j].Value == loot[j + 1].Value)
                {
                    if (loot[j].Weight < loot[j + 1].Weight)
                    {
                        // swap
                        CoinPickup temp = loot[j];
                        loot[j] = loot[j + 1];
                        loot[j + 1] = temp;
                    }
                }
            }
        }
    }

    CoinPickup GetCoinPickup()
    {
        float i = Random.Range(0f, totalWeight);
        foreach (CoinPickup coin in loot)
        {
            i -= coin.Weight;
            if (i <= 0) return coin;
        }
        return loot[0];
    }

    void GenerateMaze()  {
        SortLootTable();
        
        start = new Vector2Int(Random.Range(0, Width/2)+1, Random.Range(0, Height/2)+1);
        Player.transform.position = (scale * (Vector2) start) + new Vector2(2, 2);
        playerStart = Player.transform.position;

        Tilemap.ClearAllTiles();

        // Fill entire grid with walls
        for (int x = -8; x < Width*scale+8; x++)
            for (int y = -8; y < Height*scale+8; y++)
                Tilemap.SetTile(new Vector3Int(x, y, 0), Wall);

        bool[,] visited = new bool[Width, Height];

        Carve(start, visited);

        LavaTilemap.ClearAllTiles();
        GenerateLava();

        int oldScale = scale;
        int oldWidth = Width;
        int oldHeight = Height;

        scale /= 4;
        Width = Width*4-1;
        Height = Height*4-1;
        visited = new bool[Width, Height];
        currentChance = 1;
        Carve(start, visited, secondChance);
        scale = oldScale;
        Width = oldWidth;
        Height = oldHeight;

        SpawnCoins();
    }

    void GenerateLava() {
        BoundsInt bounds = LavaTilemap.cellBounds;
        LavaTilemap.ClearAllTiles();

        for (int x = -2; x < Width*scale; x++)
        {
            for (int y = -2; y < Height*scale; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (Tilemap.GetTile(pos) == Wall) continue;
                if ((pos - playerStart).sqrMagnitude < 5) continue;

                float noise = Mathf.PerlinNoise(x * perlinScale, y * perlinScale);

                if (noise > lavaThreshold) LavaTilemap.SetTile(pos, Lava);
            }
        }
    }

    void Carve(Vector2Int pos, bool[,] visited, float chance=1) {
        // RC BACKTRACKING WOOO

        visited[pos.x, pos.y] = true;

        Place(pos, Floor, chance);
        // Tilemap.SetTile((Vector3Int)(pos*scale), Floor);

        Shuffle(directions);
        foreach (var dir in directions)
        {
            Vector2Int newPos = pos + dir;
            if (InBounds(newPos) && !visited[newPos.x, newPos.y])
            {
                Vector2Int wallPos = pos + dir / 2;
                Place(wallPos, Floor, chance);
                // Tilemap.SetTile((Vector3Int)(wallPos * scale), Floor);
                Carve(newPos, visited, chance);
            }
        }
    }

    bool InBounds(Vector2Int pos)
    {
        return pos.x > 0 && pos.y > 0 && pos.x < Width && pos.y < Height;
    }

    void Shuffle(Vector2Int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
    
    void Place(Vector2Int pos, TileBase tile, float chance)  {
        if (Random.Range(0f, 1f) > currentChance) return;
        currentChance -= 0.1f;
        if (currentChance < secondChance) currentChance = 1f;

        // Tilemap.SetTile((Vector3Int)pos, tile);
        for (int ox=0; ox<scale; ox++) {
            for (int oy=0; oy<scale; oy++) {
                Tilemap.SetTile((Vector3Int)(pos*scale + new Vector2Int(ox, oy)), tile);
            }
        }
    }

    public void SpawnCoins()
    {
        DeleteAllCoins();

        BoundsInt bounds = Tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (Tilemap.GetTile(pos) != Floor) continue;
            if (LavaTilemap.GetTile(pos) == Lava) continue;

            int neighborCount = CountNeighbors(pos);
            float chance = baseCoinChance + additiveNeighborChance * neighborCount;

            if (Random.value < chance)
            {
                Vector3 worldPos = Tilemap.GetCellCenterWorld(pos);
                coins.Add(Instantiate(GetCoinPickup().transform, worldPos, Quaternion.identity, transform));
            }
        }
    }
    
    void DeleteAllCoins() {
        foreach (Transform coin in coins) {
            DestroyImmediate(coin.gameObject);
        }
        coins.Clear();
    }

    int CountNeighbors(Vector3Int pos)
    {
        Vector3Int[] offsets = {
            Vector3Int.up, Vector3Int.down,
            Vector3Int.left, Vector3Int.right,
            new Vector3Int(-1, 1, 0), new Vector3Int(1, 1, 0),
            new Vector3Int(-1, -1, 0), new Vector3Int(1, -1, 0)
        };

        int count = 0;
        foreach (var offset in offsets)
        {
            if (Tilemap.GetTile(pos + offset) == Wall)
                count++;
        }
        return count;
    }


    public void Regenerate() => GenerateMaze();
}
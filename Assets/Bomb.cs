using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float delay = 2f;
    public int radius = 2;
    public Tilemap targetTilemap;
    public TileBase tileToClear;

    void Start()
    {
        StartCoroutine(DetonateAfterDelay());
    }

    IEnumerator DetonateAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Detonate();
        Destroy(gameObject);
    }

    void Detonate()
    {
        Vector3Int center = targetTilemap.WorldToCell(transform.position);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector3Int pos = new Vector3Int(center.x + x, center.y + y, 0);
                if (tileToClear == null || targetTilemap.GetTile(pos) == tileToClear) {
                    targetTilemap.SetTile(pos, null);
                }
            }
        }
    }
}

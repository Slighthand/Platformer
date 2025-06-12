using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    // Start is called before the first frame update
    public const string DUNGEON_SAVEDATA = "/savedata.json";
}

[SerializeField]
public class PlayerData
{
    [SerializeField] public List<Vector3> positions;
    // public PlayerData(PlayerManager); - set instance for player position in the player movement script.
}

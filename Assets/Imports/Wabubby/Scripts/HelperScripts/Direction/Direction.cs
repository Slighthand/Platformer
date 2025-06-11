using UnityEngine;
using static UnityEngine.Mathf;

[System.Serializable]
public struct Direction {

    static float TOLERANCE = 0.5f;

	public bool top;
    public bool bot;
    public bool left;
    public bool right;

    public bool IsAlligned(Vector2 dir) {
        if (dir.y > TOLERANCE && top) return true;
        if (dir.y < -TOLERANCE && bot) return true;
        if (dir.x > TOLERANCE && right) return true;
        if (dir.x < -TOLERANCE && left) return true;

        return false;
    }

}
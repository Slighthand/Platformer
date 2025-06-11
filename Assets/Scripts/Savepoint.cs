using System;
using System.Collections;
using UnityEngine;

[Serializable] public class Savepoint {
    public Area Area;
    public int Index;
    public Vector3 Position;
    
    public Savepoint(Area area, int index, Vector3 position) {
        Area = area;
        Index = index;
        Position = position;
    }
}
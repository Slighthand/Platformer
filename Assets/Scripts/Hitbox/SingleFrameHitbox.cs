using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFrameHitbox : Hitbox
{
    protected override void Update() {
        base.Update();
        IsActive = false;
    }
}

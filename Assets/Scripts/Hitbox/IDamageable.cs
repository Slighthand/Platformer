using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(float dmg, IDamager source, Transform owner, Collider2D collider=null);

}

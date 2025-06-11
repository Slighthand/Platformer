using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Wabubby.Extensions;

public class Hitbox : MonoBehaviour
{
    public bool useCollider = false;

    [SerializeField] protected LayerMask Mask;
    [SerializeField] protected bool IsActive = true;
    
    [Header("Dimensions")]
    public Vector2 Offset;
    public Vector2 Size;
    public bool IsCircle;
    public float Radius;


    public delegate void HitHandler(Collider2D collider); public virtual event HitHandler OnHit;
    protected Vector2 center => (Vector2) (transform.position + transform.right * Offset.x + transform.up * Offset.y);
    protected Collider2D[] colliders = new Collider2D[0];
    protected bool isColliding;
    protected new Collider2D collider;
    protected ContactFilter2D filter;

    void Awake() {
        if (useCollider) {
            collider = GetComponent<Collider2D>();
            filter = new ContactFilter2D();
            filter.SetLayerMask(Mask);
            filter.useTriggers = false;
            colliders = new Collider2D[8];
        }
    }

    /// <summary>
    /// activation helper method for animators.
    /// </summary>
    public virtual void Activate() {
        IsActive = true;
    }

    /// <summary>
    /// activation helper method for animators.
    /// </summary>
    public virtual void Deactivate() {
        IsActive = false;
    }

    protected virtual void Update() {
        if (!IsActive) return;
        UpdateColliders();

        foreach (Collider2D collider in colliders) {
            if (collider == null) return;
            OnHit?.Invoke(collider);
        }
    }

    protected Collider2D[] UpdateColliders() {
        if (useCollider) {
            for (int i=0; i<colliders.Length; i++) colliders[i] = null;
            collider.OverlapCollider(filter, colliders);
        }
        else if (IsCircle) colliders = Physics2D.OverlapCircleAll(center, Radius, Mask);
        else colliders = Physics2D.OverlapBoxAll(center, Size, -transform.rotation.eulerAngles.z, Mask);
        isColliding = colliders.Length > 0;
        return colliders;
    }
    

    // DEBUG DRAWING
    protected virtual void OnDrawGizmos() {
        Color newColor;
        if (isColliding && IsActive) {
            newColor = CollidingColour;
        } else if (IsActive) {
            newColor = ActiveColor;
        } else {
            newColor = InactiveColour;
        }
        newColor.a = UnselectedTransparency;
        Gizmos.color = newColor;
        
        if (IsCircle) {
            GizmosDrawWireDisk(center, Vector3.forward, Radius);
        } else {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.matrix = rotationMatrix; 
            Gizmos.DrawWireCube((Vector3) Offset, (Vector3) Size);
        }
    }

    protected virtual void OnDrawGizmosSelected() {
        if (isColliding) {
            Gizmos.color = CollidingColour;
        } else if (IsActive) {
            Gizmos.color = ActiveColor;
        } else {
            Gizmos.color = InactiveColour;
        }

        if (IsCircle) {
            GizmosDrawWireDisk(center, Vector3.forward, Radius);
        } else {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.matrix = rotationMatrix; 
            Gizmos.DrawWireCube((Vector3) Offset, (Vector3) Size);
        }
    }

    private void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration) {
        var orientation = Quaternion.Euler(0, 0, angle);

        // Basis vectors, half the size in each direction from the center.
        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        // Four box corners.
        var topLeft = point + up - right;
        var topRight = point + up + right;
        var bottomRight = point - up + right;
        var bottomLeft = point - up - right;

        // Now we've reduced the problem to drawing lines.
        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }

    private static float UnselectedTransparency = 0.1f;
    private static Color InactiveColour = Color.gray;
    private static Color ActiveColor = Color.white;
    private static Color CollidingColour = Color.red;

}

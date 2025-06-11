using UnityEngine;
using static Wabubby.Extensions;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField, Range(0f, 30f)] public float speed = 6;

    bool stopControl = false;
    Rigidbody2D physics;
    PlayerInputCoordinator input;

    void Start() {
        input = transform.root.GetComponent<PlayerInputCoordinator>();
        physics = GetComponent<Rigidbody2D>();
    }

    protected void OnDisable() {
        stopControl = false;
    }


	protected void Update() {
        physics.velocity = speed * input.Movement;
	}

}

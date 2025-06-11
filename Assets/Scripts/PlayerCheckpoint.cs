using UnityEngine;
// using static Wabubby.Extensions;
using static UnityEngine.Mathf;
using System.Collections;

// public class PlayerCheckpoint : IMovementController {
//     static float CHECK_TICK = 0.5f;

//     Vector2 checkPoint;
//     Coroutine tickCoroutine;
//     PlayerBuoyancy buoyancy;
//     PlayerFormController playerForm;

//     protected override void Awake() {
//         physics = GetComponent<PhysicsController>();
//         buoyancy = GetComponent<PlayerBuoyancy>();
//         playerForm = transform.root.GetComponent<PlayerFormController>();
//     }

//     void OnEnable() {
//         if (tickCoroutine != null) StopCoroutine(tickCoroutine);
//         tickCoroutine = StartCoroutine(Tick());
//     }

//     void OnDisable() { if (tickCoroutine != null) StopCoroutine(tickCoroutine); }

//     public void Set() { checkPoint = GameManager.Instance.focus.position; }
//     public void Restore() {
//         transform.position = checkPoint;
//         playerForm.SetForm(PlayerFormController.PlayerForm.Coon);
//     }

//     IEnumerator Tick() {
//         #if UNITY_EDITOR
//         yield return null;
//         #endif
//         while (true) {
//             // need to be on SOLID ground
//             // NOT UNDERWATER
//             // i think that's it...
//             if (!buoyancy.isUnderwater && physics.cInfo.bot) {
//                 Set();
//             }
//             yield return GetWait(CHECK_TICK);
//         }
//     }

//     protected override void CalculateControlVariables() {  }

//     void OnDrawGizmosSelected() {
//         Gizmos.color = Color.green;
//         Gizmos.DrawSphere(checkPoint, 0.2f);
//     }
// }

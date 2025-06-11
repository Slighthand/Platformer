using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using System.Reflection;
using UnityEditor;

namespace Wabubby
{

    public static class Extensions
    {
        /// <summary>
        /// Unity does an expensive iteration over every GO for every Camera.main call.
        /// This property uses a backing field to store it.
        /// </summary>
        private static Camera mainCamera;
        public static Camera MainCamera {
            get {
                if (mainCamera == null) mainCamera = Camera.main;
                return mainCamera;
            }
        }

        /// <summary>
        /// Creating WaitForSecond() objects many times is extremely costly.
        /// This flyweight reduces the memory and garbage collection overhead.
        /// </summary>
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time) {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        /// <summary>
        /// Creating WaitForSecond() objects many times is extremely costly.
        /// This flyweight reduces the memory and garbage collection overhead.
        /// </summary>
        private static readonly Dictionary<float, WaitForSecondsRealtime> WaitRealtimeDictionary = new Dictionary<float, WaitForSecondsRealtime>();
        public static WaitForSecondsRealtime GetWaitRealtime(float time) {
            if (WaitRealtimeDictionary.TryGetValue(time, out var waitRealtime)) return waitRealtime;
            WaitRealtimeDictionary[time] = new WaitForSecondsRealtime(time);
            return WaitRealtimeDictionary[time];
        }


        /// <summary>
        /// Snap Vector3 to nearest grid position
        /// </summary>
        /// <param name="vector3">Sloppy position</param>
        /// <param name="gridSize">Grid size</param>
        /// <returns>Snapped position</returns>
        public static Vector3 Snap(this Vector3 vector3, float gridSize = 1.0f)
        {
            return new Vector3(
                Mathf.Round(vector3.x / gridSize) * gridSize,
                Mathf.Round(vector3.y / gridSize) * gridSize,
                Mathf.Round(vector3.z / gridSize) * gridSize);
        }

        /// <summary>
        /// Snap Vector2 to the nearest grid position.
        /// </summary>
        /// <param name="vector2">Sloppy position</param>
        /// <param name="gridSize">Grid size</param>
        /// <returns></returns>
        public static Vector2 Snap(this Vector2 vector2, float gridSize = 1.0f)
        {
            return new Vector2(
                Mathf.Round(vector2.x / gridSize) * gridSize,
                Mathf.Round(vector2.y / gridSize) * gridSize);
        }

        /// <summary>
        /// Snap Vector3 to nearest grid position with offset
        /// </summary>
        /// <param name="vector3">Sloppy position</param>
        /// <param name="gridSize">Grid size</param>
        /// <returns>Snapped position</returns>
        public static Vector3 SnapOffset(this Vector3 vector3, Vector3 offset, float gridSize = 1.0f)
        {
            Vector3 snapped = vector3 + offset;
            snapped = new Vector3(
                Mathf.Round(snapped.x / gridSize) * gridSize,
                Mathf.Round(snapped.y / gridSize) * gridSize,
                Mathf.Round(snapped.z / gridSize) * gridSize);
            return snapped - offset;
        }

        /// <summary>
        /// Draw a gizmos disk.
        /// </summary>
        /// <param name="center">center of the disc</param>
        /// <param name="normal">normal direction of the disc</param>
        /// <param name="radius">radius of the disc</param>
        public static void GizmosDrawWireDisk(Vector3 center, Vector3 normal, float radius, int resolution=30)
        {
            Vector3[] points = new Vector3[resolution];
            for (int i=0; i<resolution; i++) {
                float angle = 2 * PI  / resolution * i;
                Vector3 pos = center;
                pos.x += radius * Sin(angle);
                pos.y += radius * Cos(angle);
                points[i] = pos;
            }

            for (int i=0; i<resolution-1; i++) {
                Gizmos.DrawLine(points[i], points[i+1]);
            }
            Gizmos.DrawLine(points[0], points[^1]);
        }

        /// <summary>
        /// Get the sum of an integer array.
        /// </summary>
        /// <param name="nums">int array</param>
        /// <returns></returns>
        public static int Sum(this int[] nums) {
            int sum = 0;
            foreach (int num in nums) { sum += num; }
            return sum;
        }

        /// <summary>
        /// Get the sum of a float array.
        /// </summary>
        /// <param name="nums">float array</param>
        /// <returns></returns>
        public static float Sum(this float[] nums) {
            int sum = 0;
            foreach (int num in nums) { sum += num; }
            return sum;
        }

        /// <summary>
        /// Choose a random index depending on it's weight.
        /// </summary>
        /// <param name="weights">array with each index's weight.</param>
        /// <returns></returns>
        public static int ChooseWeightedIndex(int[] weights) {
            int groupIndex = 0;
            int sampleIndex = Random.Range(0, weights.Sum());

            for (int i=0; i<weights.Length; i++) {
                if (sampleIndex < weights[i]) { groupIndex = i; break; }
                sampleIndex -= weights[i];
            }

            return groupIndex;
        }

        /// <summary>
        /// Pick a random element of an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="teez"></param>
        /// <returns></returns>
        public static T RandomChoice<T>(T[] teez) {
            return teez[Random.Range(0, teez.Length)];
        }
        
        /// <summary>
        /// Remove the first instance of a character from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RemoveChar(this string str, char c) {
            string result = str;
            if (str.IndexOf(c) != -1) result = result.Remove(result.IndexOf(c), 1);
            return result;
        }

        /// <summary>
        /// Destroy all child gameobjects.
        /// </summary>
        /// <param name="t"></param>
        public static void DestroyChildren(this Transform t) {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }

        /// <summary>
        /// Rotates a 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="target"></param>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static Vector2 RotateDirectionTowards(Vector2 pos, Vector2 dir, Vector2 target, float deg) {
            Vector2 targetDirection = (target - pos).normalized;

            float angleToTarget = Vector2.SignedAngle(dir, targetDirection);
            float rotationAngle = Clamp(angleToTarget, -deg, deg);

            float newAngle = Vector2.SignedAngle(Vector2.right, dir) + rotationAngle;

            float radians = newAngle * Deg2Rad;
            Vector2 newDirection = new Vector2(Cos(radians), Sin(radians));

            return newDirection.normalized;
        }

        /// <summary>
        /// Convert a an angle to Vector2
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static Vector2 AngleToVector2(float deg)  {
            float theta = deg * Deg2Rad;

            return new Vector2(Cos(theta), Sin(theta));
        }


        /// <summary>
        /// Retrieve the maximum float in an array.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static float Max(this float[] arr) {
            float max = arr[0];
            foreach (float val in arr) {
                if (val > max) max = val;
            }
            return max;
        }

        /// <summary>
        /// Returns true if the gameobject layer falls satisfies the mask.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool IsInLayerMask(GameObject obj, LayerMask mask) {
            return (mask.value & (1 << obj.layer)) != 0;
        }

        /// <summary>
        /// Perform a raycast ignoring a supplied transform
        /// </summary>
        /// <param name="self"></param>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        /// <param name="minDepth"></param>
        /// <param name="maxDepth"></param>
        /// <returns></returns>
        public static RaycastHit2D RaycastIgnoreSelf2D(this Transform self, Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity) {
			RaycastHit2D[] queries = Physics2D.RaycastAll(origin, direction, distance, layerMask, minDepth, maxDepth);

			// foreach (RaycastHit2D query in queries) {
			// 	if (query.transform != self) { return query; }
			// }

            for (int i=queries.Length-1; i>=0; i--) {
                RaycastHit2D query = queries[i];
                if (query.transform != self) { return query; }
            }

            return default;
        }

        /// <summary>
        /// Modulus that works properly with negative numbers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int mod(int x, int m) {
            return (x%m + m)%m;
        }

        /// <summary>
        /// play a clip with randomized pitch
        /// </summary>
        /// <param name="source"></param>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="loPitch"></param>
        /// <param name="hiPitch"></param>
        public static void PlayRandom(this AudioSource source, AudioClip clip, float volume=1f, float loPitch=0.9f, float hiPitch=1.1f) {
            source.clip = clip;
            source.pitch = Random.Range(loPitch, hiPitch);
            source.volume = volume;
            source.Play();
        }

        /// <summary>
        /// play a randomly chosen clip with randomized pitch
        /// </summary>
        /// <param name="source"></param>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="loPitch"></param>
        /// <param name="hiPitch"></param>
        public static void PlayVariation(this AudioSource source, AudioClip[] clips, float volume=1f, float loPitch=0.9f, float hiPitch=1.1f) {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.pitch = Random.Range(loPitch, hiPitch);
            source.volume = volume;
            source.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySort : MonoBehaviour
{
    int first = 1;
    int second = 2;
    int third = 3;
    int fourth = 4;
    // sort enemies by distance
    public static void BubbleSort(int[] numbers)
    {
        for (int j = 0; j < numbers.Length; j++)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                Debug.Log(numbers[i] + " " + numbers[i + 1]);
                if (numbers[i] > numbers[i + 1])
                {
                    int tmp = numbers[i];
                    numbers[i] = numbers[i + 1];
                    numbers[i + 1] = tmp;
                }
            }
        }

        // foreach (int n in numbers)
        // {
        //     Debug.Log(n);
        // }
    }
    public static void Main(string[] args)
    {
        int[] nums = new int[] {6,4,3,5,2,1};
        BubbleSort(nums);
    }

}

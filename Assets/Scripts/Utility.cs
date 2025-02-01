using UnityEngine;
using System.Collections.Generic;

namespace RifyDev
{
    public static class Utility {
        public static bool CheckCameraVisibility(Camera camera, GameObject obj)
        {
            Vector3 viewPos = camera.WorldToViewportPoint(obj.transform.position);
            return (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0);
        }

        public static List<float> BubbleSortF(List<float> arr)
        {
            List<float> ret = new List<float>();

            int size = arr.Count;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        float tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
                }
            }

            return ret;
        }

        public static void BubbleSortF(ref List<float> arr)
        {
            int size = arr.Count;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(arr[j] > arr[j + 1])
                    {
                        float tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
                }
            }
        }

        // sort two lists at the same time that are correlated with one of another in some way
        public static void BubbleSortFGO(ref List<float> a, ref List<GameObject> b)
        {
            for(int i = 0; i < a.Count - 1; i++)
            {
                for(int j = 0; j < a.Count - 1; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        float ftmp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = ftmp;

                        GameObject gotmp = b[j]; // lol "bj"
                        b[j] = b[j + 1];
                        b[j + 1] = gotmp;
                    }
                }
            }
        }
    };
}
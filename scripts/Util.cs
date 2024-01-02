using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public static class Util
{
    static public T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = UnityEngine.Random.Range(0, array.Length);
            random2 = UnityEngine.Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }

    public static GameObject[] GetChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }

        return children;
    }


    // 딜레이 & player 변환
    public static IEnumerator DelayTime(GameObject player, float Maxtime)
    {
        yield return new WaitForSeconds(Maxtime);

        player.GetComponent<VideoPlayer>().Play();

    }


    // 딜레이 & Scene 변환 

    public static void Back2Main()
    {
        SceneManager.LoadScene("main");
    }

    // Scene 변환 

    public static IEnumerator Move2SSQ(string SSQ, float Maxtime)
    {
        yield return new WaitForSeconds(Maxtime);

        SceneManager.LoadScene(SSQ);
       
    }


    public static void Move2Next_Scene(string next_scene)
    { 
        SceneManager.LoadScene(next_scene);

    }



    public class PermutationGenerator
    {
        public static List<List<T>> GeneratePermutations<T>(List<T> elements)
        {
            List<List<T>> permutations = new List<List<T>>();
            GeneratePermutationsRecursive(elements, 0, permutations);
            return permutations;
        }

        private static void GeneratePermutationsRecursive<T>(List<T> elements, int start, List<List<T>> permutations)
        {
            if (start == elements.Count - 1)
            {
                permutations.Add(new List<T>(elements));
                return;
            }

            for (int i = start; i < elements.Count; i++)
            {
                Swap(elements, start, i);
                GeneratePermutationsRecursive(elements, start + 1, permutations);
                Swap(elements, start, i); // 되돌리기

            }
        }

        private static void Swap<T>(List<T> elements, int i, int j)
        {
            T temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
    }

}




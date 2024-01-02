using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dis_midScroll : MonoBehaviour
{
    // public でInspectorから、生成したいprefabを紐付けておきます。
    [SerializeField] GameObject makeprefab;

    private const float START = 0.0f;
    private const float INTERVAL = 27.5f;

    void Start()
    {
        InvokeRepeating("UpdateMakePrefab", START, INTERVAL);
    }


    private void UpdateMakePrefab()
    {

        Instantiate(makeprefab, this.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [System.NonSerialized] public float cost;
    [SerializeField] private int firstCost;
    public float costSpeed = 2.0f;
    public int maxCost = 120;
    public int lvUpCost = 30;
    public int maxLevel = 7;

    [SerializeField] private Text textCost;

    [SerializeField] private GameObject createPos;
    [System.NonSerialized] public Vector3 createPosition;

    [SerializeField] BaseStatus[] statuses;
    public Sprite[] particleSprite;

    private void Awake()
    {
        createPosition = createPos.transform.localPosition;
    }

    private void Start()
    {
        cost = firstCost;
        textCost.text = firstCost + " : " + maxCost;
    }

    private void Update()
    {
        if (cost < maxCost) cost += Time.deltaTime / costSpeed;
        else cost = maxCost;

        textCost.text = (int)cost + " : " + maxCost;
    }


    private void OnApplicationQuit()
    {
        foreach (BaseStatus status in statuses) status.ResetCost();
    }
}

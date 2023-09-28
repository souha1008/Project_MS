using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [System.NonSerialized] public float cost;
    [SerializeField] private int firstCost;
    public float costSpeed = 2.0f;
    public int maxCost = 240;

    [SerializeField] private Text textCost;

    [SerializeField] private GameObject createPos;
    [System.NonSerialized] public Vector3 createPosition;

    private void Awake()
    {
        createPosition = createPos.transform.localPosition;
    }

    private void Start()
    {
        cost = firstCost;
        textCost.text = "cost : " + firstCost;
    }

    private void Update()
    {
        if (cost < maxCost) cost += Time.deltaTime / costSpeed;
        else cost = maxCost;

        textCost.text = "cost : " + (int)cost;
    }
}

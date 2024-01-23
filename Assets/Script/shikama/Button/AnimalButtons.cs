using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtons : MonoBehaviour
{
    [SerializeField] private GameObject animalObject;
    private Animal animalScript;
    GameSetting gameSetting;
    private int lastCost;
    Button button;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        animalScript = animalObject.GetComponent<Animal>();

        lastCost = animalScript.baseStatus.cost;
        button = GetComponent<Button>();
        CosttoText();
    }

    private void Update()
    {
        if (lastCost != animalScript.baseStatus.cost) CosttoText();
        
        lastCost = animalScript.baseStatus.cost;

        if (PalletChange.palletNow == PALLET.FIRST)
        {
            if (gameSetting.cost < animalScript.baseStatus.cost)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }

    public void PressAnimal()
    {
        if (gameSetting.cost >= animalScript.baseStatus.cost)
        {
            gameSetting.cost -= animalScript.baseStatus.cost;
            Vector3 pos = gameSetting.createPosition;
            //pos.y += Random.Range(-0.3f, 0.3f);
            if(animalScript is Zebra) pos.z = Random.Range(-18.0f, -22.0f);
            if(animalScript is Buffalo) pos.z = Random.Range(-14.0f, -18.0f);
            if(animalScript is Camel) pos.z = Random.Range(-10.0f, -14.0f);
            if(animalScript is Elephant) pos.z = Random.Range(-6.0f, -10.0f);
            if(animalScript is Giraffe) pos.z = Random.Range(-2.0f, -6.0f);

            if (animalScript is Giraffe) pos.y += 0.8f;
            Instantiate(animalObject, pos, animalObject.transform.rotation);
        }
    }

    private void CosttoText()
    {
        if (transform.childCount == 0) return;
        GameObject child = transform.GetChild(0).gameObject;
        if (child.GetComponent<Text>() == null) return;
        child.GetComponent<Text>().text = animalScript.baseStatus.cost.ToString();
    }
}

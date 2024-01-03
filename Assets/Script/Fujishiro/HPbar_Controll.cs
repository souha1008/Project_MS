using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar_Controll : MonoBehaviour
{
    [SerializeField] House nowHP;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = nowHP.hp;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = nowHP.hp;
    }
}

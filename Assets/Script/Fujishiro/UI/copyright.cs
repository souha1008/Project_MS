using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class copyright : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        //tmp = GetComponent<TextMeshPro>();
        tmp.text = Application.version + " CopyRighy:" + Application.companyName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

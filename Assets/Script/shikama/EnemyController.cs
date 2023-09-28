using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    
    [SerializeField] private float createSpeed;
    private float createTime = 0;

    [SerializeField] private GameObject createPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(createTime >= createSpeed)
        {
            createTime = 0;

            int num = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[0], createPos.transform.localPosition, Quaternion.identity);
        }
        createTime += Time.deltaTime / createSpeed;
    }
}

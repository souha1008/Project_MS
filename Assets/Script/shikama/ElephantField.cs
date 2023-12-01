using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantField : MonoBehaviour
{
    Dictionary<Collider2D, float> fieldObjectsCounter;

    [SerializeField] ElephantBaseStatus elephantBase;

    private void Start()
    {
        fieldObjectsCounter = new Dictionary<Collider2D, float>();
        Destroy(this.gameObject, elephantBase.activeTimeDesert);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!fieldObjectsCounter.ContainsKey(collision))
            {
                fieldObjectsCounter.Add(collision, elephantBase.desertDamageSecond);
                collision.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
            }

            fieldObjectsCounter[collision] += Time.deltaTime;

            if (fieldObjectsCounter[collision] >= elephantBase.desertDamageSecond)
            {
                AnimalStatus status = collision.GetComponent<Animal>().status;
                status.hp -= Mathf.RoundToInt(status.maxHP * (elephantBase.desertDamageMag) * 0.01f);
                fieldObjectsCounter[collision] = 0.0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collision.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAwake;
    }
}

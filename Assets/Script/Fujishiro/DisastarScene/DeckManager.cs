using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Disastar_Scroll_Button;

public class DeckManager : MonoBehaviour
{
    [Serializable]
    public class Disastar_Baner : SerializableDictionary<string, Sprite, StringSpriteKeyValuePair> { }

    [Serializable]
    public class StringStringKeyValuePair : SerializableKeyValuePair<string, string> { }

    [Serializable]
    public class Deck : SerializableDictionary<string, string, StringStringKeyValuePair> { }

    public static DeckManager instance;

    // �ЊQ�̖��O���
    public string[] Disastar_Name { get; set; } =
    {
        "NULL",
        "Meteor",
        "EarthQuake",
        "Hurricane",
        "ThunderStome",
        "Tsunami",
        "Eruption",
        "Plague",
        "Desert",
        "IceAge",
        "BigFire"
    };

    // ���݂̃f�b�L�Ґ������i�[����
    public Deck deck;

    // �ЊQ�̃o�i�[���i�[
    public Disastar_Baner disastar_baner;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            deck.Table["deck2"] = "EarthQuake";
            Debug.Log("deck2��EarthQuake�ɕύX");
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log(deck.Table["deck1"]);
            Debug.Log(deck.Table["deck2"]);
            Debug.Log(deck.Table["deck3"]);
            Debug.Log(deck.Table["deck4"]);
            Debug.Log(deck.Table["deck5"]);
        }
    }
}

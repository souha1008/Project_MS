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

    // 災害の名前情報
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

    string[] deckobj_name =
    {
        "deck1",
        "deck2",
        "deck3",
        "deck4",
        "deck5",
    };

    // 現在のデッキ編成情報を格納する
    public Deck deck;

    // 災害のバナーを格納
    public Disastar_Baner disastar_baner;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        deck.Table[deckobj_name[0]] = Disastar_Name[3];
        deck.Table[deckobj_name[1]] = Disastar_Name[7];
        deck.Table[deckobj_name[2]] = Disastar_Name[1];
        deck.Table[deckobj_name[3]] = Disastar_Name[4];
        deck.Table[deckobj_name[4]] = Disastar_Name[9];

        // 初期化の段階で現在のデッキ情報に応じてスプライトを変更するようにしなきゃ
    }

}

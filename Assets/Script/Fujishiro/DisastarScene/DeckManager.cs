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
    public class Deck_Dic : SerializableDictionary<string, string, StringStringKeyValuePair> { }

    public static DeckManager instance;

    [SerializeField] Deck _deck;

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
    public Deck_Dic deck;

    // 災害のバナーを格納
    public Disastar_Baner disastar_baner;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        deck.Table[deckobj_name[0]] = _deck.Disaster1;
        deck.Table[deckobj_name[1]] = _deck.Disaster2;
        deck.Table[deckobj_name[2]] = _deck.Disaster3;
        deck.Table[deckobj_name[3]] = _deck.Disaster4;
        deck.Table[deckobj_name[4]] = _deck.Disaster5;


        // 初期化の段階で現在のデッキ情報に応じてスプライトを変更するようにしなきゃ
    }

}

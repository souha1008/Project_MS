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

    string[] deckobj_name =
    {
        "deck1",
        "deck2",
        "deck3",
        "deck4",
        "deck5",
    };

    // ���݂̃f�b�L�Ґ������i�[����
    public Deck deck;

    // �ЊQ�̃o�i�[���i�[
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

        // �������̒i�K�Ō��݂̃f�b�L���ɉ����ăX�v���C�g��ύX����悤�ɂ��Ȃ���
    }

}

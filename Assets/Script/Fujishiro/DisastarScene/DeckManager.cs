using Kogane;
using System;
using UnityEngine;
using static Disastar_Scroll_Button;
using UnityEngine.UI;

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
        "隕石",
        "地震",
        "ハリケーン",
        "雷雨",
        "津波",
        "噴火",
        "疫病",
        "砂漠化",
        "氷河期",
        "大火災"
    };

    string[] deckobj_key =
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

    public GameObject[] deckImage;

    private bool onc = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        deck.Table["deck1"] = _deck.Disaster1;
        deck.Table["deck2"] = _deck.Disaster2;
        deck.Table["deck3"] = _deck.Disaster3;
        deck.Table["deck4"] = _deck.Disaster4;
        deck.Table["deck5"] = _deck.Disaster5;

        // 初期化の段階で現在のデッキ情報に応じてスプライトを変更するようにしなきゃ
        int i = 0;
        foreach (var f_deck in deck.Table.Values)
        {
            for (int j = 0; j < Disastar_Name.Length; j++)
            {
                if (f_deck == Disastar_Name[j])
                {
                    var image = deckImage[i].GetComponent<Image>();
                    image.sprite = disastar_baner[Disastar_Name[j]];
                    i++;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        _deck.Disaster1 = deck.Table["deck1"];
        _deck.Disaster2 = deck.Table["deck2"];
        _deck.Disaster3 = deck.Table["deck3"];
        _deck.Disaster4 = deck.Table["deck4"];
        _deck.Disaster5 = deck.Table["deck5"];
    }

}

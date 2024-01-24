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

    // �ЊQ�̖��O���
    public string[] Disastar_Name { get; set; } =
    {
        "NULL",
        "覐�",
        "�n�k",
        "�n���P�[��",
        "���J",
        "�Ôg",
        "����",
        "�u�a",
        "������",
        "�X�͊�",
        "��΍�"
    };

    string[] deckobj_key =
    {
        "deck1",
        "deck2",
        "deck3",
        "deck4",
        "deck5",
    };

    // ���݂̃f�b�L�Ґ������i�[����
    public Deck_Dic deck;

    // �ЊQ�̃o�i�[���i�[
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

        // �������̒i�K�Ō��݂̃f�b�L���ɉ����ăX�v���C�g��ύX����悤�ɂ��Ȃ���
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

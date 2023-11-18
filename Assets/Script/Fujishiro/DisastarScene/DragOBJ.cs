using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Kogane;
using static Disastar_Scroll_Button;

public class DragOBJ : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Serializable]
    public class StringMonoKeyValuePair : SerializableKeyValuePair<string, MonoBehaviour> { }

    [Serializable]
    public class Dic_DropArea : SerializableDictionary<string, MonoBehaviour, StringMonoKeyValuePair> { }

    [SerializeField] string Name;
    [SerializeField] GameObject Canvas;
    [SerializeField] float copyScale;
    [SerializeField] float alphaMag;

    // ドロップする場所
    [SerializeField] Dic_DropArea dropArea;

    GameObject copyOBJ;

    void Start()
    {

    }
    
    // ドラッグ開始
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("ドラッグ開始");

        // コピーオブジェクトをドラッグするように設定
        copyOBJ = copy(eventData.pointerDrag);

        // 元オブジェクトの透明度を薄くする
        setAlpha(GetComponent<Image>(), GetComponent<Image>().color.a * alphaMag);
    }

    // ドラッグ中
    public void OnDrag(PointerEventData eventData)
    {
        // 追従
        copyOBJ.transform.position = eventData.position;
    }

    // ドラッグ終了
    public void OnEndDrag(PointerEventData eventData) 
    {
        // ドラッグした場所に応じて格納
        foreach (var obj in dropArea)
        {
            if (contains(obj.Value.GetComponent<RectTransform>(), eventData))
            {
                // 既に他の場所に同じオブジェクトを置かれていたら
                if(DeckManager.instance.deck.Table.ContainsValue(Name))
                {
                    // 値からキーを取得
                    string key = DeckManager.instance.deck.Table.FirstOrDefault(kvp => kvp.Value == Name).Key;
                    Debug.Log("既に置かれている：" + DeckManager.instance.deck.Table[key]);

                    // 既に置いてあるキーの中をNullに変える
                    DeckManager.instance.deck.Table[key] = DeckManager.instance.Disastar_Name[0];
                    obj.Value.GetComponent<Image>().sprite = DeckManager.instance.disastar_baner["NULL"];

                    Debug.Log("nullにした：" + DeckManager.instance.deck.Table[key]);
                }

                // 変数Deckの中身を対応したものに変える
                Debug.Log(obj.Key);
                DeckManager.instance.deck.Table[obj.Key] = Name;
                Debug.Log("中身：" + DeckManager.instance.deck.Table[obj.ToString()]);



                // スプライトを変える
                //obj.Value.GetComponent<Image>().sprite = DeckManager.instance.disastar_baner[Name];
            }
        }
        

        // 透明度を戻す
        setAlpha(GetComponent<Image>(), 1);

        // ドラッグ中のオブジェクトを破棄
        Destroy(copyOBJ);
        Debug.Log("ドラッグ終了");
    }

    private GameObject copy(GameObject source)
    {
        GameObject ret = UnityEngine.Object.Instantiate(source);
        // 元オブジェクトと同じ位置に移動させる
        ret.transform.SetParent(Canvas.transform, true);
        ret.transform.position = source.transform.position;
        // 元オブジェクトと同じ大きさにする
        ret.transform.localScale = new Vector3(copyScale, copyScale, copyScale);
        return ret;
    }

    private void setAlpha(Image i, float value)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, value);
    }

    // ドラッグしたオブジェクトが指定した場所にあるか
    private bool contains(RectTransform area, PointerEventData target)
    {
        var selfBounds = GetBounds(area);
        var worldPos = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            area,
            target.position,
            target.pressEventCamera,
            out worldPos);
        worldPos.z = 0f;
        return selfBounds.Contains(worldPos);
    }

    private Bounds GetBounds(RectTransform target)
    {
        Vector3[] s_Corners = new Vector3[4];
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        target.GetWorldCorners(s_Corners);
        for (var index2 = 0; index2 < 4; ++index2)
        {
            min = Vector3.Min(s_Corners[index2], min);
            max = Vector3.Max(s_Corners[index2], max);
        }

        max.z = 0f;
        min.z = 0f;

        Bounds bounds = new Bounds(min, Vector3.zero);
        bounds.Encapsulate(max);
        return bounds;
    }

}

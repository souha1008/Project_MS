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

    // �h���b�v����ꏊ
    [SerializeField] Dic_DropArea dropArea;

    GameObject copyOBJ;

    void Start()
    {

    }
    
    // �h���b�O�J�n
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("�h���b�O�J�n");

        // �R�s�[�I�u�W�F�N�g���h���b�O����悤�ɐݒ�
        copyOBJ = copy(eventData.pointerDrag);

        // ���I�u�W�F�N�g�̓����x�𔖂�����
        setAlpha(GetComponent<Image>(), GetComponent<Image>().color.a * alphaMag);
    }

    // �h���b�O��
    public void OnDrag(PointerEventData eventData)
    {
        // �Ǐ]
        copyOBJ.transform.position = eventData.position;
    }

    // �h���b�O�I��
    public void OnEndDrag(PointerEventData eventData) 
    {
        // �h���b�O�����ꏊ�ɉ����Ċi�[
        foreach (var obj in dropArea)
        {
            if (contains(obj.Value.GetComponent<RectTransform>(), eventData))
            {
                // ���ɑ��̏ꏊ�ɓ����I�u�W�F�N�g��u����Ă�����
                if(DeckManager.instance.deck.Table.ContainsValue(Name))
                {
                    // �l����L�[���擾
                    string key = DeckManager.instance.deck.Table.FirstOrDefault(kvp => kvp.Value == Name).Key;
                    Debug.Log("���ɒu����Ă���F" + DeckManager.instance.deck.Table[key]);

                    // ���ɒu���Ă���L�[�̒���Null�ɕς���
                    DeckManager.instance.deck.Table[key] = DeckManager.instance.Disastar_Name[0];
                    obj.Value.GetComponent<Image>().sprite = DeckManager.instance.disastar_baner["NULL"];

                    Debug.Log("null�ɂ����F" + DeckManager.instance.deck.Table[key]);
                }

                // �ϐ�Deck�̒��g��Ή��������̂ɕς���
                Debug.Log(obj.Key);
                DeckManager.instance.deck.Table[obj.Key] = Name;
                Debug.Log("���g�F" + DeckManager.instance.deck.Table[obj.ToString()]);



                // �X�v���C�g��ς���
                //obj.Value.GetComponent<Image>().sprite = DeckManager.instance.disastar_baner[Name];
            }
        }
        

        // �����x��߂�
        setAlpha(GetComponent<Image>(), 1);

        // �h���b�O���̃I�u�W�F�N�g��j��
        Destroy(copyOBJ);
        Debug.Log("�h���b�O�I��");
    }

    private GameObject copy(GameObject source)
    {
        GameObject ret = UnityEngine.Object.Instantiate(source);
        // ���I�u�W�F�N�g�Ɠ����ʒu�Ɉړ�������
        ret.transform.SetParent(Canvas.transform, true);
        ret.transform.position = source.transform.position;
        // ���I�u�W�F�N�g�Ɠ����傫���ɂ���
        ret.transform.localScale = new Vector3(copyScale, copyScale, copyScale);
        return ret;
    }

    private void setAlpha(Image i, float value)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, value);
    }

    // �h���b�O�����I�u�W�F�N�g���w�肵���ꏊ�ɂ��邩
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

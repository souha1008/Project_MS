using UnityEngine;

[CreateAssetMenu]
public class EffectState_Meteor : BaseEffectState
{
    [Header("�����_����������ꏊ�ƃT�C�Y")]
    public Vector3 randomAreaSize;
    public Vector3 randomCenterPostion;

    [Header("�����������邩")]
    public int num;

    [Header("��������N�[���^�C���ӂ蕝")]
    public Vector2 cooltime;
}

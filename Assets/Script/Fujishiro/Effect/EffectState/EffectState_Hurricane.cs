using UnityEngine;

[CreateAssetMenu]
public class EffectState_Hurricane : BaseEffectState
{
    [Header("�����_����������ꏊ�ƃT�C�Y")]
    public Vector3 randomAreaSize;
    public Vector3 randomCenterPostion;

    [Header("�X�s�[�h")]
    public Vector2 speed;

    [Header("�T�C�Y")]
    public Vector2 size;

    [Header("�����������邩")]
    public int num;

    [Header("��������N�[���^�C���ӂ蕝")]
    public Vector2 cooltime;
}

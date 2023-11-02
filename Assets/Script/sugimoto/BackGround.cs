using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] UIController UiController;

    void Start()
    {
        //EventTrigger�R���|�[�l���g���擾
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        //�C�x���g�̐ݒ�ɓ���
        EventTrigger.Entry entry = new EventTrigger.Entry();

        //PointerDown(�������u�ԂɎ��s����)�C�x���g�^�C�v��ݒ�
        entry.eventID = EventTriggerType.PointerDown;

        //�֐���ݒ�
        entry.callback.AddListener((x) =>
        {
            UiController.BackToMenu();
        });

        //�C�x���g�̐ݒ��EventTrigger�ɔ��f
        eventTrigger.triggers.Add(entry);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] UIController UiController;

    void Start()
    {
        //EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        //イベントの設定に入る
        EventTrigger.Entry entry = new EventTrigger.Entry();

        //PointerDown(押した瞬間に実行する)イベントタイプを設定
        entry.eventID = EventTriggerType.PointerDown;

        //関数を設定
        entry.callback.AddListener((x) =>
        {
            UiController.BackToMenu();
        });

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
    }

}

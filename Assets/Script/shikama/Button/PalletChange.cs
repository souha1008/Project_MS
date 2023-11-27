using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PALLET
{ 
    FIRST,
    SECOND,
}

enum DIR
{
    UP,
    DOWN,
}

public class PalletChange : MonoBehaviour
{
    [SerializeField] GameObject pallet1st;
    [SerializeField] GameObject pallet2st;
    PALLET palletNow = PALLET.FIRST;

    RectTransform pallet1stPos;
    RectTransform pallet2stPos;

    Vector3 firstPos1st;
    Vector3 firstPos2st;

    [SerializeField] float moveTime = 1.0f;
    [SerializeField] float moveDist = 30.0f;
    float moveCount = 0;

    bool palletMove = false;
    bool palletHalfChange = false;

    Button[] firstButtons;
    Button[] secondButtons;

    Vector3 dragStartPos;
    Vector3 dragEndPos;

    DIR dir = DIR.UP;

    private void Start()
    {
        pallet1stPos = pallet1st.GetComponent<RectTransform>();
        pallet2stPos = pallet2st.GetComponent<RectTransform>();

        firstPos1st = pallet1stPos.localPosition;
        firstPos2st = pallet2stPos.localPosition;

        firstButtons = pallet1stPos.gameObject.GetComponentsInChildren<Button>();
        secondButtons = pallet2stPos.gameObject.GetComponentsInChildren<Button>();
    }


    private void Update()
    {
        if (palletMove)
        {
            PalletMove();
        }

        DragPalletChange();
    }

    private void DragPalletChange()
    {
        //マウスの座標を取得してスクリーン座標を更新
        Vector3 mousePositionScreen = Input.mousePosition;
        //スクリーン座標→ワールド座標
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        // Vector3 mousePosition = mousePositionScreen;
        
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragEndPos = mousePosition;
            float dist = Vector2.Distance(new Vector2(0, dragEndPos.y), new Vector2(0, dragStartPos.y));
            if (dist > 1.0f)
            {
                if (!palletMove)
                {
                    if (dragStartPos.y > dragEndPos.y) dir = DIR.DOWN;
                    else if (dragStartPos.y < dragEndPos.y) dir = DIR.UP;
                }
                Change();
            }
        }
    }

    public void Change()
    {
        if (!palletMove)
        {
            if (palletNow == PALLET.FIRST)
            {
                palletNow = PALLET.SECOND;
                palletMove = true;
            }
            else if (palletNow == PALLET.SECOND)
            {
                palletNow = PALLET.FIRST;
                palletMove = true;
            }
        }
    }

    private void PalletMove()
    {
        float halfTime = moveTime / 2;

        float moveDist_ = moveDist;
        if (dir == DIR.DOWN) moveDist_ *= -1;

        if (palletNow == PALLET.FIRST)
        {
            if (moveCount < moveTime / 2)
            {
                pallet1stPos.localPosition = Vector3.Lerp(firstPos2st, new Vector3(firstPos2st.x, firstPos2st.y - moveDist_, firstPos2st.z),
                    moveCount / halfTime);

                pallet2stPos.localPosition = Vector3.Lerp(firstPos1st, new Vector3(firstPos1st.x, firstPos1st.y + moveDist_, firstPos1st.z),
                    moveCount / halfTime);
            }
            else
            {
                if (!palletHalfChange)
                {
                    pallet2st.transform.SetAsFirstSibling();
                    palletHalfChange = true;

                    foreach (Button btn in firstButtons)
                    {
                        btn.interactable = true;
                    }

                    foreach (Button btn in secondButtons)
                    {
                        btn.interactable = false;
                    }
                }

                pallet1stPos.localPosition = Vector3.Lerp(new Vector3(firstPos1st.x, firstPos1st.y - moveDist_, firstPos2st.z), firstPos1st,
                    (moveCount - halfTime) / halfTime);

                pallet2stPos.localPosition = Vector3.Lerp(new Vector3(firstPos2st.x, firstPos2st.y + moveDist_, firstPos1st.z), firstPos2st,
                    (moveCount - halfTime) / halfTime);
            }
        }
        else if (palletNow == PALLET.SECOND)
        {
            if (moveCount < moveTime / 2)
            {
                pallet1stPos.localPosition = Vector3.Lerp(firstPos1st, new Vector3(firstPos1st.x, firstPos1st.y + moveDist_, firstPos1st.z),
                    moveCount / halfTime);

                pallet2stPos.localPosition = Vector3.Lerp(firstPos2st, new Vector3(firstPos2st.x, firstPos2st.y - moveDist_, firstPos2st.z),
                    moveCount / halfTime);
            }
            else
            {
                if (!palletHalfChange)
                {
                    pallet1st.transform.SetAsFirstSibling();
                    foreach (Button btn in firstButtons)
                    {
                        btn.interactable = false;
                    }

                    foreach (Button btn in secondButtons)
                    {
                        btn.interactable = true;
                    }
                    palletHalfChange = true;
                }

                pallet1stPos.localPosition = Vector3.Lerp(new Vector3(firstPos2st.x, firstPos2st.y + moveDist_, firstPos1st.z), firstPos2st,
                    (moveCount - halfTime) / halfTime);

                pallet2stPos.localPosition = Vector3.Lerp(new Vector3(firstPos1st.x, firstPos1st.y - moveDist_, firstPos2st.z), firstPos1st,
                    (moveCount - halfTime) / halfTime);
            }
        }

        moveCount += Time.deltaTime;

        if(moveCount >= moveTime)
        {
            palletMove = false;
            moveCount = 0;
            palletHalfChange = false;
        }
    }
}

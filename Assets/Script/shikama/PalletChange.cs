using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PALLET
{ 
    FIRST,
    SECOND,
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
    [SerializeField] float moveDist = 10.0f;
    float moveCount = 0;

    bool palletMove = false;
    bool palletHalfChange = false;

    Button[] firstButtons;
    Button[] secondButtons;

    private void Start()
    {
        pallet1stPos = pallet1st.GetComponent<RectTransform>();
        pallet2stPos = pallet2st.GetComponent<RectTransform>();

        firstPos1st = pallet1stPos.position;
        firstPos2st = pallet2stPos.position;

        firstButtons = pallet1stPos.gameObject.GetComponentsInChildren<Button>();
        secondButtons = pallet2stPos.gameObject.GetComponentsInChildren<Button>();
    }

    public void Change()
    {
        if(palletNow == PALLET.FIRST)
        {
            palletNow = PALLET.SECOND;
            palletMove = true;
        }
        else if(palletNow == PALLET.SECOND)
        {
            palletNow = PALLET.FIRST;
            palletMove = true;
        }
    }

    private void Update()
    {
        if (palletMove)
        {
            PalletMove();
        }
    }

    private void PalletMove()
    {
        float halfTime = moveTime / 2;

        if (palletNow == PALLET.FIRST)
        {
            if (moveCount < moveTime / 2)
            {
                pallet1stPos.position = Vector3.Lerp(firstPos2st, new Vector3(firstPos2st.x, firstPos2st.y - moveDist, firstPos2st.z),
                    moveCount / halfTime);

                pallet2stPos.position = Vector3.Lerp(firstPos1st, new Vector3(firstPos1st.x, firstPos1st.y + moveDist, firstPos1st.z),
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

                pallet1stPos.position = Vector3.Lerp(new Vector3(firstPos1st.x, firstPos2st.y - moveDist, firstPos2st.z), firstPos1st,
                    (moveCount - halfTime) / halfTime);

                pallet2stPos.position = Vector3.Lerp(new Vector3(firstPos2st.x, firstPos1st.y + moveDist, firstPos1st.z), firstPos2st,
                    (moveCount - halfTime) / halfTime);
            }
        }
        else if (palletNow == PALLET.SECOND)
        {
            if (moveCount < moveTime / 2)
            {
                pallet1stPos.position = Vector3.Lerp(firstPos1st, new Vector3(firstPos1st.x, firstPos1st.y + moveDist, firstPos1st.z),
                    moveCount / halfTime);

                pallet2stPos.position = Vector3.Lerp(firstPos2st, new Vector3(firstPos2st.x, firstPos2st.y - moveDist, firstPos2st.z),
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

                pallet1stPos.position = Vector3.Lerp(new Vector3(firstPos2st.x, firstPos1st.y + moveDist, firstPos1st.z), firstPos2st,
                    (moveCount - halfTime) / halfTime);

                pallet2stPos.position = Vector3.Lerp(new Vector3(firstPos1st.x, firstPos2st.y - moveDist, firstPos2st.z), firstPos1st,
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //３つのPanelを格納する変数
    //インスペクターウィンドウからゲームオブジェクトを設定する
    [SerializeField] GameObject StagePanel;
    [SerializeField] GameObject SubPanel;
    [SerializeField] GameObject Buttan01;
    [SerializeField] GameObject Buttan02;
    [SerializeField] GameObject Buttan03;


    // Start is called before the first frame update
    void Start()
    {
        //BackToMenuメソッドを呼び出す
        BackToMenu();
    }


    //StagePanelでStageButtonが押されたときの処理
    //SubPanelをアクティブにする
    public void SelectStage()
    {
        StagePanel.SetActive(false);
        SubPanel.SetActive(true);
        Buttan01.GetComponent<Button>().interactable = false;
        Buttan02.GetComponent<Button>().interactable = false;
        Buttan03.GetComponent<Button>().interactable = false;
    }


    //2つのDescriptionPanelでBackButtonが押されたときの処理
    //StagePanelをアクティブにする
    public void BackToMenu()
    {
        StagePanel.SetActive(true);
        SubPanel.SetActive(false);
        Buttan01.GetComponent<Button>().interactable = true;
        Buttan02.GetComponent<Button>().interactable = true;
        Buttan03.GetComponent<Button>().interactable = true;
    }
}
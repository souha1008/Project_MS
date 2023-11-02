using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //�R��Panel���i�[����ϐ�
    //�C���X�y�N�^�[�E�B���h�E����Q�[���I�u�W�F�N�g��ݒ肷��
    [SerializeField] GameObject StagePanel;
    [SerializeField] GameObject SubPanel;
    [SerializeField] GameObject Buttan01;
    [SerializeField] GameObject Buttan02;
    [SerializeField] GameObject Buttan03;


    // Start is called before the first frame update
    void Start()
    {
        //BackToMenu���\�b�h���Ăяo��
        BackToMenu();
    }


    //StagePanel��StageButton�������ꂽ�Ƃ��̏���
    //SubPanel���A�N�e�B�u�ɂ���
    public void SelectStage()
    {
        StagePanel.SetActive(false);
        SubPanel.SetActive(true);
        Buttan01.GetComponent<Button>().interactable = false;
        Buttan02.GetComponent<Button>().interactable = false;
        Buttan03.GetComponent<Button>().interactable = false;
    }


    //2��DescriptionPanel��BackButton�������ꂽ�Ƃ��̏���
    //StagePanel���A�N�e�B�u�ɂ���
    public void BackToMenu()
    {
        StagePanel.SetActive(true);
        SubPanel.SetActive(false);
        Buttan01.GetComponent<Button>().interactable = true;
        Buttan02.GetComponent<Button>().interactable = true;
        Buttan03.GetComponent<Button>().interactable = true;
    }
}
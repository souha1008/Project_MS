using UnityEngine;
using UnityEngine.SceneManagement;

public class re : MonoBehaviour
{
    public string sceneToLoad; // �؂�ւ���V�[���̖��O��Inspector�p�l������ݒ�

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // �}�E�X�̍��{�^�����N���b�N������
        //{
        //    LoadScene();
        //}
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
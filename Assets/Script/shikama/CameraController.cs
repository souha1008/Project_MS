using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject floor;
    Vector2 floorLeftBottom;
    Vector2 floorRightTop;

    [SerializeField] float maxCameraSizeMag = 5.0f;
    [SerializeField] float minCameraSizeMag = 2.0f;

    [SerializeField] float dragSpeedMag = 0.5f;
    [SerializeField] float dragDeadZone = 0.1f;

    float maxCameraSize;
    float minCameraSize;

    Vector3 oldMousePosition;

    private void Start()
    {
        // �X�}�z���ƂɃJ�����ʒu�A�T�C�Y������Ă��܂��̂ł����ŏ����ݒ�
        
        { // �J�����T�C�Y�ݒ�
            // ��i�c�����j�T�C�Y�Ɗ�A�X�y�N�g�䂩���������T�C�Y���Z�o
            var baseHorizontalSize = maxCameraSizeMag * 16.0f / 9.0f;
            // ��������T�C�Y�ƑΏۃA�X�y�N�g��őΏۏc�����T�C�Y���Z�o
            var verticalSize = baseHorizontalSize / Camera.main.aspect;

            // �����J�����T�C�Y�A�ő�T�C�Y�A�ŏ��T�C�Y�ݒ�
            Camera.main.orthographicSize = maxCameraSize = verticalSize;
            minCameraSize = minCameraSizeMag * 16.0f / 9.0f / Camera.main.aspect;
        }

        { // �J�����ʒu�ݒ�
            // �J�����̎l���ʒu�擾
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        
            // �X�e�[�W���̎l���ʒu�擾
            floorLeftBottom.Set(floor.transform.position.x - floor.transform.localScale.x / 2,
                floor.transform.position.y - floor.transform.localScale.y / 2);
            floorRightTop.Set(floor.transform.position.x + floor.transform.localScale.x / 2,
                floor.transform.position.y + floor.transform.localScale.y / 2);

            // �J�������n�ʂ̉����f���Ȃ��悤�ݒ�
            Camera.main.transform.SetPositionY(floorLeftBottom.y + Mathf.Abs(rightTop.y - leftBottom.y) / 2);
        }
    }

    

    private void Update()
    {
        MouseScrollZoom();
        DragWidthScroll();
    }

    // �}�E�X�X�N���[���ɂ���ʃY�[�������邽�߂̊֐�
    void MouseScrollZoom()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            // �J�����T�C�Y���w��͈͓��Őݒ�
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize += 0.1f, minCameraSize, maxCameraSize);

            // �J�����l�����[���h���W
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

            // �J�������n�ʂ̉����ڂ��Ȃ��悤�ݒ�
            Camera.main.transform.SetPositionY(floorLeftBottom.y + Mathf.Abs(rightTop.y - leftBottom.y) / 2);

            if (leftBottom.x < floorLeftBottom.x)
            {
                Debug.Log("aaa");
                Camera.main.transform.SetPositionX(floorLeftBottom.x + Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            }

            if (rightTop.x > floorRightTop.x)
            {

                Debug.Log("bbb");
                Camera.main.transform.SetPositionX(floorRightTop.x - Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            // �J�����T�C�Y���w��͈͓��Őݒ�
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= 0.1f, minCameraSize, maxCameraSize);

            // �J�����l�����[���h���W
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

            // �J�������n�ʂ̉����ڂ��Ȃ��悤�ݒ�
            Camera.main.transform.SetPositionY(floorLeftBottom.y + Mathf.Abs(rightTop.y - leftBottom.y) / 2);
        }
    }

    // �}�E�X�h���b�O�ɂ�鉡�X�N���[���̊֐�
    void DragWidthScroll()
    {
        //�}�E�X�̍��W���擾���ăX�N���[�����W���X�V
        Vector3 mousePositionScreen = Input.mousePosition;
        //�X�N���[�����W�����[���h���W
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        if (Input.GetMouseButton(0))
        {
            // ���X�N���[��
            if (oldMousePosition.x < mousePosition.x)
            {
                // �J�����l�����[���h���W
                Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

                // �O�t���[���Ɣ�ׂĂǂꂾ�����������擾
                float dragAmount = Mathf.Abs(oldMousePosition.x - mousePosition.x);

                if (dragAmount > dragDeadZone) // �h���b�O�f�b�h�]�[��
                {
                    // �J�����|�W�V�����ړ�
                    if (leftBottom.x - dragAmount > floorLeftBottom.x)
                        Camera.main.transform.AddPositionX(-dragAmount * dragSpeedMag);
                    else
                        Camera.main.transform.SetPositionX(floorLeftBottom.x + Mathf.Abs(rightTop.x - leftBottom.x) / 2);
                }
            }

            // �E�X�N���[��
            if (oldMousePosition.x > mousePosition.x)
            {
                // �J�����l�����[���h���W
                Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

                // �O�t���[���Ɣ�ׂĂǂꂾ�����������擾
                float dragAmount = Mathf.Abs(oldMousePosition.x - mousePosition.x);
                if (dragAmount > dragDeadZone) // �h���b�O�f�b�h�]�[��
                {
                    // �J�����|�W�V�����ړ�
                    if (rightTop.x + dragAmount < floorRightTop.x)
                        Camera.main.transform.AddPositionX(dragAmount * dragSpeedMag);
                    else
                        Camera.main.transform.SetPositionX(floorRightTop.x - Mathf.Abs(rightTop.x - leftBottom.x) / 2);
                }
            }
        }

        // 1�t���[���O�̃}�E�X���W�ۑ�
        oldMousePosition = mousePosition;
    }
}

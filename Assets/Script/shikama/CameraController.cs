using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject floor;
    Vector2 floorLeftBottom;
    Vector2 floorRightTop;

    float maxCameraSizeMag = 5.0f;
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
            maxCameraSizeMag *= floor.transform.localScale.x / 18.0f;
            var baseHorizontalSize = maxCameraSizeMag * 16.0f / 9.0f;
           
            // �ő�T�C�Y�A�ŏ��T�C�Y�ݒ�    
            maxCameraSize = baseHorizontalSize / Camera.main.aspect; 
            minCameraSize = minCameraSizeMag * 16.0f / 9.0f / Camera.main.aspect;
            
            // �J�������ő�T�C�Y�ɐݒ�
            Camera.main.orthographicSize = maxCameraSize;
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
            Camera.main.transform.SetPositionX(floorRightTop.x - Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            

            if (leftBottom.x < floorLeftBottom.x)
            {
                Camera.main.transform.SetPositionX(floorLeftBottom.x + Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            }

            if (rightTop.x > floorRightTop.x)
            {
                Camera.main.transform.SetPositionX(floorRightTop.x - Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            }
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
                Camera.main.transform.SetPositionX(floorLeftBottom.x + Mathf.Abs(rightTop.x - leftBottom.x) / 2);
            }

            if (rightTop.x > floorRightTop.x)
            {
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
        if (Camera.main.orthographicSize == maxCameraSize) return;

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

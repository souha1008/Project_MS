using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFILL : MonoBehaviour
{
	[SerializeField] Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		FillScreen();
    }

	void FillScreen()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		// �J�����̊O�g�̃X�P�[�������[���h���W�n�Ŏ擾
		float worldScreenHeight = MainCamera.orthographicSize * 2f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		// �X�v���C�g�̃X�P�[�������[���h���W�n�Ŏ擾
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		//  ���҂̔䗦���o���ăX�v���C�g�̃��[�J�����W�n�ɔ��f
		transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);

		// �J�����̒��S�ƃX�v���C�g�̒��S�����킹��
		Vector3 camPos = MainCamera.transform.position;
		camPos.z = 0;
		transform.position = camPos;
	}
}

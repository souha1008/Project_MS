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
        // スマホごとにカメラ位置、サイズがずれてしまうのでここで初期設定
        
        { // カメラサイズ設定
            // 基準（縦方向）サイズと基準アスペクト比から基準横方向サイズを算出
            maxCameraSizeMag *= floor.transform.localScale.x / 18.0f;
            var baseHorizontalSize = maxCameraSizeMag * 16.0f / 9.0f;
           
            // 最大サイズ、最小サイズ設定    
            maxCameraSize = baseHorizontalSize / Camera.main.aspect; 
            minCameraSize = minCameraSizeMag * 16.0f / 9.0f / Camera.main.aspect;
            
            // カメラを最大サイズに設定
            Camera.main.orthographicSize = maxCameraSize;
        }

        { // カメラ位置設定
            // カメラの四隅位置取得
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        
            // ステージ床の四隅位置取得
            floorLeftBottom.Set(floor.transform.position.x - floor.transform.localScale.x / 2,
                floor.transform.position.y - floor.transform.localScale.y / 2);
            floorRightTop.Set(floor.transform.position.x + floor.transform.localScale.x / 2,
                floor.transform.position.y + floor.transform.localScale.y / 2);

            // カメラが地面の下を映さないよう設定
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

    // マウススクロールによる画面ズームをするための関数
    void MouseScrollZoom()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            // カメラサイズを指定範囲内で設定
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize += 0.1f, minCameraSize, maxCameraSize);

            // カメラ四隅ワールド座標
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

            // カメラが地面の下を移さないよう設定
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
            // カメラサイズを指定範囲内で設定
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= 0.1f, minCameraSize, maxCameraSize);

            // カメラ四隅ワールド座標
            Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

            // カメラが地面の下を移さないよう設定
            Camera.main.transform.SetPositionY(floorLeftBottom.y + Mathf.Abs(rightTop.y - leftBottom.y) / 2);
        }
    }

    // マウスドラッグによる横スクロールの関数
    void DragWidthScroll()
    {
        if (Camera.main.orthographicSize == maxCameraSize) return;

        //マウスの座標を取得してスクリーン座標を更新
        Vector3 mousePositionScreen = Input.mousePosition;
        //スクリーン座標→ワールド座標
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        if (Input.GetMouseButton(0))
        {
            // 左スクロール
            if (oldMousePosition.x < mousePosition.x)
            {
                // カメラ四隅ワールド座標
                Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

                // 前フレームと比べてどれだけ動いたか取得
                float dragAmount = Mathf.Abs(oldMousePosition.x - mousePosition.x);

                if (dragAmount > dragDeadZone) // ドラッグデッドゾーン
                {
                    // カメラポジション移動
                    if (leftBottom.x - dragAmount > floorLeftBottom.x)
                        Camera.main.transform.AddPositionX(-dragAmount * dragSpeedMag);
                    else
                        Camera.main.transform.SetPositionX(floorLeftBottom.x + Mathf.Abs(rightTop.x - leftBottom.x) / 2);
                }
            }

            // 右スクロール
            if (oldMousePosition.x > mousePosition.x)
            {
                // カメラ四隅ワールド座標
                Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                Vector3 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

                // 前フレームと比べてどれだけ動いたか取得
                float dragAmount = Mathf.Abs(oldMousePosition.x - mousePosition.x);
                if (dragAmount > dragDeadZone) // ドラッグデッドゾーン
                {
                    // カメラポジション移動
                    if (rightTop.x + dragAmount < floorRightTop.x)
                        Camera.main.transform.AddPositionX(dragAmount * dragSpeedMag);
                    else
                        Camera.main.transform.SetPositionX(floorRightTop.x - Mathf.Abs(rightTop.x - leftBottom.x) / 2);
                }
            }
        }

        // 1フレーム前のマウス座標保存
        oldMousePosition = mousePosition;
    }
}

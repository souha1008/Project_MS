using UnityEngine;

// transformクラスに関数を追加するクラス

public static class TransformAddMethod
{
    static Vector3 vector3;

    #region SetPosition

    /// <summary>
    /// x,y,zによるポジションセット
    /// </summary>
    public static void SetPosition(this Transform transform, float x, float y, float z)
    {
        vector3.Set(x, y, z);
        transform.position = vector3;
    }
    /// <summary>
    /// x座標セット
    /// </summary>
    public static void SetPositionX(this Transform transform, float x)
    {
        vector3.Set(x, transform.position.y, transform.position.z);
        transform.position = vector3;
    }
    /// <summary>
    /// y座標セット
    /// </summary>
    public static void SetPositionY(this Transform transform, float y)
    {
        vector3.Set(transform.position.x, y, transform.position.z);
        transform.position = vector3;
    }
    /// <summary>
    /// z座標セット
    /// </summary>
    public static void SetPositionZ(this Transform transform, float z)
    {
        vector3.Set(transform.position.x, transform.position.y, z);
        transform.position = vector3;
    }
    #endregion

    #region AddPosition
    /// <summary>
    /// x,y,z座標加算
    /// </summary>
    public static void AddPosition(this Transform transform, float x, float y, float z)
    {
        vector3.Set(transform.position.x + x, transform.position.y + y, transform.position.z + z);
        transform.position = vector3;
    }
    /// <summary>
    /// x座標加算
    /// </summary>
    public static void AddPositionX(this Transform transform, float x)
    {
        vector3.Set(transform.position.x + x, transform.position.y, transform.position.z);
        transform.position = vector3;
    }
    /// <summary>
    /// y座標加算
    /// </summary>
    public static void AddPositionY(this Transform transform, float y)
    {
        vector3.Set(transform.position.x, transform.position.y + y, transform.position.z);
        transform.position = vector3;
    }
    /// <summary>
    /// z座標加算
    /// </summary>
    public static void AddPositionZ(this Transform transform, float z)
    {
        vector3.Set(transform.position.x, transform.position.y, transform.position.z + z);
        transform.position = vector3;
    }
    #endregion

    #region SetLocalPosition
    public static void SetLocalPosition(this Transform transform, float x, float y, float z)
    {
        vector3.Set(x, y, z);
        transform.localPosition = vector3;
    }
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        vector3.Set(x, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = vector3;
    }
    public static void SetLocalPositionY(this Transform transform, float y)
    {
        vector3.Set(transform.localPosition.x, y, transform.localPosition.z);
        transform.localPosition = vector3;
    }
    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        vector3.Set(transform.localPosition.x, transform.localPosition.y, z);
        transform.localPosition = vector3;
    }
    #endregion

    #region AddLocalPosition
    public static void AddLocalPosition(this Transform transform, float x, float y, float z)
    {
        vector3.Set(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
        transform.localPosition = vector3;
    }
    public static void AddLocalPositionX(this Transform transform, float x)
    {
        vector3.Set(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = vector3;
    }
    public static void AddLocalPositionY(this Transform transform, float y)
    {
        vector3.Set(transform.localPosition.x, transform.localPosition.y + y, transform.localPosition.z);
        transform.localPosition = vector3;
    }
    public static void AddLocalPositionZ(this Transform transform, float z)
    {
        vector3.Set(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
        transform.localPosition = vector3;
    }
    #endregion

    #region SetLocalScale
    public static void SetLocalScale(this Transform transform, float x, float y, float z)
    {
        vector3.Set(x, y, z);
        transform.localScale = vector3;
    }
    public static void SetLocalScaleX(this Transform transform, float x)
    {
        vector3.Set(x, transform.localScale.y, transform.localScale.z);
        transform.localScale = vector3;
    }
    public static void SetLocalScaleY(this Transform transform, float y)
    {
        vector3.Set(transform.localScale.x, y, transform.localScale.z);
        transform.localScale = vector3;
    }
    public static void SetLocalScaleZ(this Transform transform, float z)
    {
        vector3.Set(transform.localScale.x, transform.localScale.y, z);
        transform.localScale = vector3;
    }
    #endregion

    #region AddLocalScale
    public static void AddLocalScale(this Transform transform, float x, float y, float z)
    {
        vector3.Set(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
        transform.localScale = vector3;
    }
    public static void AddLocalScaleX(this Transform transform, float x)
    {
        vector3.Set(transform.localScale.x + x, transform.localScale.y, transform.localScale.z);
        transform.localScale = vector3;
    }
    public static void AddLocalScaleY(this Transform transform, float y)
    {
        vector3.Set(transform.localScale.x, transform.localScale.y + y, transform.localScale.z);
        transform.localScale = vector3;
    }
    public static void AddLocalScaleZ(this Transform transform, float z)
    {
        vector3.Set(transform.localScale.x, transform.localScale.y, transform.localScale.z + z);
        transform.localScale = vector3;
    }
    #endregion
}
using UnityEngine;

public class scr_TestPlayer : MonoBehaviour
{
    float moveSpeed;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        Move(moveSpeed);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        moveSpeed = 5;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="_speed">移動速度</param>
    void Move(float _speed)
    {
        transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _speed);
    }
}

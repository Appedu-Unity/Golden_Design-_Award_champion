using UnityEngine;
namespace WIE
{
    public class player : MonoBehaviour
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
        /// ��l��
        /// </summary>
        void Initialize()
        {
            moveSpeed = 5;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_speed">���ʳt��</param>
        void Move(float _speed)
        {
            transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _speed);
        }
    }
}

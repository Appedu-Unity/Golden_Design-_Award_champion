using UnityEngine;
namespace WIE
{
    public class wei_player : MonoBehaviour
    {
        float moveSpeed;
        private Rigidbody rig;
        private CapsuleCollider cal; 

        void Start()
        {
            Initialize();
        }
        void Awake()
        {
            rig = GetComponent<Rigidbody>();
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

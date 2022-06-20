using UnityEngine;

namespace WEI
{
    public class Camera : MonoBehaviour
    {
        GameObject player;

        void Awake()
        {
            player = GameObject.Find("Player");
        }

        void Update()
        {
            Track();
        }

        /// <summary>
        /// Äá¼v¾÷°lÂÜª±®a
        /// </summary>
        void Track()
        {
            Transform _player = player.transform;
            Vector3 temp = new Vector3(_player.position.x, 10f, _player.position.z);

            transform.position = temp;
        }
    }
}

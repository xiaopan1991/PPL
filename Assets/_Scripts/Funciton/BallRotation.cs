using UnityEngine;

namespace Assets._Scripts.Funciton
{
    public class BallRotation : MonoBehaviour
    {
        public bool Pause = false;
        public float Rate = 1.0f;

        private const float Speed = 90.0f;	

        void FixedUpdate () {
            if (!Pause)
            {
                this.transform.Rotate(Speed*Rate*Time.deltaTime, 0, 0, Space.Self);
            }
        }
    }
}

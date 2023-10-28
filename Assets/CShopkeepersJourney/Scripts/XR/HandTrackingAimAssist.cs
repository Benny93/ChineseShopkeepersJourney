using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames
{


    public class HandTrackingAimAssist : MonoBehaviour
    {
        public static HandTrackingAimAssist Instance { get; private set; }

        public float throwForce = 10f; // Adjust the throw force as needed.

        public Transform ThrowTarget;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ThrowItemAtDefaultTarget(Rigidbody  rigidbody) {

            // Calculate the direction from the hand to the target position.
            Vector3 throwDirection = (ThrowTarget.position - rigidbody.position).normalized;
            rigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        }

    }

}

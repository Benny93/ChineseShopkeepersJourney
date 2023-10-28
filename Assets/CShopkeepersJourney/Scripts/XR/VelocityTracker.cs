using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames {

    using UnityEngine;

    public class VelocityTracker : MonoBehaviour
    {
        public int velocitySamples = 5; // Number of samples to use for velocity calculation.

        private Vector3[] velocityOverTime;
        private Vector3 lastPosition;

        // Initialize the array and other variables.
        void Start()
        {
            velocityOverTime = new Vector3[velocitySamples];
            lastPosition = transform.position;
        }

        // Update the velocity array and calculate velocity.
        void FixedUpdate()
        {
            Vector3 displacement = transform.position - lastPosition;

            // Shift previous samples.
            for (int i = velocitySamples - 1; i > 0; i--)
            {
                velocityOverTime[i] = velocityOverTime[i - 1];
            }

            // Store the current displacement as the first sample.
            velocityOverTime[0] = displacement;
            lastPosition = transform.position;
        }

        // Get the current velocity.
        public Vector3 GetVelocity()
        {
            Vector3 averageVelocity = Vector3.zero;

            for (int i = 0; i < velocitySamples; i++)
            {
                averageVelocity += velocityOverTime[i];
            }

            return averageVelocity / velocitySamples;
        }
    }


}

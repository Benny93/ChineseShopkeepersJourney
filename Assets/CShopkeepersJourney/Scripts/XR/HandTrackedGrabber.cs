using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.vollmergames
{
   

    public class HandTrackedGrabber : MonoBehaviour
    {
        public PXR_Hand m_Hand;
       

        private HandGrabbable currentGrabbable;
        private bool isInHand = false;       


        private void OnTriggerEnter(Collider other)
        {
            //Debug.LogWarning(other.name + "Entered");
            currentGrabbable = other.GetComponent<HandGrabbable>();
        }
        private void OnTriggerStay(Collider other)
        {
            //Debug.LogWarning(other.name + "OnTriggerStay");
            if (m_Hand != null)
            {
                if (m_Hand.Pinch && !isInHand)
                {
                    currentGrabbable = other.GetComponent<HandGrabbable>();
                    if (currentGrabbable != null)
                    {
                        Debug.Log("Grab Item!");
                        currentGrabbable.transform.parent = transform;
                        currentGrabbable.transform.localPosition = Vector3.zero;                        
                        isInHand = true;
                        currentGrabbable.SelectEntered();
                    }
                }
                if (!m_Hand.Pinch && isInHand)
                {
                    currentGrabbable = other.GetComponent<HandGrabbable>();
                    if (currentGrabbable != null)
                    {
                        isInHand = false;
                        currentGrabbable.transform.parent = null;
                        Debug.Log("Release Grabbable!");
                        currentGrabbable.SelectExited();

                      
                        // Clear the reference to the currentGrabbable.
                        currentGrabbable = null;

                    }

                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //Debug.LogWarning(other.name + "OnTriggerExit");
            currentGrabbable = null;
        }

    


    }

}
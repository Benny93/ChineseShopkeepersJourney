using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.vollmergames
{

    public class HandGrabbable : MonoBehaviour
    {

        public delegate void HandGrabbableDelegate();

        public UnityEvent SelectEnteredEvent;
        public UnityEvent SelectExitedEvent;
        public event HandGrabbableDelegate OnSelectEntered;
        public event HandGrabbableDelegate OnSelectExited;
       

        private Rigidbody rigb;
        private Vector3 previousPosition;
        private VelocityTracker velocityTracker;
        private void Awake()
        {
            rigb = GetComponent<Rigidbody>();
        }

        internal void SelectEntered()
        {

            rigb.isKinematic = true;
            OnSelectEntered?.Invoke();
            SelectEnteredEvent.Invoke();

        }

        internal void SelectExited()
        {
            rigb.isKinematic = false;
            OnSelectExited?.Invoke();
            SelectExitedEvent.Invoke();

            HandTrackingAimAssist.Instance.ThrowItemAtDefaultTarget(rigb);

        }

        private void Update()
        {
            previousPosition = transform.position;
        }
    }

}

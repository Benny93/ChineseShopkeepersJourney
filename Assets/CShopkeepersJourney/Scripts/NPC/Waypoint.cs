using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames
{

    public class Waypoint : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }
        public bool CanOrder = false;

        // This method marks the waypoint as occupied.
        public void Occupy()
        {
            IsOccupied = true;
        }

        // This method marks the waypoint as vacant.
        public void Vacate()
        {
            IsOccupied = false;
        }

        private void OnDrawGizmos()
        {
            // Draw a green Gizmo sphere when the waypoint is occupied.
            Gizmos.color = IsOccupied ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }

        
    }

}

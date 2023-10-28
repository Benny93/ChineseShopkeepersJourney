using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.vollmergames
{
    public class CustomerManager : MonoBehaviour
    {
        public Waypoint[] waypoints;
        public GameObject customerPrefab;
        public Transform SpawnPoint;
        public Waypoint exit;
        public List<GameObject> CustomerAvatars;
        private Queue<Waypoint> waypointQueue = new Queue<Waypoint>();
        private Queue<Customer> customerQueue = new Queue<Customer>();

        public float spawnDelay = 2.0f; // Adjust this to set the delay between customer spawns.
        private Coroutine spawnRoutine;
        private void Start()
        {
            GameManager.Instance.OnGameStart += HandleGameStart;
            GameManager.Instance.OnGameEnd += HandleGameEnd;
            GameManager.Instance.OnGameAbort += HandleGameEnd;
            InitializeQueue();
        }

        private void InitializeQueue()
        {
            foreach (var waypoint in waypoints)
            {
                waypointQueue.Enqueue(waypoint);
            }
          
        }

        private void HandleGameEnd()
        {
            StopCoroutine(spawnRoutine);

            foreach (var customer in customerQueue)
            {
                customer.OnOrderCompleted -= HandleOrderComplete;
                Destroy(customer.gameObject);
            }
            customerQueue.Clear();
            foreach (var wp in waypoints) {
                wp.Vacate();
            }
            waypointQueue.Clear();
            InitializeQueue();
        }

        private void HandleGameStart()
        {
           spawnRoutine =  StartCoroutine(SpawnCustomerWithDelay());
        }

        private IEnumerator SpawnCustomerWithDelay()
        {
            while (true) // This coroutine will keep running indefinitely.
            {
                yield return new WaitForSeconds(spawnDelay);

                
                if (waypointQueue.Count > 0)
                {
                    SpawnCustomer();
                }
            }
        }

        private void SpawnCustomer()
        {
            Waypoint targetWaypoint = waypointQueue.Dequeue();
            var spawnPos = FindClosestNavMeshPoint(SpawnPoint.position);
            GameObject newCustomer = Instantiate(customerPrefab, spawnPos, Quaternion.identity);

          
            Customer c = newCustomer.GetComponent<Customer>();
            c.SetWaypoint(targetWaypoint);
            c.RequestableItems = GameManager.Instance.currentLevel.learningItems;
            c.OnOrderCompleted += HandleOrderComplete;

            if (CustomerAvatars.Count > 0) {
                // Generate a random index within the range of the list.
                int randomIndex = UnityEngine.Random.Range(0, CustomerAvatars.Count);
                // Get the GameObject at the random index.
                GameObject randomAvatar = CustomerAvatars[randomIndex];
                c.AvatarPrefab = randomAvatar;
            }
          

            // Occupy the waypoint.
            targetWaypoint.GetComponent<Waypoint>().Occupy();
            customerQueue.Enqueue(c);
            
        }

        private void HandleOrderComplete(Customer customer)
        {
            GameManager.Instance.PlayerScored();

            var wp = customer.GetCurrentWaypoint();
            wp.Vacate();
            customer.SetWaypoint(exit);
            customerQueue.Dequeue();
            // the other customers should now move to the next free waypoint
            foreach (var cc in customerQueue)
            {            
                var ccwp = cc.GetCurrentWaypoint();
                cc.SetWaypoint(wp);
                wp = ccwp;                
            }
            waypointQueue.Enqueue(wp);

        }

     

        // Method to find the closest point on the NavMesh to a given point.
        public Vector3 FindClosestNavMeshPoint(Vector3 targetPoint)
        {
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(targetPoint, out hit, Mathf.Infinity, UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                // If no valid point on the NavMesh was found, return the original point.
                return targetPoint;
            }
        }
    }

}
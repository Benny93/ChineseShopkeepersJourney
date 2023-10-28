using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.vollmergames
{
    public class Customer : MonoBehaviour
    {
        public delegate void CustomerCallback(Customer customer);

        public CustomerCallback OnOrderCompleted;
        public float destinationThreshold = 0.1f;
        public List<ChineseLearningItem> RequestableItems;
        public AudioClip OrderCompleteSound;
        public float RepeatAfterSeconds = 3f;
        public AudioSource OrderAudioSource;
        public GameObject AvatarPrefab;
        public Transform AvatarOffset;

        [SerializeField]
        private ChineseLearningItem currentRequestedItem;

        [SerializeField]
        private Waypoint currentWaypoint;
        private NavMeshAgent navMeshAgent;
        
        public AudioSource audioSource;

        private Animator avatarAnimator;

        private bool hasPerformedOrder = false;
        private bool orderIsPending = false;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            // Ensure the NavMeshAgent component is attached to the customer GameObject.
            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component is missing on the customer GameObject.");
            }

            var avatar = Instantiate(AvatarPrefab, AvatarOffset);
            avatarAnimator = avatar.GetComponent<Animator>();

        }

        private void Update()
        {

            // Check if there's a current waypoint to move towards.
            if (currentWaypoint != null)
            {
                // Draw a debug line between the customer and the waypoint.
                Debug.DrawLine(transform.position, currentWaypoint.transform.position, Color.green);


                float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.transform.position);

                if (currentWaypoint.CanOrder && distanceToWaypoint <= destinationThreshold && !hasPerformedOrder)
                {
                    hasPerformedOrder = true;
                    PerformOrder();
                }

                if (distanceToWaypoint <= destinationThreshold) {
                   var mainCam = Camera.main;

                    Vector3 lookAtPosition = new Vector3(mainCam.transform.position.x, transform.position.y, mainCam.transform.position.z);
                    Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
                    targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
                    transform.rotation = targetRotation;
                }
                else
                {
                    roateTowardsWaypoint();
                }
            }

            if (avatarAnimator != null && navMeshAgent != null)
            {
                // Calculate normalized speed (between 0 and 1) based on the NavMeshAgent's speed.
                float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;

                // Clamp the value to ensure it stays within the range [0, 1].
                normalizedSpeed = Mathf.Clamp(normalizedSpeed, 0f, 1f);
                avatarAnimator.SetFloat("x", normalizedSpeed);
            }

           
        }

        private void roateTowardsWaypoint()
        {
            if (navMeshAgent != null && navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                // Get the agent's velocity.
                Vector3 velocity = navMeshAgent.velocity;

                // Ignore changes in the Y-axis (vertical) to avoid tilting.
                velocity.y = 0;

                // Calculate the rotation to look at the velocity direction.
                Quaternion targetRotation = Quaternion.LookRotation(velocity);

                // Apply the rotation to your GameObject.
                transform.rotation = targetRotation;
            }
        }

        public void PerformOrder()
        {                 
            if (RequestableItems.Count > 0)
            {
                // Choose a random item from RequestableItems
                int randomIndex = (int)(UnityEngine.Random.Range(0f, 1f) * RequestableItems.Count);
                currentRequestedItem = RequestableItems[randomIndex];
                orderIsPending = true;
                StartCoroutine(RepeatRequest());
            }
            else
            {
                Debug.LogError("No requestable items!");
            }
        }

        IEnumerator RepeatRequest() {
            while (orderIsPending) {
                OrderAudioSource.PlayOneShot(currentRequestedItem.audioFile);
                yield return new WaitForSeconds(RepeatAfterSeconds);
            }
            
        }

        public void CompleteOrder()
        {
            if (!orderIsPending) {
                return;
            }
            if (audioSource != null && OrderCompleteSound != null)
            {
                audioSource.PlayOneShot(OrderCompleteSound);
            }
            orderIsPending = false;
            OnOrderCompleted(this);
        }

        public void SetWaypoint(Waypoint nextWaypoint)
        {
            currentWaypoint = nextWaypoint;
            Invoke("MoveToTarget", 1f);

        }

        void MoveToTarget()
        {
            // Move the customer towards the current waypoint.
            navMeshAgent.SetDestination(currentWaypoint.transform.position);

        }

        public Waypoint GetCurrentWaypoint()
        {
            return currentWaypoint;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Check if the collided object has a ShopItem component.
            ShopItem shopItem = collision.gameObject.GetComponent<ShopItem>();

            if (shopItem != null && shopItem.LearningItem == currentRequestedItem)
            {
                Destroy(shopItem.gameObject);             

                // Call the OrderComplete function.
                CompleteOrder();
            }
        }

    }


}
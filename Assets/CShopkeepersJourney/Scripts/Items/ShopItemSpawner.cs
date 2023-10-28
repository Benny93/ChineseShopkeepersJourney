using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames
{
    public class ShopItemSpawner : MonoBehaviour
    {
        public GameObject shopItemPrefab;
        public ChineseLearningItem LearningItem;
        public float maxDistanceForNewSpawn = 10f;

        private GameObject currentShopItem;

        private bool shouldSpawn = false;

        private void Update()
        {
            if (!shouldSpawn) {
                return;
            }
            if (currentShopItem == null || Vector3.Distance(currentShopItem.transform.position, transform.position) > maxDistanceForNewSpawn)
            {
                SpawnShopItem();
            }
        }

        private void SpawnShopItem()
        {
            if (shopItemPrefab != null)
            {
                if (currentShopItem != null)
                {
                    Destroy(currentShopItem,3f);
                }

                currentShopItem = Instantiate(shopItemPrefab, transform.position, Quaternion.identity);
                ShopItem shopItem = currentShopItem.GetComponent<ShopItem>();

                shopItem.SetLearningItem(LearningItem);
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            if (currentShopItem) {
                // Debug line between spawner and the spawned item.
                Debug.DrawLine(transform.position, currentShopItem.transform.position, Color.blue, 2f);
            }            

        }

        public void Setup()
        {
            shouldSpawn = true;
        }

        public void ResetSpawner() {
            shouldSpawn = false;
            if (currentShopItem) {
                Destroy(currentShopItem);
            }
        }
    }
}
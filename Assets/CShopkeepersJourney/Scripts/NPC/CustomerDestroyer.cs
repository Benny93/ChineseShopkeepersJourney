using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.vollmergames
{

    public class CustomerDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var customer = other.gameObject.GetComponent<Customer>();
            if (customer)
            {
                Destroy(customer.gameObject);

            }
        }
    }

}

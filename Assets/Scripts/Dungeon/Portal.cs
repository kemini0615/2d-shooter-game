using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static event Action OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnPlayerEnter != null)
            {
                OnPlayerEnter.Invoke();
            }
        }
    }
}

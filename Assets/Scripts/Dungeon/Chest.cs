using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform itemPosition;
    
    // TEMP
    [SerializeField] private GameObject item;

    private Animator animator;
    private bool hasOpened;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ShowItem()
    {
        Instantiate(item, transform.position, Quaternion.identity, itemPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasOpened)
            return;

        if (!other.CompareTag("Player"))
            return;

        animator.SetTrigger("Open");
        ShowItem();
        hasOpened = true;
    }
}

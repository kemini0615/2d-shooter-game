using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayCloseAnimation()
    {
        animator.SetTrigger("Close");
    }

    public void PlayOpenAnimation()
    {
        animator.SetTrigger("Open");
    }
}

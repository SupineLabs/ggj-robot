using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAndOut : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Movement.Instance != null) transform.position = Movement.Instance.gameObject.transform.position;
    }

    public void FadeOut()
    {
        Movement.Instance.CanMove = false;
        _animator.Play("FadeOut");
    }

    public void FadeIn()
    {
        Movement.Instance.gameObject.transform.position += new Vector3(2, 0, 0);
        _animator.Play("FadeIn");
    }

    public void FinishedAnim()
    {
        if (Movement.Instance != null) Movement.Instance.CanMove = true;
    }
}

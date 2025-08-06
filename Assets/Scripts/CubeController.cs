using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rayDistance = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;
    private Animator animator;

    private bool isClicked = false;
    private bool shouldMove = false;

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.75f;
    [SerializeField] private float _rotationYValue = 15f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        if (isClicked) return;

        if (IsFrontBlocked())
        {
            transform.DOKill(true);
            transform.DOPunchRotation(new Vector3(0, _rotationYValue, 0), _animationDuration);
            return;
        }
        else
        {
            isClicked = true;
            animator.SetTrigger("FirstClick");
            StartCoroutine(BounceAndMove());
        }

    }

    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    private bool IsFrontBlocked()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.right;
        Ray ray = new Ray(origin, direction);

        return Physics.Raycast(ray, rayDistance, obstacleLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Gizmos.DrawRay(origin, transform.right * rayDistance);
    }

    private IEnumerator BounceAndMove()
    {
        Vector3 originalPos = transform.position;
        Vector3 bounceBackPos = originalPos - transform.right * 0.07f;

        transform.DOMove(bounceBackPos, 0.15f).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(0.1f);

        shouldMove = true;
    }
}

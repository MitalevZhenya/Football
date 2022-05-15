using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimator : MonoBehaviour
{
    [SerializeField] private Animator modelAnimator;
    [SerializeField] private Transform modelTransform;
    [SerializeField] private int throwBallActionNumber, idleAnimationNumber;
    [SerializeField] private float invokeThrowBallAnimation;
    [SerializeField] private Vector3 modelPos;
    private void Start()
    {
        modelPos = modelTransform.localPosition;
    }
    public void SetThrowBallAnimation()
    {
        modelAnimator.SetInteger("Action", throwBallActionNumber);
        StartCoroutine(SetIdleAnim());
    }
    IEnumerator SetIdleAnim()
    {
        yield return new WaitForSeconds(invokeThrowBallAnimation);
        modelAnimator.SetInteger("Action", idleAnimationNumber);
    }
    public void SetDefaultPosModel()
    {
        modelTransform.localPosition = modelPos;
    }
}
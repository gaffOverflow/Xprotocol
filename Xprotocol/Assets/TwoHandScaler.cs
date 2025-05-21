using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TwoHandScaler : MonoBehaviour
{
    public XRBaseInteractor leftHand;
    public XRBaseInteractor rightHand;

    private bool isLeftGrabbing = false;
    private bool isRightGrabbing = false;
    private float initialDistance;
    private Vector3 initialScale;

    void Update()
    {
        if (isLeftGrabbing && isRightGrabbing)
        {
            float currentDistance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            float scaleFactor = currentDistance / initialDistance;
            transform.localScale = initialScale * scaleFactor;
        }
    }

    public void OnLeftGrab()
    {
        isLeftGrabbing = true;
        CheckInitialValues();
    }

    public void OnLeftRelease()
    {
        isLeftGrabbing = false;
    }

    public void OnRightGrab()
    {
        isRightGrabbing = true;
        CheckInitialValues();
    }

    public void OnRightRelease()
    {
        isRightGrabbing = false;
    }

    private void CheckInitialValues()
    {
        if (isLeftGrabbing && isRightGrabbing)
        {
            initialDistance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            initialScale = transform.localScale;
        }
    }
}

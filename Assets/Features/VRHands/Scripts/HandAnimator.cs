using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    public OVRInput.Controller controller; // e.g., LTouch or RTouch

    private Animator handAnimator = null;

    private readonly List<Finger> grippingFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointingFingers = new List<Finger>()
    {
        new Finger(FingerType.Index)
    };

    private readonly List<Finger> primaryFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb)
    };

    private void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Read trigger and grip input values from Meta input system
        float gripValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller); // grip (middle-ring-pinky)
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller); // index trigger
        float thumbTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, controller) ? 1.0f : 0.0f; // thumb touch

        // Animate grip
        SetFingerAnimationValues(grippingFingers, gripValue);
        AnimateActionInput(grippingFingers);

        // Animate index
        SetFingerAnimationValues(pointingFingers, triggerValue);
        AnimateActionInput(pointingFingers);

        // Animate thumb touch
        SetFingerAnimationValues(primaryFingers, thumbTouch);
        AnimateActionInput(primaryFingers);
    }

    public void SetFingerAnimationValues(List<Finger> fingersToAnimate, float targetValue)
    {
        foreach (Finger finger in fingersToAnimate)
        {
            finger.target = targetValue;
        }
    }

    public void AnimateActionInput(List<Finger> fingersToAnimate)
    {
        foreach (Finger finger in fingersToAnimate)
        {
            var fingerName = finger.type.ToString();
            var animationBlendValue = finger.target;
            handAnimator.SetFloat(fingerName, animationBlendValue);
        }
    }
}

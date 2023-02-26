using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;


    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactableObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}
public class HapticController : MonoBehaviour
{
    public Haptic hapticOnActivated;
    public Haptic hapticOnSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticOnSelected.TriggerHaptic);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class GearLever : MonoBehaviour
{/*
    [SerializeField] InputActionReference rightHapticAction;*/
    HingeJoint hinge;
    [SerializeField] WheelController wheelController;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    public void Update()
    {
        float rot =hinge.angle;
        Debug.Log(rot);
        if(rot <= 40 && rot >= 35)
        {
            Debug.Log("+ve");
            wheelController.dir = 1f;
            //OpenXRInput.SendHapticImpulse(rightHapticAction, 0.3f, 0.1f, UnityEngine.InputSystem.XR.XRController.rightHand); //Right Hand Haptic Impulse

        }
        else if(rot <= -35 && rot >= -40)
        {
            Debug.Log("-ve");
            wheelController.dir = -1f;
            //OpenXRInput.SendHapticImpulse(rightHapticAction, 0.3f, 0.1f, UnityEngine.InputSystem.XR.XRController.rightHand); //Right Hand Haptic Impulse
        }
        else if (rot >= -3 && rot <= 3)
        {
            Debug.Log("+/-ve");
            wheelController.dir = 0f;
            //OpenXRInput.SendHapticImpulse(rightHapticAction, 0.3f, 0.1f, UnityEngine.InputSystem.XR.XRController.rightHand); //Right Hand Haptic Impulse
        }

    }

    public void OnDeselect()
    {
        float rot = hinge.angle;
        Debug.Log(rot);
        if (rot <= 40 && rot >= 13)
        {
            transform.localRotation = Quaternion.Euler(40f, transform.localEulerAngles.y, transform.localEulerAngles.z);

        }
        else if (rot <= -13 && rot >= -40)
        {
            transform.localRotation = Quaternion.Euler(-40f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else if (rot > -13 && rot < 13)
        {
            transform.localRotation = Quaternion.Euler(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}

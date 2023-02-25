using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class WheelController : MonoBehaviour
{

    [SerializeField] private WheelCollider wheelFL;
    [SerializeField] private WheelCollider wheelFR;
    [SerializeField] private WheelCollider wheelRL;
    [SerializeField] private WheelCollider wheelRR;

    [SerializeField] private Transform wheelFLTransform;
    [SerializeField] private Transform wheelFRTransform;
    [SerializeField] private Transform wheelRLTransform;
    [SerializeField] private Transform wheelRRTransform;

    public float acceleration = 500f;
    public float breakingForce = 200f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakforce = 0f;
    private float currentTurnAngle = 0f;
    private InputDevice rightController;
    private InputDevice leftController;

    List<InputDevice> rdevices = new List<InputDevice>();
    List<InputDevice> ldevices = new List<InputDevice>();

    bool breakDown = false;
    [SerializeField] private SteeringWheel steering;

    private void Start()
    {
        GetRightController();
        GetLeftController();
    }

    private void GetRightController()
    {
        var characteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristic, rdevices);
        rightController = rdevices.FirstOrDefault();
        Debug.Log("Controller : " + rightController.name);
    }

    private void GetLeftController()
    {
        var characteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristic, ldevices);
        leftController = ldevices.FirstOrDefault();
        Debug.Log("Controller : " + leftController.name);
    }

    void FixedUpdate()
    {
        if (ldevices.Count == 0 || rdevices.Count == 0)
        {
            GetRightController();
            GetLeftController();
        }

        ApplyAcceleration();
        ApplyBreak();
        TurnVehicle();
        UpdateWheel(wheelFLTransform, wheelFL);
        UpdateWheel(wheelFRTransform, wheelFR);
        UpdateWheel(wheelRLTransform, wheelRL);
        UpdateWheel(wheelRRTransform, wheelRR);
    }

    private void TurnVehicle()
    {
        currentTurnAngle = -steering.GetSteeringAngle() / 8;
        wheelFL.steerAngle = wheelFR.steerAngle = currentTurnAngle;
    }

    private void ApplyBreak()
    {
        leftController.TryGetFeatureValue(CommonUsages.trigger, out float breakForce);
        //Debug.Log("Breakforce : " + breakForce);
        if (breakForce > 0.5f)
        {
            currentBreakforce = breakingForce * breakingForce;
            breakDown = true;
        }
        else
        {
            currentBreakforce = 0f;
            breakDown = false;
        }

        if (breakDown)
        {
            wheelRL.brakeTorque = currentBreakforce;
            wheelRR.brakeTorque = currentBreakforce;
        }
    }

    private void ApplyAcceleration()
    {
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float speed);


        if (speed > 0.5f)
            currentAcceleration = acceleration * speed;
        else
        {
            currentAcceleration = acceleration * 0f;
        }


        wheelFL.motorTorque = currentAcceleration;
        wheelFR.motorTorque = currentAcceleration;
        Debug.LogWarning("Acce : " + currentAcceleration + " wheel RPM : " + wheelFL.rpm + "  wheel torque : " + wheelFL.motorTorque);

        if (!breakDown)
        {
            wheelRL.motorTorque = currentAcceleration;
            wheelRR.motorTorque = currentAcceleration;
        }


    }

    private void UpdateWheel(Transform wheel, WheelCollider col)
    {
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);

        wheel.position = pos;
        wheel.rotation = rot;

    }
}

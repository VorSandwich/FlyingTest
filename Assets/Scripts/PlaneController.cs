using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;

public class PlaneController : MonoBehaviour {

    InputDevice inputDevice;

    [SerializeField] private float m_horizontalTilt; //150
    [SerializeField] private float m_verticalTilt; //100

    [SerializeField] private float m_horizontalPrecision; //50
    [SerializeField] private float m_verticalPrecision; //50

    public float m_planeSpeed;
    public float m_planeAccelerationSpeed;

	// Use this for initialization
	void Start ()
    {
        Init();
	}

    void Init()
    {
        m_planeSpeed = 0f;

        inputDevice = InputManager.ActiveDevice;
        inputDevice.RightTrigger.LowerDeadZone = 0.05f;
        inputDevice.LeftTrigger.LowerDeadZone = 0.05f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        LeftJoystickTiltControl();
        BackTriggersAccelerationControl();


        SpeedCalculations();
    }

    private void LeftJoystickTiltControl()
    {
        if (inputDevice.LeftStick)
            StopCoroutine("BackToFront");

        if (!inputDevice.LeftBumper.IsPressed)
        {
            if (inputDevice.LeftStickX)
                transform.Rotate(-Vector3.forward, m_horizontalTilt * Time.deltaTime * inputDevice.LeftStickX, Space.Self);
            if (inputDevice.LeftStickY)
                transform.Rotate(Vector3.right, m_verticalTilt * Time.deltaTime * inputDevice.LeftStickY, Space.Self);
        }
        //L1 +
        else if (inputDevice.LeftBumper.IsPressed)
        {
            if (inputDevice.LeftStickX)
                transform.Rotate(Vector3.up, m_horizontalPrecision * Time.deltaTime * inputDevice.LeftStickX, Space.Self);
            if (inputDevice.LeftStickY)
                transform.Rotate(Vector3.right, m_verticalPrecision * Time.deltaTime * inputDevice.LeftStickY, Space.Self);
            if (inputDevice.LeftStickButton)
                StartCoroutine("BackToFront");
        }
    }

    private void BackTriggersAccelerationControl()
    {
        //R2
        if (inputDevice.RightTrigger)
        {
            m_planeSpeed += inputDevice.RightTrigger.Value * 10 * Time.deltaTime;
        }

        //L2
        if (inputDevice.LeftTrigger)
        {
            m_planeSpeed -= inputDevice.LeftTrigger.Value * 10 * Time.deltaTime;
        }
    }

    private void SpeedCalculations()
    {
        SpeedIncrease();
        SpeedDecrease();


        if (m_planeSpeed > 100)
        {
            m_planeSpeed = 100;
        }

        if (m_planeSpeed < 0)
        {
            m_planeSpeed = 0;
        }
    }

    IEnumerator BackToFront()
    {
        Vector3 forwardVector = Camera.main.transform.forward;
        while (Quaternion.FromToRotation(this.transform.forward, forwardVector) != Quaternion.Euler(Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(this.transform.forward, forwardVector);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime/2f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void SpeedIncrease()
    {

    }

    private void SpeedDecrease()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlaneController : MonoBehaviour {

    [Range(50, 250)] [SerializeField] private float m_horizontalTilt;
    [Range(50, 250)] [SerializeField] private float m_verticalTilt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var inputDevice = InputManager.ActiveDevice;

        transform.Rotate(-Vector3.forward, m_horizontalTilt * Time.deltaTime * inputDevice.LeftStickX, Space.World);
        transform.Rotate(Vector3.right, m_verticalTilt * Time.deltaTime * inputDevice.LeftStickY, Space.World);
    }
}

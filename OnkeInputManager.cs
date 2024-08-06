using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OnkeInputManager : MonoBehaviour
{
    public static OnkeInputManager Instance { get; private set; }

    public InputDeviceCharacteristics rControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    public InputDeviceCharacteristics lControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

    private InputDevice rightController;
    private InputDevice leftController;

    public bool rGrab { get; private set; }
    public bool lGrab { get; private set; }

    public bool rPrimary { get; private set; }
    public bool lPrimary { get; private set; }

    public bool rSecondary { get; private set; }
    public bool lSecondary { get; private set; }

    public Vector2 rStickAxis { get; private set; }
    public Vector2 lStickAxis { get; private set; }

    public bool rTrigger { get; private set; }
    public bool lTrigger { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeControllers();
    }

    void Update()
    {
        UpdateControllerStates();
    }

    private void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(rControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }

        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(lControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }

    private void UpdateControllerStates()
    {
        if (rightController.isValid)
        {
            bool grabValue, primaryValue, secondaryValue, triggerValue;
            Vector2 stickAxisValue;

            rightController.TryGetFeatureValue(CommonUsages.gripButton, out grabValue);
            rightController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryValue);
            rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue);
            rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out stickAxisValue);
            rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue);

            rGrab = grabValue;
            rPrimary = primaryValue;
            rSecondary = secondaryValue;
            rStickAxis = stickAxisValue;
            rTrigger = triggerValue;
        }

        if (leftController.isValid)
        {
            bool grabValue, primaryValue, secondaryValue, triggerValue;
            Vector2 stickAxisValue;

            leftController.TryGetFeatureValue(CommonUsages.gripButton, out grabValue);
            leftController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryValue);
            leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryValue);
            leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out stickAxisValue);
            leftController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue);

            lGrab = grabValue;
            lPrimary = primaryValue;
            lSecondary = secondaryValue;
            lStickAxis = stickAxisValue;
            lTrigger = triggerValue;
        }
    }
}

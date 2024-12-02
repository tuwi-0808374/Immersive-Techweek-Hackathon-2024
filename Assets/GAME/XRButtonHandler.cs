using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonHandler : MonoBehaviour
{
    // Define the XR controllers
    public XRController leftController;
    public XRController rightController;

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    void Start()
    {
        // Get the input devices from the controllers
        if (leftController != null)
            leftDevice = leftController.inputDevice;

        if (rightController != null)
            rightDevice = rightController.inputDevice;
    }

    void Update()
    {
        // Check if the input devices are valid
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            return;
        }

        // Check if the "A" button (primaryButton on right controller) is pressed
        bool isAButtonPressed = false;
        if (rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isAButtonPressed) && isAButtonPressed)
        {
            Debug.Log("A button pressed on right controller.");
        }

        // Check if the "B" button (secondaryButton on left controller) is pressed
        bool isBButtonPressed = false;
        if (leftDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out isBButtonPressed) && isBButtonPressed)
        {
            Debug.Log("B button pressed on left controller.");
        }

        // You can also check other button states like trigger, grip, etc.
        bool isTriggerPressed = false;
        if (rightDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerPressed) && isTriggerPressed)
        {
            Debug.Log("Trigger button pressed on right controller.");
        }
    }
}

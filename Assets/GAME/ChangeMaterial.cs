using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    // Public variable to assign the new material in the Inspector
    public Material newMaterial;

    // Function to change the material of the GameObject
    public void ChangeObjectMaterial()
    {
        // Get the Renderer component of the GameObject
        Renderer renderer = GetComponentInChildren<Renderer>();

        // Check if the Renderer and newMaterial are not null
        if (renderer != null && newMaterial != null)
        {
            // Change the material of the GameObject
            renderer.material = newMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer or newMaterial is not assigned.");
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Book : MonoBehaviour
{
    public List<Texture2D> pages = new List<Texture2D>();

    // The Renderer component (MeshRenderer, etc.) with the material we want to change
    public Renderer targetRenderer;

    // The current page index in the pages list
    private int currentPageIndex = 0;

    private bool isHolding = false;

    public bool IsHolding { get => isHolding; set => isHolding = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Check if targetRenderer is assigned, otherwise, try to get it automatically
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        // Optionally, initialize the first page
        if (pages.Count > 0 && targetRenderer != null)
        {
            // Set the first page texture to the material
            SetPageTexture(pages[currentPageIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHolding && OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("A button pressed");
            ChangePage();
        }
    }


    private void ChangePage()
    {
        // Ensure we have pages in the list
        if (pages.Count == 0)
            return;

        // Increment the page index and loop back to 0 if we exceed the list length
        currentPageIndex = (currentPageIndex + 1) % pages.Count;

        // Set the new page texture on the material
        SetPageTexture(pages[currentPageIndex]);
    }

    private void SetPageTexture(Texture newTexture)
    {
        // Ensure the material is available
        if (targetRenderer != null && newTexture != null)
        {
            Material material = targetRenderer.material;

            // Check if the _BaseMap property exists in the material
            if (material.HasProperty("_BaseMap"))
            {
                // Set the texture for the _BaseMap property
                material.SetTexture("_BaseMap", newTexture);
            }
            else
            {
                Debug.LogWarning("The material does not have a _BaseMap property.");
            }
        }
    }
}

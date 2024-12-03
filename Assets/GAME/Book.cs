using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Book : MonoBehaviour
{
    public bool bookIsSeed = false;
    public InputActionAsset inputActions;
    private InputAction primaryButtonAction;
    private InputAction secondaryButtonAction;
    Vector3 startPos;
    Quaternion startRot;
    public GameObject bookOpen;
    public GameObject bookClose;
    public GameObject seedModel;
    public UnityEvent pressB;
    public UnityEvent pressA;

    public List<Texture2D> pages = new List<Texture2D>();

    // The Renderer component (MeshRenderer, etc.) with the material we want to change
    public Renderer targetRenderer;

    // The current page index in the pages list
    private int currentPageIndex = 0;

    private bool isHolding = false;

    public bool IsHolding { get => isHolding; set => isHolding = value; }

    bool wait = false;

    //bool seedOut = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;


        primaryButtonAction = inputActions.FindAction("GAME/primary");
        primaryButtonAction.Enable();
        secondaryButtonAction = inputActions.FindAction("GAME/secondary");
        secondaryButtonAction.Enable();

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

    public void ResetPosRot()
    {
        transform.position = startPos;
        transform.rotation = startRot;  
    }

    // Update is called once per frame
    void Update()
    {
        if (seedModel != null && seedModel.activeInHierarchy)
        {
            return;
        }


        if (isHolding && primaryButtonAction.IsPressed())
        {
            if (!wait)
            {
                wait = true;
                //ChangePage();
                pressA.Invoke();
                Invoke("Wait", .5f);
                Debug.Log("a");
            }
        }

        if (isHolding && secondaryButtonAction.IsPressed())
        {
            if (!wait)
            {
                Debug.Log("b");
                wait = true;
                Invoke("Wait", .5f);

                if (bookIsSeed)
                {
                    if (seedModel != null && !seedModel.activeInHierarchy)
                    {
                        seedModel.SetActive(true);
                        bookOpen.SetActive(false);
                        bookClose.SetActive(false);
                    }
                    else
                    {

                    }

                    return;
                }

                pressB.Invoke();
  
                
            }
        }
    }

    public void Wait()
    {
        wait = false;
    }

    public void ChangePage()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
            ResetPosRot();
            bookOpen.SetActive(false);
            bookClose.SetActive(true);
            Debug.Log("book hit floor");
            seedModel.SetActive(false);
        }
    }

    public void HoldBook()
    {
        if (seedModel != null && seedModel.activeInHierarchy)
        {
            return;
        }
        IsHolding = true;
        bookOpen.SetActive(true);
        bookClose.SetActive(false);
    }
}

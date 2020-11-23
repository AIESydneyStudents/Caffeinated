/*----------------------------------
    File Name: WindowQuestPointer.cs
    Purpose: Enable image to flash
    Author: Logan Ryan
    Modified: 23 November 2020
------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowQuestPointer : MonoBehaviour
{
    public Camera cam;
    public GameObject arrow;
    public GameObject targetObject;
    public Vector3 offset;
    public Vector3 offscreenOffset;
    public float xPosForArrowToSwitch;
    public float yPosForArrowToSwitch;
    public Color colourForFindingTea;
    public Color colourForFindingCustomer;

    private RectTransform pointerRectTransform;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        targetObject = GameObject.FindGameObjectWithTag("Collectable");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // If there is no target object, deactivate arrow
        if (targetObject == null)
        {
            arrow.SetActive(false);
        }
        else
        {
            // Otherwise, calculate the target position
            arrow.SetActive(true);
            CalculateTargetPosition();

            // Change colour of arrow depending on the target
            if (targetObject.CompareTag("Collectable"))
            {
                arrow.GetComponent<Image>().color = colourForFindingTea;
            }
            else if (targetObject.CompareTag("Customer"))
            {
                arrow.GetComponent<Image>().color = colourForFindingCustomer;
            }
        }
    }

    /// <summary>
    /// Calculate the target position
    /// </summary>
    void CalculateTargetPosition()
    {
        float borderSize = 50f;

        // Get the target screen point position
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetObject.transform.position);

        // Check if the target is offscreen
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        // If the target is offscreen
        if (isOffscreen)
        {
            RotatePointerTowardsTargetPosition();

            // Clamp target screen position
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            // Convert target position to world position
            Vector3 pointerWorldPosition = cam.ScreenToWorldPoint(cappedTargetScreenPosition);

            // Fix offset for the x position of the arrow
            if (pointerRectTransform.position.x > xPosForArrowToSwitch)
            {
                // If the x position of the offscreen offset is a positive number then switch it to a negative number
                if (offscreenOffset.x > 0)
                {
                    offscreenOffset.x = -offscreenOffset.x;
                }
            }
            else if (pointerRectTransform.position.x < -xPosForArrowToSwitch)
            {
                // If the x position of the offscreen offset is a negative number then switch it to a positive number
                if (offscreenOffset.x < 0)
                {
                    offscreenOffset.x = -offscreenOffset.x;
                }
            }

            // Fix offset for the y position of the arrow
            if (pointerRectTransform.position.y > yPosForArrowToSwitch)
            {
                // If the y position of the offscreen offset is a positive number then switch it to a negative number
                if (offscreenOffset.y > 0)
                {
                    offscreenOffset.y = -offscreenOffset.y;
                }
            }
            else if (pointerRectTransform.position.y < -yPosForArrowToSwitch)
            {
                // If the y position of the offscreen offset is a negative number then switch it to a positive number
                if (offscreenOffset.y < 0)
                {
                    offscreenOffset.y = -offscreenOffset.y;
                }
            }

            // Change position of the arrow
            pointerRectTransform.position = pointerWorldPosition + offscreenOffset;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            // If the target is onscreen, then set arrow to targets position
            Vector3 pointerWorldPosition = cam.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition + offset;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            pointerRectTransform.localEulerAngles = new Vector3(0, 0, -90);
        }
    }

    /// <summary>
    /// Calculate angle from vector
    /// </summary>
    /// <param name="dir"></param>
    /// <returns>angle of vector</returns>
    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    /// <summary>
    /// Rotate arrow to point towards target position
    /// </summary>
    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = targetObject.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// Hide arrow
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Show arrow
    /// </summary>
    /// <param name="targetObject">The target object</param>
    public void Show(GameObject targetObject)
    {
        gameObject.SetActive(true);
        this.targetObject = targetObject;
    }
}

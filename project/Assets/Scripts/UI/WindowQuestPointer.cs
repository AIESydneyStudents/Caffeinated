using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowQuestPointer : MonoBehaviour
{
    public Camera cam;

    public GameObject arrow;
    public GameObject targetObject;
    public Vector3 offset;
    public Vector3 offscreenOffset;
    public float xPosForArrowToSwitch;
    public float yPosForArrowToSwitch;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        targetObject = GameObject.FindGameObjectWithTag("Collectable");
    }

    void Update()
    {
        if (targetObject == null)
        {
            arrow.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            CalculateTargetPosition();
        }
    }

    void CalculateTargetPosition()
    {
        float borderSize = 50f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffscreen)
        {
            RotatePointerTowardsTargetPosition();

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPosition = cam.ScreenToWorldPoint(cappedTargetScreenPosition);

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

            pointerRectTransform.position = pointerWorldPosition + offscreenOffset;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPosition = cam.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition + offset;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            pointerRectTransform.localEulerAngles = new Vector3(0, 0, -90);
        }
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = targetObject.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(GameObject targetObject)
    {
        gameObject.SetActive(true);
        this.targetObject = targetObject;
    }
}

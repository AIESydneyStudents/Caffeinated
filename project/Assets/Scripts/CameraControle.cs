using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControle : MonoBehaviour
{
    public GameObject Target;
    //public GameObject VantagePoint;
    public float VantageSize = 10;
    public Vector3 Offset;

    public int interpolationFrames = 45; // Number of frames to completely interpolate between the 2 positions
    private int elapsedFrames = 0;

    private Vector3 lastTargetPos;
    private Vector3 TargetPos;
    private Camera cam;
    private float defaultSize;
    private float activeSize;
    //private PlayerActions Controles;
    //private bool toogle = true;
    //private void Awake()
    //{
    //    Controles = new PlayerActions();
    //}

    //private void OnEnable()
    //{
    //    Controles.Default.Birdseye_View.performed += Birdseye_View_performed;
    //    Controles.Default.Birdseye_View.canceled += Birdseye_View_canceled;
    //    Controles.Default.Birdseye_View.Enable();
    //    //Controles.Enable();
    //}

    //private void Birdseye_View_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    toogle = true;
    //}

    //private void Birdseye_View_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    toogle = false;
    //}
    //public void m_Birdeye_Toggle()
    //{
    //    toogle = !toogle;
    //}


    //private void OnDisable()
    //{
    //    Controles.Default.Birdseye_View.performed -= Birdseye_View_performed;
    //    Controles.Default.Birdseye_View.canceled -= Birdseye_View_canceled;
    //    Controles.Default.Birdseye_View.Disable();
    //    //Controles.Disable();
    //}
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        TargetPos = transform.position;
        lastTargetPos = TargetPos;
        defaultSize = cam.orthographicSize;
        activeSize = defaultSize;
    }

    // Update is called once per frame
    void Update()
    {
        //float m_Input = Controles.Default.Birdseye_View.ReadValue<float>();
        //if (!toogle)
        //{
        //    TargetPos = VantagePoint.transform.position;
        //    activeSize = VantageSize;
        //}
        //else
        //{
            TargetPos = Target.transform.position + Offset;
        //    activeSize = defaultSize;
        //}

        if (lastTargetPos != TargetPos)
        {
            elapsedFrames = 0;
        }
        float interpolationRatio = (float)elapsedFrames / interpolationFrames;
        transform.position = Vector3.Lerp(transform.position, TargetPos, interpolationRatio);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, activeSize, interpolationRatio);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFrames + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
        lastTargetPos = TargetPos;
    }
}

using System;
using System.Collections;
using UnityEngine;

//08.03.23

public class FZASimple : MonoBehaviour
{
    [Header("Modes")]
    public bool rotate;
    public bool zoomInOut;
    public bool swing;
    public bool shake;

    [Header("Values")]
    public bool fromStart;
    public float stepTime = 0.01f;

    [Header("Set Rotation")]
    public Vector3 stepAngle;

    [Header("Set Zoom In-Out")]
    public float a;

    [Header("Set Swing")]
    public Vector3 maxAngle;

    [Header("Shake")]
    public Vector3 maxShake;


    private bool running = false;
    private bool oldRotate, oldZoomInOut, oldSwing;

    void Start()
    {
        if (fromStart)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (!running)
        {
            running = true;
            StartCoroutines();
        }
    }

    public void Deactivate()
    {
        running = false;
        shaking = false;
    }

    public void StartCoroutines()
    {
        if (rotate)
            StartCoroutine(Rotate());
        if (zoomInOut)
            StartCoroutine(ZoomInOut());
        if (swing)
            StartCoroutine(Swing());
        if (shake)
            Shake();
    }

    private IEnumerator Rotate()
    {
        while (running)
        {
            yield return new WaitForSeconds(stepTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + stepAngle.x, transform.eulerAngles.y + stepAngle.y, transform.eulerAngles.z + stepAngle.z);
        }

    }

    private IEnumerator ZoomInOut()
    {
        while (running)
        {
            yield return new WaitForSeconds(stepTime);
            if (Math.Abs(transform.eulerAngles.x) <= Math.Abs(maxAngle.y) && Math.Abs(transform.eulerAngles.y) <= Math.Abs(maxAngle.x)
                && Math.Abs(transform.eulerAngles.z) <= Math.Abs(maxAngle.z))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + maxAngle.x, transform.eulerAngles.y + maxAngle.y, transform.eulerAngles.z + maxAngle.z);
            }
            else
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - maxAngle.x, transform.eulerAngles.y - maxAngle.y, transform.eulerAngles.z - maxAngle.z);
        }
    }

    private IEnumerator Swing()
    {
        while (running)
        {
            yield return new WaitForSeconds(stepTime);
            if (Math.Abs(transform.eulerAngles.x) <= Math.Abs(maxAngle.y) && Math.Abs(transform.eulerAngles.y) <= Math.Abs(maxAngle.x)
                && Math.Abs(transform.eulerAngles.z) <= Math.Abs(maxAngle.z))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + maxAngle.x, transform.eulerAngles.y + maxAngle.y, transform.eulerAngles.z + maxAngle.z);
            }
            else
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - maxAngle.x, transform.eulerAngles.y - maxAngle.y, transform.eulerAngles.z - maxAngle.z);
        }
    }

    private void Shake()
    {
        oldPos = transform.position;
        shaking = true;

    }

    public bool shaking = false;
    Vector3 targetPos;
    bool shakeOff = true;
    Vector3 oldPos;

    private void Update()
    {
        if (shaking)
        {
            if (shakeOff)
            {

                if (transform.GetComponent<RectTransform>().anchoredPosition.y < maxShake.y)
                    transform.Translate(Vector3.up * 10f * Time.deltaTime);
                else
                    shakeOff = false;
            }
            else
            {
                if (transform.GetComponent<RectTransform>().anchoredPosition.y > 0)
                    transform.Translate(Vector3.down * 10f * Time.deltaTime);
                else
                    shakeOff = true;
            }
        }
    }
}

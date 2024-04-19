using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    public static WebcamController instance;

    public RawImage display;

    public WebCamTexture camTexture;
    WebCamDevice device;

    int camWidth;
    int camHeight;

    private void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;

        StartWebCam();
    }

    public void StartWebCam()
    {
        device = WebCamTexture.devices[0];

        camWidth = (int)display.rectTransform.sizeDelta.x;
        camHeight = (int)display.rectTransform.sizeDelta.y;
        camTexture = new WebCamTexture(device.name, camWidth, camHeight);

        display.texture = camTexture;
        camTexture.Play();
    }

    public void QuitWebCam()
    {
        camTexture.Stop();
    }
}

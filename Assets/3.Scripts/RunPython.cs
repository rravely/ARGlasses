using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPython : MonoBehaviour
{
    public void RunPythonProcess()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();

        string path = "C:/Tas815k/SSPOSE/";
        process.StartInfo.FileName = path + "ssposeBat_20230905_assay3_s_01_b32e20.bat";
        process.StartInfo.WorkingDirectory = path;

        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.CreateNoWindow = true;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.EnableRaisingEvents = true;

        process.Start();

        string result = process.StandardOutput.ReadToEnd();
        string[] results = result.Split('\n');
        Debug.Log(result);

        process.WaitForExit();
        process.Close();

        SharedMemory.instance.isStart = true;
    }
}

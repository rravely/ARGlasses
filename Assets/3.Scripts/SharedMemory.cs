using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.MemoryMappedFiles;
using System.IO;

public class SharedMemory : MonoBehaviour
{
    public static SharedMemory instance;

    public bool isStart = false;

    uint SHARED_MEMORY_SIZE = 22118400; //2764800  
    uint SHARED_MEMORY_SIZE_pose = 1024; //1024

    string TAG_NAME = "Local\\Test";
    string TAG_NAME_pose = "Local\\Pose";

    MemoryMappedFile mmf;
    MemoryMappedFile mmf_pose;
    MemoryMappedViewStream stream;
    MemoryMappedViewStream stream_pose;

    StreamWriter streamWriter;
    StreamReader streamReader_pose;

    private void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;

        mmf = MemoryMappedFile.CreateOrOpen(TAG_NAME, SHARED_MEMORY_SIZE);
        mmf_pose = MemoryMappedFile.CreateOrOpen(TAG_NAME_pose, SHARED_MEMORY_SIZE_pose);
        stream = mmf.CreateViewStream(0, SHARED_MEMORY_SIZE, MemoryMappedFileAccess.Write);
        stream_pose = mmf_pose.CreateViewStream(0, SHARED_MEMORY_SIZE_pose, MemoryMappedFileAccess.Read);
    }

    private void Update()
    {
        if (isStart)
        {
            WriteJPEG();
            ReadPose();
        }
    }


    private char[] reads = new char[1024];
    void ReadPose()
    {
        stream = mmf.CreateViewStream(0, SHARED_MEMORY_SIZE, MemoryMappedFileAccess.Write);
        stream_pose = mmf_pose.CreateViewStream(0, SHARED_MEMORY_SIZE_pose, MemoryMappedFileAccess.Read);

        streamReader_pose = new StreamReader(stream_pose);
        streamReader_pose.Read(reads, 0, (int)SHARED_MEMORY_SIZE_pose);

        Debug.Log(reads[0]);

        UpdateRT(string.Concat(reads));
    }

    void WriteJPEG()
    {
        Texture2D texture = new Texture2D(1280, 720);
        texture.SetPixels(WebcamController.instance.camTexture.GetPixels());
        texture.Apply();

        byte[] bytes = texture.EncodeToJPG(100);

        streamWriter = new StreamWriter(stream);
        streamWriter.BaseStream.Write(bytes, 0, bytes.Length);

        streamWriter.Flush(); 
    }

    void UpdateRT(string s)
    {
        string[] tok = s.Split(' ');
        Debug.Log(tok[0]);
    }
}

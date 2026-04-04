using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Result
{
    public int code;
    public string message;
    public string data;


    public override string ToString()
    {
        return $"Result{{code={code}, message='{message}', data={data}}}";
    }
}
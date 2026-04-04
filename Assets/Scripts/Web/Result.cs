using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Result<T>
{

    public int Code => code;
    public string Message => msg;
    public T Data => data;

    [JsonProperty("code")]
    private int code;
    [JsonProperty("msg")]
    private string msg;
    [JsonProperty("data")]
    private T data;


    public override string ToString()
    {
        return $"Result{{code={code}, message='{msg}', data={data}}}";
    }
}
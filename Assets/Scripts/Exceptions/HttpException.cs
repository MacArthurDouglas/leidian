using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpException : Exception
{
    public int StatusCode{get;set;}
    public string Url{get;set;}
    public HttpException(int statusCode,string message,string url)
        :base(message)
    {
        StatusCode = statusCode;
        Url = url;
    }

}
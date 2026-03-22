using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Serialization;

public class YamlUtils
{

    public static T BindByPrefix<T>(string yaml, string prefix)
    {
        var deserializer = new DeserializerBuilder().Build();
        var data=deserializer.Deserialize<Dictionary<object, object>>(yaml);
        object current = data;
        foreach (var part in prefix.Split('.'))
        {
            current = ((Dictionary<object, object>)current)[part];
        }
        var serializer = new SerializerBuilder().Build();
        var subYaml = serializer.Serialize(current);
        return deserializer.Deserialize<T>(subYaml);
    }
    
    
    
}
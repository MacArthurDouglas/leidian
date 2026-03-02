using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ApplicationConfig
{
    public static ApplicationConfig Instance { get; set; }
    static ApplicationConfig()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("application");
        var yaml = textAsset.text;
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        ApplicationConfig applicationConfig = deserializer.Deserialize<ApplicationConfig>(yaml);
        Instance= applicationConfig;
        /*var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        Debug.Log(serializer.Serialize(applicationConfig));*/
    }
    public Startech startech { get; set; }
    
}
public class Startech
{
    public Backend backend { get; set; }
    
}
public class Backend
{
    public string url { get; set; }
}
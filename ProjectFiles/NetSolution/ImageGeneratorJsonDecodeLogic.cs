#region Using directives
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion

public class ImageGeneratorJsonDecodeLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void GetGeneratedImageUrl()
    {
        var jsonVariable = Owner.Get<IUAVariable>("JSON");
        if (jsonVariable == null)
            throw new Exception("Expected JSON Variable");

        var imageURLVariable = Owner.Get<IUAVariable>("ImageURL");
        if (jsonVariable == null)
            throw new Exception("Expected Message Variable");

        string json = jsonVariable.Value;
        JsonNode root = JsonValue.Parse(json);
        imageURLVariable.Value = new UAValue(root?.AsObject()?["data"]?.AsArray()?.ElementAt(0)?.AsObject()?["url"]?.AsValue()?.GetValue<string>());
    }
}

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

public class JsonDecodeLogic : BaseNetLogic
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
    public void GetResponseMessage()
    {
        var jsonVariable = Owner.Get<IUAVariable>("JSON");
        if (jsonVariable == null)
            throw new Exception("Expected JSON Variable");

        var messageVariable = Owner.Get<IUAVariable>("Message");
        if (jsonVariable == null)
            throw new Exception("Expected Message Variable");

        string json = jsonVariable.Value;
        JsonNode root = JsonValue.Parse(json);
        messageVariable.Value = new UAValue(root.AsObject()?["choices"]?.AsArray()?.ElementAt(0)?.AsObject()?["message"]?.AsObject()?["content"].AsValue()?.GetValue<string>());
    }
}

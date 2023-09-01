#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

public class ChatCompletitionPOST : BaseNetLogic
{
    public override void Start()
    {
    }

    public override void Stop()
    {
    }

    private NetLogicObject GetRestApiClient()
    {
        var restApiClient = LogicObject.Owner?.Owner?.Get<IUAObject>("RESTApiClient");
        if (restApiClient == null)
            throw new Exception($"Missing RESTApiClient object { LogicObject.BrowseName }");

        return (NetLogicObject)restApiClient;
    }

    private string GetApiUrl()
    {
        var urlVariable = LogicObject.Get<IUAVariable>("APIUrl");
        if (urlVariable == null)
            throw new Exception($"Missing APIUrl variable under the NetLogic { LogicObject.BrowseName }");

        return urlVariable.Value;
    }

    private string GetContentType()
    {
        var userAgentVariable = LogicObject.Get<IUAVariable>("ContentType");
        if (userAgentVariable == null)
            throw new Exception($"Missing ContentType variable under the NetLogic { LogicObject.BrowseName }");

        return userAgentVariable.Value;
    }

    [ExportMethod]
    public void PerformRequest(string requestBody, string bearerToken, out string response, out int code)
    {
        object[] inputParamas = { GetApiUrl(), requestBody, bearerToken, GetContentType() };
        object[] outputParams = { };

        var restApiCLient = GetRestApiClient();
        restApiCLient.ExecuteMethod("Post", inputParamas, out outputParams);

        if (outputParams.Length != 2)
            throw new Exception("Unexpected outputParams length");

        (response, code) = ((string)outputParams[0], (int)outputParams[1]);
    }
}

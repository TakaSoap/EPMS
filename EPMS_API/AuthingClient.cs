using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;

namespace EPMS_API;

public static class AuthingClient
{
    public static readonly ManagementClient Client = new ManagementClient(init: opt =>
    {
        opt.UserPoolId = "61c5f5f420f2561c3f51e6de";
        opt.Secret = "";
        opt.Host = "https://epms-szu.authing.cn";
    });
}
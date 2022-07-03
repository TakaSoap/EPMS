import { AuthenticationClient, ManagementClient } from "authing-js-sdk";

export const authenticationClient = new AuthenticationClient({
  appId: "62a35e7d54fab18ac73c5932",
  appHost: "https://epms-szu.authing.cn"
});


//   redirectUri: "http://localhost:4000/callback"
//   tokenEndPointAuthMethod: "none",
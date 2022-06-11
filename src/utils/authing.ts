import { AuthenticationClient } from "authing-js-sdk";

export const authenticationClient = new AuthenticationClient({
  appId: "61c5f5f435508b05390c04d3",
  //appId: "626eaee585a5257cf21440f7",
  appHost: "https://epms-takasoap.authing.cn"
});

//   redirectUri: "http://localhost:4000/callback"
//   tokenEndPointAuthMethod: "none",
# Secure Your .NET 6 Web API

This repository sohws you how to impliment the client credentials flow on an ASP.NET Core Web API using Okta.  Please read [Secure Your .NET 6 Web API][blog] to see how it was created.

**Prerequisites:**

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

- [Okta](https://developer.okta.com/) has Authentication and User Management APIs that reduce development time with instant-on, scalable user infrastructure. Okta's intuitive API and expert support make it easy for developers to authenticate, manage and secure users and roles in any application.


* [Getting Started](#getting-started)
* [Links](#links)
* [Help](#help)
* [License](#license)

## Getting Started

### Obtain your issuer from your Okta authorization server

Navigate to the Security > Api page in your Okta admin portal and obtain the `Issuer URI` from the `default` Authorization Server.

### Create a custom scope for your authorization server 

Click on the `default` authorization server.

Navigate to the `Scopes` tab.

Click **Add Scope**.  Name it `api` and give it a phrase and description.

### Obtain your client id and client secret from your Okta authorization server

Navigate Applications > Applications.

Click **Create App Integration**.

Select *API Services*

Name your application `DotNet6ClientFlow`.

Click **Save**.

Note your Client ID and Client secret.

## Run the Application

To run this example, run the following commands:

```bash
git clone https://github.com/nickolasfisher/Okta_DependencyInjection_DotNet.git
cd Okta_DotNetSixClientFlow
```

Create a file called `appsettings.Development.json` and add the following code to it.

```JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Okta": {
    "Issuer": "{yourIssuer}"
  }
}
```

Start debugging your application.

## Test your Application

Open your API test tool of choice and set up your request to the Okta authorization server.

{yourBase64EncodedCredentials} is of the format `{yourClientId}:{yourClientSecret}` Base64 encoded

 - {yourIssuer}/v1/token POST
 - headers
    - Accept: `application/json`
    - Authorization: `Basic {yourBase64EncodedCredentials}`
    - Content-Type: `application/x-www-form-urlencoded`
 - body
    - grant_type: `client_credentials`
    - scope: `api`

The response will have a field for `access_token`.

Create a new request in your API test tool and use the following setup.

- {yourWebAPIDomain}/weatherforecast GET
- headers
    - Authorization: `Bearer {yourAccessToken}`

## Links

This example uses the following open source libraries from Okta:

* [Okta .NET SDK](https://github.com/okta/okta-sdk-dotnet)
* [Okta CLI](https://github.com/okta/okta-cli)

## Help

Please post any questions as comments on the [blog post][blog], or visit our [Okta Developer Forums](https://devforum.okta.com/).

## License

Apache 2.0, see [LICENSE](LICENSE).

[blog]: ()

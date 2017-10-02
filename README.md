# dvc-ivr-signalr
A SignalR hub that can be used to test DVC IVR applications.

This is a .NET (Windows) Console application that will host a SignalR hub called ivrHub. It can be used as a central host to test Integrated Voice Response (IVR) for Avaya Call-Server-based systems.

It was created to help test IVR applications for Disney Vacation Club (DVC).

_Note: This code uses [SignalR 2.x](https://www.asp.net/signalr), and not SignalR for ASP.NET Core._

## Quick Start

1. Clone [this repository](https://github.disney.com/WDPR-SALES-UI/dvc-ivr-signalr).
1. Open the `ivr-test.sln` solution file in Visual Studio.
1.  Press `F5` to build and execute the application.
2.  In your consuming project's `index.html` file, include the script file `http://{server}:8623/signalr/hubs`, where `{server}` is the name or IP address of the Windows system running this application. _Note: 
3.  Connect to the SignalR server and call the `subscribe` server method. See the sample code below.

## Prerequisites

* Visual Studio 2015 or later.
* .NET Framework 4.6.
* Windows 7 or later.
* jQuery 1.9 or later.
* Understanding of the [SignalR 2 API](https://www.asp.net/signalr).

## Sample Code
### Connect to Hub

```javascript
$.connection.hub.url = 'http://10.211.55.3:8623/signalr/';

return $.connection.hub.start()
    .then(function () {
        console.log('Now connected, connection ID=' + $.connection.hub.id);
    })
    .fail(function () { console.log('Could not Connect!'); })
    .done(function () {
        console.log('Done trying to connect.')
    });
```

### Subscribe to Group

```javascript
return ivrHubProxy.server.subscribe(groupName)
    .then(function () {
        console.log('Subscribed.');
    })
    .fail(function (err) { console.log('Could not Subscribe!', err); });
```

### Unsubscribe from Group

```javascript
return ivrHubProxy.server.unsubscribe(ash.id)
    .then(function () { console.log('Unubscribed.') })
    .fail(function (err) { console.log('Could not Unsubscribe!', err); });
```


## Questions/Comments
If you have questions or comments, please open a GitHub issue on this project and tag @callm031.


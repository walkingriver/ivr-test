# ivr-signalr
A SignalR hub that can be used to test IVR applications.

This is a .NET (Windows) Console application that will host a SignalR hub called ivrHub. It can be used as a central host to test Integrated Voice Response (IVR) for Avaya Call-Server-based systems.
d
_Note: This code uses [SignalR 2.x](https://www.asp.net/signalr), and not SignalR for ASP.NET Core._

## Quick Start

1. Clone this repository.
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
return ivrHubProxy.server.subscribe("MyGroup")
    .then(function () {
        console.log('Subscribed.');
    })
    .fail(function (err) { console.log('Could not Subscribe!', err); });
```

### Unsubscribe from Group

```javascript
return ivrHubProxy.server.unsubscribe("MyGroup")
    .then(function () { console.log('Unubscribed.') })
    .fail(function (err) { console.log('Could not Unsubscribe!', err); });
```


## Send Message to Members of Group

To simulate an IVR call message from outside of the hub to members of a given group (MyGroup above), you send an HTTP POST request to the server, as shown below.

* **URL**

  /call

* **Method:**
  
  `POST`
  
  
*  **URL Params**

	_None_

* **Data Params**

  ```json
  {
    "Advisor": "CALLM031",          -- Advisor's HUB ID
    "Phone": "1123121123",          -- Guest Phone
    "SSN": "3865",                  -- Last 4 of Guest SSN
    "MemberId": "617560765315",     -- Guest Membership ID
    "ReservationID": "617560765315" -- Guest Reservation Number
  }
  ```
  
  _Note: Do not send both MemberId and ReservationId in a single request._

* **Success Response:**
  
  * **Code:** 200 <br />
    **Content:** _None_
 
* **Error Response:**

  * **Code:** 400 BAD REQUEST <br />
    **Content:** `{ error : "Must include the advisor's Hub ID in the call details." }`
  
## SSL Cert

If your website that will host the signalr client is served by SSL, you'll need a few more steps.

This app requires a valid SSL certificate. Follow the 
[instructions here](https://weblog.west-wind.com/posts/2013/sep/23/hosting-signalr-under-sslhttps)
to get one set up.

Once you have the certificate set up and the application runs, it may not be trusted by client browsers.
For testing purposes, the way to get that working will be to visit the server's URL, as in this example:

* https://{server-url}:8623/signalr/hubs

This will display a warning that the server's identity cannot be trusted. You'll need to accept that
warning and continue to the site anyway. Once you have done that, leave that browser or tab open. Now
you can open a new tab and hit the website that hosts your signalr client, without an SSL error.

Obviously, if your site uses an already-trusted SSL certificate, and you can bind it to port
8623, these workaround hacks will not be necessary.

## Questions/Comments
If you have questions or comments, please open a GitHub issue on this project.


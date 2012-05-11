WebTokens

C# sample code for working with Simple Web Tokens to access REST based web services.

Two10.Sample1 - Retrieves a token from ACS, parses it and checks its validity.
Two10.SampleRest - An MVC3 web app that exposes a WRAP endpoint and a simple REST endpoint protected by a Simple Web Token
Two10.Swt - Models a Simple Web Token (the core of this thing really)
Two10.Swt.Tests - a few unit tests, including demo of calling the above REST endpoint etc.

Neil Alderson
Rob Blackwell
Richard Astbury
Anton Staykov

May 2012

TODO

Check for matching realm/scope - DONE
Support DateTime object for specifying expiry - DONE
Surface the claims as part of the Authorize attribute or Iclaimprinciple. - DONE
Can we move RESTAUthorize to Two10.Swt ? - DONE
What about building a TokenIssuer class to wrap up the stuff in the WRAP endpoint?
Could we use Uri.EscapeUriString insted of HTTPUtility to allow .NET 4 CLient framework instead of full framework? (Beware capitalization).
What about refresh tokens?
What would it take to add  OAUTH2.0 compatibility?

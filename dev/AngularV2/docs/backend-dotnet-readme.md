# Backend .NET Guidance for External Redirect SSO

Recommendations:
- Use a **one-time token (OTK)** pattern:
  - Short TTL (e.g., 60 seconds)
  - Single-use (invalidate after first validation)
  - Store server-side in a fast store (Redis) or signed JWT that you can validate
- The external application should perform a server-to-server call to your `/external/validate-otk` endpoint
  to obtain user identity and create its own session (do not trust OTK in the browser).
- If you control both apps, consider performing a backend-initiated redirect (server-side 302) instead of exposing
  the OTK to the client.
- Protect endpoints with standard security (HTTPS, CORS allowlist, rate-limiting).

C# snippets included:
- docs/backend-dotnet-controller.cs
- docs/backend-dotnet-otkservice.cs

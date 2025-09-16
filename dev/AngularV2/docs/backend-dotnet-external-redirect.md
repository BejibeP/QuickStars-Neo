# .NET Backend: Suggested endpoints for external redirect SSO

POST /api/external/create-redirect
- Authenticates user (cookie or bearer)
- Creates a one-time token (OTK) with short TTL (e.g. 60s) and single-use
- Stores OTK server-side and associates with userId + target app
- Returns { url: "https://external.app/sso?otk=..." } OR responds with 302 to the external app

POST /external/validate-otk
- Called by the external app (server-to-server) to validate OTK and obtain user info
- Marks OTK as used

Security considerations:
- OTK should be random, signed, tied to origin and IP optionally
- Use HTTPS, set CORS policy tight, and set SameSite cookie appropriately

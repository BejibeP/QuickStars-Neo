# Angular 20 Skeleton (Auth + External Redirect + Storybook)

This repository is a **minimal skeleton** to bootstrap an Angular 20 app with:
- Core auth services (cookie / JWT hybrid pattern)
- External redirect flow using backend-generated one-time tokens
- Basic routing + lazy feature example
- Storybook integration (minimal config + a sample component)

> This is a scaffold: run `npm install` and adapt versions/providers to match your stack.

Quick start:
1. `npm install`
2. `npm run start` (ng serve) — requires Angular CLI installed globally
3. `npm run storybook` to run Storybook.

Files of interest:
- `src/app/core/` : auth.service.ts, auth.guard.ts, token.interceptor.ts, external-redirect.service.ts
- `src/app/core/external-redirect.component.ts` : route that triggers secure redirect
- `libs/api` : typed API client examples
- `storybook` : basic Storybook config

Security note:
- Prefer backend-generated one-time tokens for external redirects (see external-redirect.service.ts).
- Do not put long-lived JWTs in URLs.


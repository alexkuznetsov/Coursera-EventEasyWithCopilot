# EventEasy

EventEasy is a Blazor WebAssembly application for browsing events, editing event details, registering attendees, and
tracking participation.

## What Is Implemented

- Event list with reusable `EventCard` component
- Event details page with edit and save flow
- Event registration form with validation and disabled submit for invalid input
- Attendance tracking per event (participants and reserved tickets)
- User session tracking (session id, last route, registration counters)
- Session persistence in `localStorage`
- CRUD operations for events:
    - Create (`/events/new`)
    - Read (`/events`, `/events/{id}`)
    - Update (event details save)
    - Delete (event details delete action)
- Routing with graceful handling of unknown routes (`Not Found` page)
- Basic render optimization in event list using `@key`

## Project Structure

- `Pages/` - UI pages (`Home`, `EventDetails`, `EventRegistration`, `Attendance`, `EventCreate`)
- `Components/` - reusable components (`EventCard`)
- `Data/` - in-memory repository (`EventRepository`)
- `Models/` - data models and validation attributes
- `Services/` - app state services (`UserSessionTracker`, `AttendanceTracker`)
- `EventEasy.Tests/` - unit and component tests (xUnit + bUnit)

## Run Locally

```bash
dotnet build
dotnet test EventEasy.slnx
```

To run the app from IDE, open the project and start the Blazor WebAssembly target.

## How Codex Assisted

Codex was used as a development copilot to:

- scaffold and refine Blazor components and pages
- implement routing and validation logic
- introduce state management services and persistence strategy
- add missing CRUD operations and connect them to UI flows
- create and update automated tests to prevent regressions
- verify changes by running build/test commands after each iteration

This enabled faster implementation cycles while keeping the codebase consistent and test-verified.

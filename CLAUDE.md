# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Basketball League API - A .NET Core 3.1 Web API for managing basketball league statistics, teams, players, matches, and real-time match incidents. Uses Clean Architecture with CQRS pattern.

**Client Application:** React TypeScript + MobX frontend at https://github.com/pieetrus/basketball-league-front

## Build and Run Commands

### Build
```bash
dotnet build BasketballLeague.sln
```

### Run API
```bash
dotnet run --project SRC/API/API.csproj
```

The API will be available at:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000
- Swagger UI: https://localhost:5001/swagger

### Database Migrations
```bash
# Create new migration (from repository root)
dotnet ef migrations add <MigrationName> --project SRC/Persistence/Persistence.csproj --startup-project SRC/API/API.csproj

# Apply migrations (automatic on startup, or manually)
dotnet ef database update --project SRC/Persistence/Persistence.csproj --startup-project SRC/API/API.csproj

# Remove last migration
dotnet ef migrations remove --project SRC/Persistence/Persistence.csproj --startup-project SRC/API/API.csproj
```

### Database Configuration
- **Connection String:** Set in `SRC/API/appsettings.json` under `ConnectionStrings:BasketballLeagueDatabase`
- **Default:** SQL Server local instance (`.`) with database `BASKETBALL_LEAGUE_SYSTEM`
- **Migrations:** Auto-applied on application startup in Program.cs
- **Seeding:** Database seeded automatically on startup via Seed.cs

## Architecture Overview

### Clean Architecture Layers

The project follows **Clean Architecture** with dependency inversion - inner layers define interfaces that outer layers implement.

```
┌─────────────────────────────────────────────┐
│  API (Presentation)                         │
│  - Controllers, SignalR Hubs, Middleware   │
│  Dependencies: Infrastructure, Persistence  │
└──────────────────┬──────────────────────────┘
                   │
        ┌──────────┴──────────┐
        ▼                     ▼
┌─────────────────┐   ┌─────────────────┐
│ Infrastructure  │   │  Persistence    │
│ - JWT, Photos   │   │  - EF Core      │
│ - External APIs │   │  - Identity     │
└────────┬────────┘   └────────┬────────┘
         │                     │
         └──────────┬──────────┘
                    ▼
         ┌─────────────────────┐
         │  Application        │
         │  - CQRS/MediatR     │
         │  - DTOs, Validators │
         │  - AutoMapper       │
         └──────────┬──────────┘
                    ▼
         ┌─────────────────────┐
         │  Domain (Core)      │
         │  - Entities         │
         │  - No dependencies  │
         └─────────────────────┘
```

**Key Principle:** Dependencies point inward. Domain has zero dependencies. Application defines interfaces (e.g., `IBasketballLeagueDbContext`, `IJwtGenerator`) that outer layers implement.

### CQRS Pattern with MediatR

All business logic uses Command Query Responsibility Segregation via MediatR:

**Commands** (write operations):
- Located in: `Application/<Feature>/Commands/<CommandName>/`
- Example: `Application/Players/Commands/CreatePlayer/CreatePlayerCommand.cs`
- Pattern: Command class implements `IRequest<TResponse>` with nested `Handler` class
- Returns: Entity ID, DTO, or `Unit` (void equivalent)

**Queries** (read operations):
- Located in: `Application/<Feature>/Queries/<QueryName>/`
- Example: `Application/Players/Queries/GetPlayersList/`
- Files: `<Query>.cs`, `<Query>Handler.cs`, `<Dto>.cs`
- Returns: DTOs mapped via AutoMapper

**Controllers** dispatch to MediatR:
```csharp
public class PlayerController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePlayerCommand command)
    {
        return await Mediator.Send(command);
    }
}
```

All controllers inherit from `BaseController` which provides the `Mediator` property via service locator pattern.

### Layer Responsibilities

**Domain Layer (`SRC/Domain`)**
- Pure domain entities with no framework dependencies
- Entity classes are simple POCOs (Player, Team, Match, Shot, Foul, etc.)
- Abstract base classes from EF Core are treated as part of .NET Core platform

**Application Layer (`SRC/Application`)**
- Defines `IBasketballLeagueDbContext` interface (implemented by Persistence)
- Organized by feature folders (Players, Teams, Matches, Shots, etc.)
- Each feature contains Commands, Queries, DTOs, Validators
- Validators use FluentValidation co-located with commands
- AutoMapper profiles in `Common/Mappings/MappingProfile.cs`
- Dependencies registered in `DependencyInjection.cs` (MediatR, AutoMapper)

**Infrastructure Layer (`SRC/Infrastructure`)**
- Implements `IJwtGenerator` for JWT token creation (7-day expiration, HS512)
- Implements `IUserAccessor` to get current user from HttpContext
- Implements `IPhotoAccessor` for Cloudinary photo storage
- JWT configuration with special handling for SignalR auth via query string
- Dependencies registered in `DependencyInjection.cs`

**Persistence Layer (`SRC/Persistence`)**
- `BasketballLeagueDbContext` implements `IBasketballLeagueDbContext`
- Inherits from `IdentityDbContext<AppUser>` for authentication
- Entity configurations in `Configurations/` folder using `IEntityTypeConfiguration<T>`
- Keeps domain entities clean (no attributes, fluent API only)
- SQL Server provider configured in `DependencyInjection.cs`
- Custom password policy: 6+ chars, requires digit, lowercase, uppercase

**API Layer (`SRC/API`)**
- Controllers inherit from `BaseController` (provides MediatR access)
- Route pattern: `api/[controller]` (e.g., `/api/player`)
- Global authorization policy: all endpoints require authentication by default
- Use `[AllowAnonymous]` for public endpoints (login, register, division list)
- SignalR hub at `/incidentsHub` for real-time match updates
- Custom exception handling middleware maps exceptions to HTTP codes

## Adding New Features

To add a new entity and CRUD operations (e.g., "Coach"):

### 1. Domain Layer
Create entity: `SRC/Domain/Entities/Coach.cs`

### 2. Persistence Layer
Add to context interfaces and implementation:
```csharp
// IBasketballLeagueDbContext.cs
DbSet<Coach> Coach { get; set; }

// BasketballLeagueDbContext.cs
public DbSet<Coach> Coach { get; set; }
```

Create configuration: `SRC/Persistence/Configurations/CoachConfiguration.cs`
```csharp
public class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.Name).HasColumnName("NAME");
        // ... additional configuration
    }
}
```

Create migration:
```bash
dotnet ef migrations add AddCoach --project SRC/Persistence/Persistence.csproj --startup-project SRC/API/API.csproj
```

### 3. Application Layer
Create feature folder structure:
```
SRC/Application/Coaches/
├── Commands/
│   ├── CreateCoach/
│   │   ├── CreateCoachCommand.cs          # Command + Handler
│   │   └── CreateCoachCommandValidator.cs
│   ├── UpdateCoach/
│   │   ├── UpdateCoachCommand.cs
│   │   └── UpdateCoachCommandValidator.cs
│   └── DeleteCoach/
│       └── DeleteCoachCommand.cs
└── Queries/
    ├── GetCoachesList/
    │   ├── GetCoachesListQuery.cs
    │   ├── GetCoachesListQueryHandler.cs
    │   └── CoachListDto.cs
    └── GetCoachDetail/
        ├── GetCoachDetailQuery.cs
        ├── GetCoachDetailQueryHandler.cs
        └── CoachDetailDto.cs
```

Command example:
```csharp
public class CreateCoachCommand : IRequest<int>
{
    public string Name { get; set; }

    public class Handler : IRequestHandler<CreateCoachCommand, int>
    {
        private readonly IBasketballLeagueDbContext _context;

        public Handler(IBasketballLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCoachCommand request, CancellationToken ct)
        {
            var entity = new Coach
            {
                Name = request.Name
            };

            _context.Coach.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity.Id;
        }
    }
}
```

Add AutoMapper mappings in `SRC/Application/Common/Mappings/MappingProfile.cs`:
```csharp
CreateMap<Coach, CoachListDto>();
CreateMap<Coach, CoachDetailDto>();
```

### 4. API Layer
Create controller: `SRC/API/Controllers/CoachController.cs`
```csharp
public class CoachController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoachListDto>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetCoachesListQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoachDetailDto>> Get(int id)
    {
        return await Mediator.Send(new GetCoachDetailQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCoachCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> Update(int id, UpdateCoachCommand command)
    {
        command.Id = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Delete(int id)
    {
        return await Mediator.Send(new DeleteCoachCommand { Id = id });
    }
}
```

## Authentication & Security

**JWT Configuration:**
- Token key configured in `appsettings.json` under `TokenKey`
- Tokens expire after 7 days
- Username stored in `NameId` claim
- SignalR authentication via query string parameter `access_token`

**Global Authorization:**
- All endpoints require authentication by default (configured in Startup.cs)
- Public endpoints must use `[AllowAnonymous]` attribute
- Current user accessible via `IUserAccessor.GetCurrentUsername()`

**Password Requirements:**
- Minimum 6 characters
- Requires digit, lowercase, and uppercase letter
- Configured in `Persistence/DependencyInjection.cs`

## Exception Handling

Custom exceptions in `Application/Common/Exceptions/`:
- `NotFoundException` → HTTP 404
- `BadRequestException` → HTTP 400
- `UnauthorizedException` → HTTP 401

Throw in handlers:
```csharp
var entity = await _context.Coach.FindAsync(id);
if (entity == null)
    throw new NotFoundException(nameof(Coach), id);
```

Global middleware (`API/Common/ErrorHandlingMiddleware.cs`) catches and converts to appropriate HTTP responses.

## Complex Business Logic Example

For complex operations involving multiple aggregates, see `Application/Shots/Commands/CreateShot/CreateShotCommand.cs` which:
- Creates shot incident with associated data (assist, rebound)
- Updates player match statistics
- Updates team match statistics
- Handles various basketball scoring scenarios (2pt, 3pt, assists, rebounds)

This demonstrates the pattern for complex domain operations within a single command handler.

## Real-Time Updates with SignalR

The `IncidenctsHub` at `/incidentsHub` handles real-time match updates:
- Hub methods accept MediatR commands and broadcast results
- Frontend connects via SignalR client with JWT in query string
- Used for live match tracking and statistics updates

## Database Seeding

Seed data applied automatically on startup in `Program.cs` via `Persistence/Seed.cs`. Modify `Seed.cs` to add initial data for development/testing.

## Configuration Files

**appsettings.json:**
- `ConnectionStrings:BasketballLeagueDatabase` - SQL Server connection
- `TokenKey` - JWT signing key (use User Secrets for production)
- `SwaggerOptions` - Swagger endpoint configuration

**CORS Policy:**
- Configured to allow `http://localhost:3000` (React frontend)
- Allows credentials, any header, any method
- Modify in `Startup.cs` for production

## Technology Stack

- .NET Core 3.1 Web API
- Entity Framework Core 3.1 + SQL Server
- ASP.NET Core Identity (authentication)
- MediatR 8.0 (CQRS)
- AutoMapper 8.0 (object mapping)
- FluentValidation 8.6 (validation)
- JWT Bearer authentication
- SignalR (real-time communication)
- Swagger/OpenAPI (API documentation)
- Cloudinary (photo storage)
- Newtonsoft.Json (JSON serialization)

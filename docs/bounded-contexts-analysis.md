# Bounded Contexts Analysis for Basketball League

## Overview

This document analyzes different approaches to dividing the Basketball League system into bounded contexts and domains. It presents multiple options with their trade-offs to help choose the best architectural approach.

---

## Current System Analysis

### Entities (23 total)

**Core Master Data:**
- Player, Team, Division, Season, AppUser, Photo

**Aggregating Entities (Season Context):**
- SeasonDivision, TeamSeason, PlayerSeason

**Match Aggregate:**
- Match, TeamMatch, PlayerMatch

**Match Incidents (Real-time events):**
- Incident (base), Shot, Foul, FreeThrows, Assist, Rebound, Block, Steal, Turnover, Substitution, Timeout

### Key Relationships

```
Season (1) ----< (N) SeasonDivision >---- (1) Division
                         |
                         |---< (N) TeamSeason >---- (1) Team
                         |---< (N) PlayerSeason >---- (1) Player
                         +---< (N) Match

Match
├── FK: SeasonDivisionId → SeasonDivision
├── FK: TeamSeasonHomeId → TeamSeason
├── FK: TeamSeasonGuestId → TeamSeason
├── Collection: Incidents (1:N)
└── Collection: PlayerMatches (1:N)

Incident (polymorphic)
├── 1:1 → Shot (optional)
├── 1:1 → Foul (optional)
├── 1:1 → Substitution (optional)
└── etc.
```

### Transaction Boundaries

**Complex Transaction: CreateShot**
- Entities involved: Match, TeamMatch, PlayerMatch, PlayerSeason, Incident, Shot, Assist, Rebound
- Updates statistics atomically
- Creates PlayerMatch on-demand

**Complex Transaction: EndMatch**
- Rolls up statistics from PlayerMatch → PlayerSeason
- Rolls up statistics from TeamMatch → TeamSeason
- Determines winner, updates RankingPoints

---

## Option 1: Conservative Modular Monolith

### Bounded Contexts

```
┌─────────────────────────────────────────┐
│  LeagueManagement (Single Module)       │
│  - All 23 entities                      │
│  - All business logic                   │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Identity (Separate Module)             │
│  - AppUser, Authentication              │
└─────────────────────────────────────────┘
```

### Database Strategy
- Shared database
- Separate schemas: `league.*`, `identity.*`

### Communication
- In-process domain events via MediatR
- Shared database = ACID transactions

### Pros ✅
- Minimal risk - small code changes
- Preserves transactional consistency
- Easy migration from current state
- Quick to implement (1-2 weeks)

### Cons ❌
- No real bounded contexts
- Large single module
- Limited scalability
- Doesn't teach distributed patterns

### When to Choose
- Want quick start
- First time with modular monolith
- Minimize risk
- Not planning microservices soon

---

## Option 2: Domain-Driven Modular Monolith (RECOMMENDED)

### Bounded Contexts

```
┌─────────────────────────────────────────┐
│  MatchEngine (Core Domain)              │
│  - Match, TeamMatch, PlayerMatch        │
│  - Incident (all types)                 │
│  - Real-time operations                 │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  CompetitionManagement (Core Domain)     │
│  - Season, Division, SeasonDivision     │
│  - TeamSeason, PlayerSeason             │
│  - Standings, Statistics                │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Catalog (Supporting Domain)            │
│  - Team, Player, Photo                  │
│  - Master data                          │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Identity (Generic Domain)              │
│  - AppUser, Authentication              │
└─────────────────────────────────────────┘
```

### Module Structure

```
SRC/
  Modules/
    MatchEngine/
      Domain/
        Match.cs, Incident.cs, Shot.cs, Foul.cs
      Application/
        Commands/
          StartMatch, RecordShot, RecordFoul
        Queries/
          GetMatchStats, GetLiveMatch
      Infrastructure/
        Persistence/
        SignalR/
      API/
        MatchController

    CompetitionManagement/
      Domain/
        Season.cs, Division.cs, TeamSeason.cs
      Application/
        Commands/
          RegisterTeam, RegisterPlayer
        Queries/
          GetStandings, GetSeasonStats
      Infrastructure/
      API/

    Catalog/
      Domain/
        Team.cs, Player.cs
      Application/
      Infrastructure/
      API/

  SharedKernel/
    DomainEvents/
      MatchEndedEvent
      PlayerRegisteredEvent
    Common/
    Abstractions/
```

### Communication Between Modules

#### Domain Events (In-Process)

```csharp
// MatchEngine publishes event
public class MatchEndedEvent : IIntegrationEvent
{
    public int MatchId { get; set; }
    public int WinnerTeamSeasonId { get; set; }
    public int LoserTeamSeasonId { get; set; }
    public Dictionary<int, PlayerMatchStats> PlayerStats { get; set; }
}

// CompetitionManagement consumes
public class MatchEndedEventHandler : INotificationHandler<MatchEndedEvent>
{
    public async Task Handle(MatchEndedEvent @event, CancellationToken ct)
    {
        // Update TeamSeason stats, RankingPoints, Wins/Lost
        // Update PlayerSeason cumulative stats
    }
}
```

#### Anti-Corruption Layer

```csharp
// MatchEngine doesn't know PlayerSeason - only ID
// CompetitionManagement provides adapter
public interface IPlayerSeasonProvider // in SharedKernel
{
    Task<PlayerSeasonInfo> GetPlayerSeasonAsync(int id);
}

// Implementation in CompetitionManagement
public class PlayerSeasonProvider : IPlayerSeasonProvider
{
    // ...
}
```

### Database Strategy

**Shared DB with logical separation:**

```sql
-- MatchEngine schema
match_engine.matches
match_engine.team_matches
match_engine.player_matches
match_engine.incidents
match_engine.shots

-- CompetitionManagement schema
competition.seasons
competition.divisions
competition.team_seasons
competition.player_seasons

-- Catalog schema
catalog.teams
catalog.players
```

**Rule:** Each module only writes to its schema, can read from others (read-only views).

### Pros ✅
- Real bounded contexts - clear separation
- Each module has its own subdomain
- Domain events - learn event-driven architecture
- Eventual consistency between modules
- Good balance between complexity and learning
- Easier refactoring to microservices later
- Independent testing per module
- Anti-Corruption Layer protects modules

### Cons ❌
- Eventual consistency between modules
- More complex than Option 1
- Need to design integration events
- Shared database = still coupling on data level
- Risk of circular dependencies if poorly designed

### When to Choose
- **Want to learn DDD and modular monolith** (RECOMMENDED!)
- Planning microservices eventually
- Want to showcase bounded contexts on portfolio
- Accept eventual consistency
- 2-3 months for refactoring

---

## Option 3: Event-Sourced Core + Traditional Supporting

### Bounded Contexts

```
┌─────────────────────────────────────────┐
│  MatchEngine (Event Sourced)            │
│  Event Store: match-{id}                │
│  Aggregate: Match                       │
│  Projections: LiveMatchView, MatchStats │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Statistics (CQRS Read Side)            │
│  Consumes: MatchEnded                   │
│  Updates: PlayerSeasonStats, TeamStats  │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Competition (Traditional CRUD)          │
│  Season, Division, TeamSeason, etc.     │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│  Catalog (Traditional CRUD)             │
│  Team, Player, Photo                    │
└─────────────────────────────────────────┘
```

### Event Stream

```
Stream: match-123
├── MatchScheduled
├── MatchStarted
├── ShotRecorded { PlayerId, ShotType, IsAccurate, Value }
├── AssistRecorded
├── ReboundRecorded
├── FoulCommitted
├── SubstitutionMade
└── MatchEnded
```

### Projections

```csharp
public class MatchStatsProjection : IHandleEvent<ShotRecorded>
{
    public async Task HandleAsync(ShotRecorded @event)
    {
        // Update PostgreSQL materialized view
        await _db.ExecuteAsync(@"
            UPDATE player_match_stats
            SET pts = pts + @points, fga = fga + 1, ...
            WHERE match_id = @matchId AND player_id = @playerId",
            new { points = @event.Value, matchId = @event.MatchId, playerId = @event.PlayerId }
        );
    }
}

public class SeasonStatsAggregator : IHandleEvent<MatchEnded>
{
    public async Task HandleAsync(MatchEnded @event)
    {
        // Read from PlayerMatchStats projection
        var playerStats = await GetPlayerStatsFromMatch(@event.MatchId);

        // Aggregate to PlayerSeason
        foreach (var stats in playerStats)
        {
            await UpdatePlayerSeasonStats(stats);
        }
    }
}
```

### Pros ✅
- Full audit trail for matches
- Temporal queries ("score at Q3?")
- Event replay capability
- Real-time projections (Redis for sub-second latency)
- Separation of concerns (event store vs read models)
- Statistics as projections eliminates sync problems

### Cons ❌
- Large streams (200-300 events per match)
- Projection lag (100-500ms eventual consistency)
- One module event-sourced, rest traditional (inconsistent architecture)
- Learning curve for Event Sourcing

### When to Choose
- Want Event Sourcing experience
- Match domain is well isolated
- Accept eventual consistency
- 2-3 months implementation

---

## Option 4: Multi-Stream Event Sourcing (Advanced)

### Bounded Contexts

```
┌────────────────────────────────────────┐
│  Match Orchestration                   │
│  Stream: match-{id}                    │
│  Events: MatchStarted, MatchEnded      │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Game Events                           │
│  Streams:                              │
│    - match-{id}-shots                  │
│    - match-{id}-fouls                  │
│    - match-{id}-substitutions          │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Player Performance                    │
│  Stream: player-{id}-season-{seasonId} │
│  Events: PerformanceRecorded           │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Team Performance                      │
│  Stream: team-{id}-season-{seasonId}   │
│  Events: MatchResultRecorded           │
└────────────────────────────────────────┘
```

### Process Manager (Saga)

```csharp
public class MatchCompletionSaga :
    IHandleEvent<MatchEnded>,
    IHandleEvent<PerformanceRecorded>
{
    public async Task HandleAsync(MatchEnded @event)
    {
        var saga = new MatchCompletionSagaState
        {
            MatchId = @event.MatchId,
            PlayersToProcess = await GetPlayersInMatch(@event.MatchId)
        };

        // Trigger performance recording for all players
        foreach (var playerId in saga.PlayersToProcess)
        {
            await _commandBus.SendAsync(new RecordPlayerPerformance
            {
                PlayerId = playerId,
                MatchId = @event.MatchId
            });
        }
    }
}
```

### Pros ✅
- Separation of concerns - different streams for different event types
- Scalability - each stream processed independently
- Granular replay - replay shots, fouls separately
- Multiple aggregates with their own invariants
- Process Manager (Saga) - advanced pattern
- Full domain event sourced
- Microservices-ready

### Cons ❌
- High complexity - many streams to manage
- Eventual consistency between streams
- Saga complexity
- Harder debugging across streams
- No ACID between streams
- Most code to write

### When to Choose
- Want to maximize Event Sourcing showcase
- Planning microservices
- Interested in advanced patterns (Saga, Process Manager)
- 3-4 months implementation

---

## Option 5: Domain-Specific Streams (Basketball-Optimized)

### Bounded Contexts

```
┌────────────────────────────────────────┐
│  Game Flow                             │
│  Stream: game-flow-{matchId}           │
│  Events: PossessionStarted, etc.       │
│  Aggregate: GameFlow                   │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Scoring                               │
│  Stream: scoring-{matchId}             │
│  Events: FieldGoalAttempted, Made      │
│  Aggregate: ScoringSequence            │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Player Actions                        │
│  Stream: player-actions-{matchId}      │
│  Events: AssistProvided, Rebound       │
│  Aggregate: PlayerContributions        │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Violations & Fouls                    │
│  Stream: violations-{matchId}          │
│  Events: PersonalFoulCalled, FoulOut   │
│  Aggregate: FoulManagement             │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│  Lineup Management                     │
│  Stream: lineup-{matchId}              │
│  Events: PlayerSubstituted             │
│  Aggregate: ActiveLineup               │
└────────────────────────────────────────┘
```

### Basketball-Specific Aggregates

```csharp
public class ScoringSequence : AggregateRoot
{
    private int _shotClockSeconds = 24;

    public void AttemptFieldGoal(int playerId, ShotType shotType)
    {
        // Invariant: shot clock must not be expired
        var elapsed = (DateTime.UtcNow - _possessionStarted).TotalSeconds;
        if (elapsed > _shotClockSeconds)
            throw new ShotClockViolationException();

        var @event = new FieldGoalAttempted { /* ... */ };
        ApplyEvent(@event);
    }
}

public class FoulManagement : AggregateRoot
{
    public void CommitPersonalFoul(int playerWhoFouled, FoulType foulType)
    {
        // Invariant: player cannot have more than 6 fouls
        var currentFouls = _playerFouls.GetValueOrDefault(playerWhoFouled, 0);
        if (currentFouls >= 6)
            throw new PlayerAlreadyFouledOutException();

        // Check team fouls for bonus situation
        var teamFouls = _teamFoulsPerQuarter[_currentQuarter];

        var @event = new PersonalFoulCalled
        {
            FoulNumber = currentFouls + 1,
            IsInBonusSituation = teamFouls >= 4
        };

        ApplyEvent(@event);
    }
}
```

### Pros ✅
- Domain-optimized - streams map to basketball concepts
- Rich invariants - enforces basketball rules
- Advanced analytics - lineup effectiveness, +/- stats
- Granular replay
- Microservices-ready
- Portfolio showcase - shows deep domain understanding

### Cons ❌
- Highest complexity
- Coordination overhead between streams
- Basketball domain knowledge required
- Event correlation complexity
- Most code

### When to Choose
- Want best domain understanding showcase
- Care about advanced analytics
- Competing for senior/lead positions
- Basketball enthusiast
- 4-5 months implementation

---

## Decision Matrix

| Criterion | Option 1 | Option 2 | Option 3 | Option 4 | Option 5 |
|-----------|----------|----------|----------|----------|----------|
| **Time** | 1-2 wk | 2-3 mo | 2-3 mo | 3-4 mo | 4-5 mo |
| **Complexity** | ⭐ | ⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Portfolio Value** | ⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Learning** | Low | Medium | Medium-High | High | High |
| **Production Ready** | ✅ | ✅ | ✅ | ⚠️ | ⚠️ |
| **DDD** | ❌ | ✅ | ⚠️ | ✅ | ✅ |
| **Event Sourcing** | ❌ | ❌ | ✅ | ✅ | ✅ |
| **Microservices Ready** | ❌ | ⚠️ | ⚠️ | ✅ | ✅ |

---

## Recommended Approach: Phased Evolution

### Phase 1: Start with Option 2 (2-3 months)
**Domain-Driven Modular Monolith**
- Learn bounded contexts
- Domain events
- Module boundaries
- Good portfolio piece

### Phase 2: Evolve to Option 5 (2-3 months)
**Domain-Specific Streams (ES-D)**
- Extract domain-specific streams
- Basketball-optimized aggregates
- Advanced analytics
- **Best portfolio showcase**

### Phase 3: Add Enterprise Patterns (ongoing)
**Apply Archetype Patterns**
- Party archetype for player lifecycle
- Quantity archetype for statistics
- Rule archetype for basketball rules
- **Portfolio gold** 🏀🔥

### Total Timeline: 4-6 months

This gives you:
- ✅ Learning path (incremental complexity)
- ✅ Multiple blog post opportunities
- ✅ Impressive portfolio pieces
- ✅ Real-world architectural evolution

---

## Next Steps

1. **Choose your starting point** (recommend Option 2)
2. **Plan the migration** from current state
3. **Start implementation** incrementally
4. **Document as you go** (blog posts, ADRs)
5. **Evolve to ES-D** when ready for advanced patterns

See related documents:
- `event-sourcing-variants.md` - Detailed ES approaches
- `archetype-patterns.md` - Enterprise patterns
- `es-d-plus-archetypes.md` - Ultimate combination

# Event Sourcing Variants for Basketball League

## Overview

This document describes four different approaches to implementing Event Sourcing in the Basketball League system, ranging from conservative to aggressive strategies. Each variant has different complexity levels, learning curves, and portfolio impact.

---

## Variant ES-A: Single Event Stream per Match (Minimalist)

**Complexity Score:** ⭐⭐⭐☆☆ (3/5)
**Portfolio Impact:** ⭐⭐⭐⭐☆ (4/5)
**Implementation Time:** 1-2 months

### Architecture

```
┌─────────────────────────────────────────────────┐
│  Match Management (Event Sourced)               │
│  Event Store: match-{id}                        │
│  Events:                                        │
│    - MatchScheduled, MatchStarted               │
│    - ShotRecorded, FoulCommitted                │
│    - SubstitutionMade, TimeoutCalled            │
│    - TurnoverCommitted, MatchEnded              │
│                                                 │
│  Aggregate: Match                               │
│  Projections: LiveMatchView, MatchStatsView     │
└─────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────┐
│  Statistics Aggregation (CQRS Read Side)       │
│  Consumes: MatchEnded event                     │
│  Updates: PlayerSeasonStats, TeamSeasonStats    │
└─────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────┐
│  Competition Management (Traditional CRUD)       │
│  Entities: Season, Division, SeasonDivision     │
│           TeamSeason, PlayerSeason              │
└─────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────┐
│  Catalog (Traditional CRUD)                     │
│  Entities: Team, Player, Photo                  │
└─────────────────────────────────────────────────┘
```

### Event Stream Structure

```
Stream: match-123
├── MatchScheduled (v1)
├── MatchStarted (v2)
├── ShotRecorded (v3)
│   └── { PlayerId, ShotType, IsAccurate, Value, Quarter, Time }
├── AssistRecorded (v4)
├── ReboundRecorded (v5)
├── FoulCommitted (v6)
├── FreeThrowsAttempted (v7)
├── SubstitutionMade (v8)
├── TimeoutCalled (v9)
├── TurnoverCommitted (v10)
├── StealRecorded (v11)
└── MatchEnded (v12)
```

### Projections

#### LiveMatchProjection (Real-time)
- Updates Redis for sub-second latency
- Maintains live score
- Tracks current game state

#### PlayerMatchStatsProjection
- Aggregates player statistics from events
- Updates denormalized read model in PostgreSQL
- Handles concurrent event processing

#### SeasonStatsAggregator
- Consumes MatchEnded event
- Rolls up statistics to PlayerSeason and TeamSeason
- Updates standings and rankings

### Pros ✅
- **Single stream per match** - simple structure
- **Full audit trail** - every action recorded
- **Natural event boundaries** - events map 1:1 to basketball actions
- **Easy replay** - reconstruct match from any point
- **Real-time projections** - Redis for LiveMatchView
- **Separation of concerns** - event store vs read models

### Cons ❌
- **Large streams** - match can have 200-300 events (acceptable)
- **Projection lag** - 100-500ms eventual consistency
- **No separation of incident types** - all in one stream
- **Single module event-sourced** - rest is traditional

### When to Choose
- Want **quick start** with Event Sourcing
- Value simplicity
- Match domain is sufficiently isolated
- 1-2 months implementation time

---

## Variant ES-B: Multi-Stream Event Sourcing (Recommended)

**Complexity Score:** ⭐⭐⭐⭐⭐ (5/5)
**Portfolio Impact:** ⭐⭐⭐⭐⭐ (5/5)
**Implementation Time:** 3-4 months

### Architecture

```
┌────────────────────────────────────────────────┐
│  Match Orchestration (Event Sourced)           │
│  Stream: match-{id}                            │
│  Events: MatchScheduled, MatchStarted,         │
│          QuarterStarted, MatchEnded            │
│  Aggregate: Match (lifecycle only)             │
└────────────────────────────────────────────────┘

┌────────────────────────────────────────────────┐
│  Game Events (Event Sourced)                   │
│  Streams:                                      │
│    - match-{id}-shots                          │
│    - match-{id}-fouls                          │
│    - match-{id}-substitutions                  │
│    - match-{id}-turnovers                      │
│                                                │
│  Aggregates per stream:                        │
│    - ShotAggregate                             │
│    - FoulAggregate                             │
│    - SubstitutionAggregate                     │
└────────────────────────────────────────────────┘

┌────────────────────────────────────────────────┐
│  Player Performance (Event Sourced)            │
│  Stream: player-{playerId}-season-{seasonId}   │
│  Events: PlayerRegistered,                     │
│          PerformanceRecorded                   │
│  Aggregate: PlayerSeasonPerformance            │
└────────────────────────────────────────────────┘

┌────────────────────────────────────────────────┐
│  Team Performance (Event Sourced)              │
│  Stream: team-{teamId}-season-{seasonId}       │
│  Events: TeamRegistered, MatchResultRecorded   │
│  Aggregate: TeamSeasonPerformance              │
└────────────────────────────────────────────────┘

┌────────────────────────────────────────────────┐
│  Statistics & Analytics (Projections Only)     │
│  Consumes events from ALL streams              │
│  Databases: PostgreSQL, Elasticsearch, Redis   │
└────────────────────────────────────────────────┘
```

### Stream Examples

#### Stream: match-{id} (Orchestration)
```
match-123
├── MatchScheduled
├── MatchStarted
├── QuarterStarted (Q1)
├── QuarterEnded (Q1)
├── QuarterStarted (Q2)
└── MatchEnded
```

#### Stream: match-{id}-shots
```
match-123-shots
├── ShotAttempted { ShotId, PlayerId, ShotType, Value }
├── ShotMade { ShotId, Points }
├── ShotMissed { ShotId }
├── ShotBlocked { ShotId, BlockedByPlayerId }
├── AssistAwarded { ShotId, AssistByPlayerId }
└── ReboundSecured { ShotId, PlayerId, ReboundType }
```

#### Stream: player-{playerId}-season-{seasonId}
```
player-456-season-2024
├── PlayerRegisteredForSeason
├── MatchPerformanceRecorded { MatchId, Stats }
├── SeasonStatisticsUpdated { CumulativeStats }
└── AwardReceived { AwardType }
```

### Cross-Stream Process Manager (Saga)

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
- **Separation of concerns** - different streams for different event types
- **Scalability** - each stream processed independently
- **Granular replay** - replay only shots, only fouls, etc.
- **Multiple aggregates** - each type has its own aggregate with invariants
- **Process Manager (Saga)** - orchestration between streams (advanced!)
- **Full domain event sourced** - not just Match, but Player/Team performance too
- **Query flexibility** - projections consume from multiple streams
- **Microservices-ready** - easy to extract streams to different services

### Cons ❌
- **Complexity** - many streams to manage
- **Eventual consistency** - between streams
- **Saga complexity** - Process Manager is advanced pattern
- **Debugging** - harder to trace flow between streams
- **Transaction boundaries** - no ACID between streams
- **More code** - each stream needs aggregate, handlers, projections

### When to Choose
- Want to **maximize Event Sourcing showcase** on portfolio
- Planning **microservices** in future
- Interested in **advanced patterns** (Saga, Process Manager)
- Want **granular control** over replay and projections
- 3-4 months implementation

---

## Variant ES-C: Hybrid - Event Sourced Write + Traditional Read (Pragmatic)

**Complexity Score:** ⭐⭐⭐☆☆ (3/5)
**Portfolio Impact:** ⭐⭐⭐☆☆ (3/5)
**Implementation Time:** 2-3 months

### Architecture

```
┌────────────────────────────────────────────┐
│  Match Engine Write Side (Event Sourced)   │
│  Event Store: Marten on PostgreSQL         │
│  Stream: match-{id}                        │
│  Aggregate: Match                          │
└────────────────────────────────────────────┘
          │ Events published
          ▼
┌────────────────────────────────────────────┐
│  Match Engine Read Side (Traditional)      │
│  Database: SQL Server (existing schema!)   │
│  Tables: Incidents, Shots, Fouls           │
│         TeamMatch, PlayerMatch             │
│  Projections write to existing tables      │
└────────────────────────────────────────────┘
```

### Migration Strategy

#### Phase 1: Dual Write
```csharp
public async Task HandleAsync(RecordShotCommand cmd)
{
    // 1. Event Sourcing (new)
    var match = await _eventStore.LoadAggregateAsync<Match>(cmd.MatchId);
    match.RecordShot(cmd.PlayerId, cmd.ShotType, cmd.IsAccurate);
    await _eventStore.SaveAggregateAsync(match);

    // 2. Traditional DB (legacy, for now)
    var incident = new Incident { /* ... */ };
    _legacyDb.Incidents.Add(incident);
    await _legacyDb.SaveChangesAsync();

    // 3. Verify consistency
    await _consistencyChecker.VerifyAsync(cmd.MatchId);
}
```

#### Phase 2: Event Store Primary, Legacy via Projections
```csharp
public class ShotProjection : IHandleEvent<ShotRecorded>
{
    public async Task HandleAsync(ShotRecorded @event)
    {
        // Write to legacy database via projection
        await _legacyDb.ExecuteAsync(@"
            INSERT INTO incidents (match_id, incident_type, ...)
            VALUES (@matchId, 'SHOT', ...)",
            @event
        );
    }
}
```

#### Phase 3: Cut Over - Deprecate Legacy Writes
```csharp
public async Task HandleAsync(RecordShotCommand cmd)
{
    var match = await _eventStore.LoadAggregateAsync<Match>(cmd.MatchId);
    match.RecordShot(cmd.PlayerId, cmd.ShotType, cmd.IsAccurate);
    await _eventStore.SaveAggregateAsync(match);
    // Projection handles legacy DB automatically
}
```

### Pros ✅
- **Gradual migration** - no big bang
- **Zero frontend changes** - projections write to existing tables
- **Safety** - dual write allows consistency verification
- **Rollback possible** - can revert at any time
- **Existing queries work** - nothing broken on read side
- **Best of both worlds** - event sourcing benefits + legacy compatibility

### Cons ❌
- **Technical debt** - maintain both implementations during migration
- **Dual write overhead** - more work in command handlers
- **Consistency verification** - must test both paths
- **Slower adoption** - learn Event Sourcing slower (legacy fallback exists)

### When to Choose
- Want to **minimize risk**
- Have **working frontend** and don't want to change it
- Prefer **incremental approach**
- Limited time (want quick results)
- 2-3 months for full migration

---

## Variant ES-D: Domain-Specific Streams (Basketball-Optimized)

**Complexity Score:** ⭐⭐⭐⭐⭐ (5/5)
**Portfolio Impact:** ⭐⭐⭐⭐⭐ (5/5) + bonus for domain expertise
**Implementation Time:** 4-5 months

### Architecture

```
┌────────────────────────────────────────────┐
│  Game Flow (Event Sourced)                 │
│  Stream: game-flow-{matchId}               │
│  Events: PossessionStarted, QuarterStarted │
│  Aggregate: GameFlow                       │
│  Invariants: possession rules, timing      │
└────────────────────────────────────────────┘

┌────────────────────────────────────────────┐
│  Scoring (Event Sourced)                   │
│  Stream: scoring-{matchId}                 │
│  Events: FieldGoalAttempted, Made/Missed   │
│  Aggregate: ScoringSequence                │
│  Invariants: score calc, shot clock        │
└────────────────────────────────────────────┘

┌────────────────────────────────────────────┐
│  Player Actions (Event Sourced)            │
│  Stream: player-actions-{matchId}          │
│  Events: AssistProvided, ReboundSecured    │
│  Aggregate: PlayerContributions            │
└────────────────────────────────────────────┘

┌────────────────────────────────────────────┐
│  Violations & Fouls (Event Sourced)        │
│  Stream: violations-{matchId}              │
│  Events: PersonalFoulCalled, FoulOut       │
│  Aggregate: FoulManagement                 │
│  Invariants: foul limits, bonus FT         │
└────────────────────────────────────────────┘

┌────────────────────────────────────────────┐
│  Lineup Management (Event Sourced)         │
│  Stream: lineup-{matchId}                  │
│  Events: PlayerSubstituted, LineupChange   │
│  Aggregate: ActiveLineup                   │
│  Use case: +/- stats, lineup effectiveness │
└────────────────────────────────────────────┘
```

### Basketball-Specific Aggregates

#### ScoringSequence Aggregate
```csharp
public class ScoringSequence : AggregateRoot
{
    private int _homeScore = 0;
    private int _awayScore = 0;
    private int _shotClockSeconds = 24;

    public void AttemptFieldGoal(int playerId, ShotType shotType, Position shotPosition)
    {
        // Invariant: shot clock must not be expired
        var elapsed = (DateTime.UtcNow - _possessionStarted).TotalSeconds;
        if (elapsed > _shotClockSeconds)
            throw new ShotClockViolationException();

        var @event = new FieldGoalAttempted { /* ... */ };
        ApplyEvent(@event);
    }
}
```

#### FoulManagement Aggregate
```csharp
public class FoulManagement : AggregateRoot
{
    private Dictionary<int, int> _playerFouls = new();
    private Dictionary<int, int> _teamFoulsPerQuarter = new();

    public void CommitPersonalFoul(int playerWhoFouled, FoulType foulType)
    {
        // Invariant: player cannot have more than 6 fouls
        var currentFouls = _playerFouls.GetValueOrDefault(playerWhoFouled, 0);
        if (currentFouls >= 6)
            throw new PlayerAlreadyFouledOutException();

        var teamFouls = _teamFoulsPerQuarter[_currentQuarter];

        var @event = new PersonalFoulCalled
        {
            FoulNumber = currentFouls + 1,
            IsInBonusSituation = teamFouls >= 4
        };

        ApplyEvent(@event);

        if (currentFouls + 1 >= 6)
        {
            ApplyEvent(new PlayerFouledOut { PlayerId = playerWhoFouled });
        }
    }
}
```

#### ActiveLineup Aggregate
```csharp
public class ActiveLineup : AggregateRoot
{
    private HashSet<int> _homePlayersOnCourt = new();
    private HashSet<int> _awayPlayersOnCourt = new();

    public void SubstitutePlayer(int playerOut, int playerIn, bool isGuest)
    {
        // Invariant: playerOut must be on court
        var activePlayers = isGuest ? _awayPlayersOnCourt : _homePlayersOnCourt;
        if (!activePlayers.Contains(playerOut))
            throw new PlayerNotOnCourtException();

        var @event = new PlayerSubstituted
        {
            PlayerOut = playerOut,
            PlayerIn = playerIn,
            LineupSnapshot = CreateSnapshot()
        };

        ApplyEvent(@event);
    }

    // For +/- statistics
    public LineupEffectiveness CalculateLineupEffectiveness()
    {
        // Analyze scoring differential for lineup combinations
    }
}
```

### Pros ✅
- **Domain-optimized** - streams map to basketball concepts
- **Rich invariants** - enforces basketball rules (fouls, shot clock, lineups)
- **Advanced analytics** - lineup effectiveness, +/- stats, possession analysis
- **Granular replay** - replay scoring, fouls, etc. separately
- **Microservices-ready** - each stream can be separate service
- **Portfolio showcase** - shows deep domain understanding

### Cons ❌
- **High complexity** - most streams and aggregates
- **Coordination overhead** - orchestrate between streams
- **Domain knowledge required** - must understand basketball rules deeply
- **Event correlation** - harder to trace dependencies
- **Most code** - most to write

### When to Choose
- Want to show **best domain understanding**
- Care about **advanced analytics** (lineup effectiveness)
- Planning to **compete** for senior/lead positions
- **Basketball fanatic** - understand and love the domain
- 4-5 months implementation

---

## Decision Matrix

| Criterion | ES-A | ES-B | ES-C | ES-D |
|-----------|------|------|------|------|
| **Time** | 1-2 months | 3-4 months | 2-3 months | 4-5 months |
| **Learning Curve** | Low | Medium | Medium | High |
| **Portfolio Value** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Production Ready** | ✅ High | ✅ High | ✅ High | ⚠️ Medium |
| **Eventual Consistency** | ⚠️ Between modules | ⚠️ Between streams | ⚠️ Read models | ⚠️ Between streams |
| **Audit Trail** | Basic | Basic | Basic | ✅ Full |
| **Scalability** | Limited | ✅ High | Medium | ✅ High |
| **Microservices Ready** | ❌ | ✅ | ⚠️ Refactor needed | ✅ |

---

## Recommended Approach: Phased Implementation

### Phase 1 (2-3 months): Start with ES-A
- Single stream per match
- Learn Event Sourcing basics
- Build confidence

### Phase 2 (2-3 months): Evolve to ES-D
- Extract domain-specific streams
- Implement basketball-specific aggregates
- Add advanced analytics

### Phase 3 (ongoing): Add Enterprise Patterns
- Apply Archetype Patterns (see archetype-patterns.md)
- Implement Rule Engine
- Advanced projections

This gives you a learning path while building impressive portfolio pieces incrementally.

# ES-D + Archetype Patterns: The Ultimate Combination

## Why This Combination is Perfect

**ES-D (Domain-Specific Streams)** provides:
- Basketball-optimized event streams
- Domain-specific aggregates
- Natural modeling of basketball concepts

**Archetype Patterns** add:
- Universal structure
- Reusability across domains
- Enterprise credibility
- Cross-domain abstractions

**Together = 🔥 Portfolio Gold**

This combination shows:
1. ✅ Deep domain understanding (basketball rules)
2. ✅ Enterprise patterns (archetypes)
3. ✅ Modern architecture (event sourcing)
4. ✅ Theoretical knowledge (Arlow's book)

---

## Architecture Overview

```
┌─────────────────────────────────────────────────┐
│  ARCHETYPE LAYER (Universal Patterns)           │
│  Party → Quantity → Rule → Event → Product      │
└─────────────────┬───────────────────────────────┘
                  │ Maps to
                  ▼
┌─────────────────────────────────────────────────┐
│  ES-D LAYER (Basketball-Specific Streams)       │
│  GameFlow → Scoring → PlayerActions →           │
│  Violations → LineupManagement                  │
└─────────────────┬───────────────────────────────┘
                  │ Persisted in
                  ▼
┌─────────────────────────────────────────────────┐
│  EVENT STORE                                    │
│  Marten on PostgreSQL or EventStoreDB           │
└─────────────────┬───────────────────────────────┘
                  │ Projected to
                  ▼
┌─────────────────────────────────────────────────┐
│  READ MODELS (CQRS)                             │
│  PostgreSQL, Elasticsearch, Redis               │
└─────────────────────────────────────────────────┘
```

---

## 1. Party Archetype + ES-D Implementation

### Mapping

```
Party Archetype          →  ES-D Streams
────────────────────────────────────────────
Player (Party)          →  player-lifecycle-{playerId}
PlayerInMatch (Role)    →  lineup-{matchId}
PlayerInSeason (Role)   →  party-role-{roleId}
Team (Organization)     →  team-lifecycle-{teamId}
Official (Party)        →  officials-{matchId}
```

### Stream: player-lifecycle-{playerId}

```csharp
// ═══════════════════════════════════════════════════════
// Archetype: Party (Person)
// ES-D: Player Lifecycle Management
// ═══════════════════════════════════════════════════════

public class PlayerLifecycleEvents
{
    // Party Created
    public class PlayerRegistered : IDomainEvent
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Position PrimaryPosition { get; set; }
        public Position? SecondaryPosition { get; set; }
    }

    // Party Role Assigned
    public class PlayerAssignedToTeamSeason : IDomainEvent
    {
        public int PlayerId { get; set; }
        public int TeamSeasonId { get; set; }
        public int SeasonId { get; set; }
        public string JerseyNumber { get; set; }
        public DateTime EffectiveDate { get; set; }

        // Party Role specific (Contract archetype)
        public ContractTerms ContractTerms { get; set; }
    }

    // Party Role Changed
    public class PlayerTransferred : IDomainEvent
    {
        public int PlayerId { get; set; }
        public int FromTeamSeasonId { get; set; }
        public int ToTeamSeasonId { get; set; }
        public DateTime TransferDate { get; set; }
        public TransferType TransferType { get; set; } // Trade, Free Agency
    }

    // Party Role Ended
    public class PlayerReleasedFromTeam : IDomainEvent
    {
        public int PlayerId { get; set; }
        public int TeamSeasonId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ReleaseReason Reason { get; set; }
    }
}
```

### Stream: lineup-{matchId}

```csharp
// ═══════════════════════════════════════════════════════
// Archetype: Party Role (PlayerInMatch context)
// ES-D: LineupManagement
// ═══════════════════════════════════════════════════════

public class LineupManagementEvents
{
    // Party Role Activated in Context
    public class PlayerEnteredMatch : IDomainEvent
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public bool IsGuest { get; set; }
        public bool IsStarter { get; set; }
        public DateTime Timestamp { get; set; }

        // Party Role attributes
        public string JerseyNumber { get; set; }
        public Position Position { get; set; }
    }

    // Party Role Deactivated
    public class PlayerExitedMatch : IDomainEvent
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public DateTime Timestamp { get; set; }
        public SubstitutionReason Reason { get; set; } // Tactical, FoulOut, Injury
    }

    // Party Role Handoff
    public class PlayerSubstituted : IDomainEvent
    {
        public int MatchId { get; set; }
        public int PlayerOutId { get; set; }  // Role A ended
        public int PlayerInId { get; set; }   // Role B started
        public bool IsGuest { get; set; }
        public int Quarter { get; set; }
        public TimeSpan GameTime { get; set; }

        // Party Role collection (lineup snapshot)
        public LineupSnapshot CurrentLineup { get; set; }
    }
}

public class LineupSnapshot
{
    public List<PlayerInLineup> HomeLineup { get; set; }
    public List<PlayerInLineup> AwayLineup { get; set; }
}

public class PlayerInLineup
{
    public int PlayerId { get; set; }
    public Position Position { get; set; }
    public string JerseyNumber { get; set; }
    public TimeSpan TimeOnCourt { get; set; }
}
```

### Aggregate: ActiveLineup

```csharp
public class ActiveLineup : AggregateRoot
{
    private HashSet<int> _homePlayersOnCourt = new();
    private HashSet<int> _awayPlayersOnCourt = new();
    private List<LineupSnapshot> _lineupHistory = new();

    public void SubstitutePlayer(int playerOut, int playerIn, bool isGuest)
    {
        var activePlayers = isGuest ? _awayPlayersOnCourt : _homePlayersOnCourt;

        // Invariant: playerOut must be on court (Party Role must exist)
        if (!activePlayers.Contains(playerOut))
            throw new PlayerNotOnCourtException(playerOut);

        // Invariant: playerIn must not already be on court
        if (activePlayers.Contains(playerIn))
            throw new PlayerAlreadyOnCourtException(playerIn);

        // Invariant: must have exactly 5 players after substitution
        if (activePlayers.Count != 5)
            throw new InvalidLineupSizeException();

        var @event = new PlayerSubstituted
        {
            PlayerOut = playerOut,
            PlayerIn = playerIn,
            IsGuest = isGuest,
            Timestamp = DateTime.UtcNow,
            CurrentLineup = CreateLineupSnapshot()
        };

        ApplyEvent(@event);
    }

    // Temporal query: lineup at specific time
    public LineupSnapshot GetLineupAtTime(DateTime timestamp)
    {
        return _lineupHistory
            .LastOrDefault(s => s.Timestamp <= timestamp);
    }

    // Advanced Party Role metric: +/- rating
    public Dictionary<int, decimal> CalculatePlusMinusRatings()
    {
        // For each lineup combination, calculate scoring differential
        // Attribute +/- to players who were on court
    }
}
```

### Query Examples

```csharp
// Who played in this match? (Party participation)
var lineup = await _eventStore
    .AggregateStreamAsync<LineupAggregate>("lineup-match-123");
var players = lineup.GetAllPlayersWhoPlayed();

// What was the lineup at start of Q4? (Temporal Party Roles)
var q4Lineup = lineup.GetLineupAtQuarter(4);

// When did LeBron enter? (Party Role activation timestamp)
var lebronEntry = lineup.GetPlayerEntryTime(lebronId);

// What was LeBron's +/- rating? (Party Role metric)
var plusMinus = lineup.GetPlayerPlusMinus(lebronId);
```

---

## 2. Quantity Archetype + ES-D Implementation

### Mapping

```
Quantity Archetype       →  ES-D Streams
────────────────────────────────────────────
Points (Countable)      →  scoring-{matchId}
FG% (Ratio)            →  statistics-derived-{matchId}
TimeOnCourt (Temporal) →  lineup-{matchId}
PossessionDuration     →  game-flow-{matchId}
```

### Stream: scoring-{matchId}

```csharp
// ═══════════════════════════════════════════════════════
// Archetype: Quantity (CountableQuantity)
// ES-D: Scoring
// ═══════════════════════════════════════════════════════

public class ScoringEvents
{
    // Quantity: Points (Countable)
    public class PointsScored : IDomainEvent
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        // Quantity properties (Archetype)
        public int Quantity { get; set; }        // 2 or 3 points
        public string Unit { get; set; }         // "points"
        public QuantityType Type { get; set; }   // "FieldGoal" or "FreeThrow"

        // Context
        public ShotType ShotType { get; set; }
        public bool IsGuest { get; set; }
        public int Quarter { get; set; }
        public TimeSpan GameTime { get; set; }

        // Measurement metadata (Archetype pattern)
        public MeasurementContext MeasurementContext { get; set; }
    }

    // Quantity: Field Goal Attempt (Countable)
    public class FieldGoalAttempted : IDomainEvent
    {
        public Guid AttemptId { get; set; }
        public int PlayerId { get; set; }
        public ShotType ShotType { get; set; }
        public int PotentialValue { get; set; }  // 2 or 3

        // Spatial quantity
        public ShotLocation Location { get; set; }
        public double DistanceFromBasket { get; set; }  // in feet

        // Temporal quantity
        public int ShotClockRemaining { get; set; }
        public TimeSpan GameTimeRemaining { get; set; }
    }

    public class FieldGoalMade : IDomainEvent
    {
        public Guid AttemptId { get; set; }
        public int Points { get; set; }

        // Quantity validation (Archetype)
        public QuantityAssertion Assertion { get; set; }
    }
}

// Supporting classes (Archetype)
public class MeasurementContext
{
    public DateTime Timestamp { get; set; }
    public string MeasuredBy { get; set; }      // "Official", "System"
    public double Confidence { get; set; }       // 0.0 to 1.0
    public string ValidationMethod { get; set; } // "Manual", "ShotTracking"
}

public class ShotLocation
{
    public double X { get; set; }  // Court coordinates
    public double Y { get; set; }
    public ShotZone Zone { get; set; }  // "Paint", "MidRange", "ThreePoint"
}

public enum ShotZone
{
    RestrictedArea,
    Paint,
    MidRange,
    Corner3,
    AboveTheBreak3
}
```

### Stream: statistics-derived-{matchId}

```csharp
// ═══════════════════════════════════════════════════════
// Archetype: Quantity (RatioQuantity - Derived)
// ES-D: Advanced Analytics
// ═══════════════════════════════════════════════════════

public class DerivedStatisticsEvents
{
    // Ratio Quantity calculated from base quantities
    public class FieldGoalPercentageCalculated : IDomainEvent
    {
        public int EntityId { get; set; }  // Player or Team
        public EntityType EntityType { get; set; }

        // Ratio Quantity archetype
        public decimal Numerator { get; set; }    // FGM
        public decimal Denominator { get; set; }  // FGA
        public decimal Ratio { get; set; }        // FGM / FGA
        public string Unit { get; set; }          // "percentage"

        // Calculation metadata
        public CalculationContext Context { get; set; }
        public List<Guid> BaseEventIds { get; set; }  // Events used
    }

    // Complex derived quantity (PER)
    public class PlayerEfficiencyRatingCalculated : IDomainEvent
    {
        public int PlayerId { get; set; }

        // PER = (PTS + REB + AST + STL + BLK - MissedFG - MissedFT - TO) / MIN
        public decimal PER { get; set; }

        // Components (all Quantity archetypes)
        public Dictionary<string, decimal> Components { get; set; }
    }

    // Temporal Quantity (time-based measurement)
    public class PossessionDurationMeasured : IDomainEvent
    {
        public Guid PossessionId { get; set; }

        // Temporal Quantity
        public TimeSpan Duration { get; set; }
        public string Unit { get; set; }  // "seconds"

        // Outcome
        public PossessionOutcome Outcome { get; set; }
        public int? PointsScored { get; set; }
    }
}
```

### Unified Quantity Model

```csharp
// All statistics inherit from Quantity (Archetype)
public abstract class Quantity
{
    public string Unit { get; set; }
    public DateTime MeasuredAt { get; set; }
    public MeasurementContext Context { get; set; }
}

public class CountableQuantity : Quantity
{
    public int Value { get; set; }
}

public class RatioQuantity : Quantity
{
    public decimal Numerator { get; set; }
    public decimal Denominator { get; set; }
    public decimal Value => Denominator != 0 ? Numerator / Denominator : 0;
}

public class TemporalQuantity : Quantity
{
    public TimeSpan Duration { get; set; }
}
```

### Temporal Quantity Queries

```csharp
// Query any quantity at any point in time
var query = new TemporalQuantityQuery
{
    EntityId = playerId,
    QuantityType = "PTS",
    AsOf = new DateTime(2024, 3, 15, 20, 30, 0), // 3Q, 6:30 remaining
    MatchId = matchId
};

var pointsAt3Q = await _quantityRepository.GetAsync(query);
// Returns: 18 points (at that moment in game)

// Query derived quantities
var fgPercentageAt3Q = await _quantityRepository.GetAsync(new TemporalQuantityQuery
{
    EntityId = playerId,
    QuantityType = "FG_PERCENTAGE",
    AsOf = sameTimestamp
});
// Returns: 0.571 (57.1% FG at that moment)
```

### Projection: Advanced Statistics

```csharp
public class AdvancedStatisticsProjection :
    IHandleEvent<FieldGoalAttempted>,
    IHandleEvent<FieldGoalMade>,
    IHandleEvent<ReboundSecured>
{
    public async Task HandleAsync(FieldGoalMade @event)
    {
        var player = await LoadPlayerStats(@event.PlayerId);

        // True Shooting % = PTS / (2 * (FGA + 0.44 * FTA))
        var ts = CalculateTrueShootingPercentage(player);

        await PublishEvent(new TrueShootingPercentageCalculated
        {
            PlayerId = @event.PlayerId,
            Value = ts,
            Numerator = player.PTS,
            Denominator = 2 * (player.FGA + 0.44 * player.FTA),
            BasedOnEventId = @event.EventId
        });
    }
}
```

---

## 3. Rule Archetype + ES-D Implementation

### Mapping

```
Rule Archetype          →  ES-D Streams
────────────────────────────────────────────
PersonalFoulLimitRule  →  violations-{matchId}
TeamFoulBonusRule      →  violations-{matchId}
ShotClockRule          →  game-flow-{matchId}
SubstitutionRule       →  lineup-{matchId}
```

### Stream: violations-{matchId}

```csharp
// ═══════════════════════════════════════════════════════
// Archetype: Rule (Business Rules as First-Class Objects)
// ES-D: Violations & Fouls
// ═══════════════════════════════════════════════════════

// Rule Definition (Archetype)
public interface IBasketballRule
{
    string RuleId { get; }
    string RuleName { get; }
    string RuleVersion { get; set; }  // "NBA2024", "FIBA2024"

    RuleEvaluationResult Evaluate(RuleContext context);
}

// Rule Implementation
public class PersonalFoulLimitRule : IBasketballRule
{
    public string RuleId => "PERSONAL_FOUL_LIMIT";
    public string RuleName => "Personal Foul Limit Rule";
    public string RuleVersion { get; set; }

    private int _maxFouls;

    public PersonalFoulLimitRule(string ruleVersion)
    {
        RuleVersion = ruleVersion;
        _maxFouls = ruleVersion switch
        {
            "NBA2024" => 6,
            "FIBA2024" => 5,
            "NCAA2024" => 5,
            _ => 6
        };
    }

    public RuleEvaluationResult Evaluate(RuleContext context)
    {
        var playerFouls = context.GetValue<int>("PlayerFoulCount");

        if (playerFouls >= _maxFouls)
        {
            return new RuleEvaluationResult
            {
                Triggered = true,
                RuleId = RuleId,
                Consequence = new PlayerDisqualification
                {
                    Reason = $"Personal fouls limit exceeded ({_maxFouls})",
                    MustLeaveGame = true
                },
                Metadata = new Dictionary<string, object>
                {
                    ["FoulCount"] = playerFouls,
                    ["FoulLimit"] = _maxFouls,
                    ["RuleVersion"] = RuleVersion
                }
            };
        }

        return RuleEvaluationResult.NotTriggered;
    }
}

// Team Foul Bonus Rule
public class TeamFoulBonusRule : IBasketballRule
{
    public string RuleId => "TEAM_FOUL_BONUS";
    public string RuleName => "Team Foul Bonus Rule";
    public string RuleVersion { get; set; }

    private int _bonusThreshold;

    public TeamFoulBonusRule(string ruleVersion)
    {
        RuleVersion = ruleVersion;
        _bonusThreshold = ruleVersion switch
        {
            "NBA2024" => 5,
            "FIBA2024" => 5,
            _ => 5
        };
    }

    public RuleEvaluationResult Evaluate(RuleContext context)
    {
        var teamFouls = context.GetValue<int>("TeamFoulsInQuarter");
        var foulType = context.GetValue<FoulType>("FoulType");

        // Offensive fouls don't trigger bonus
        if (foulType == FoulType.OFFENSIVE)
            return RuleEvaluationResult.NotTriggered;

        if (teamFouls >= _bonusThreshold)
        {
            return new RuleEvaluationResult
            {
                Triggered = true,
                RuleId = RuleId,
                Consequence = new FreeThrowsAwarded
                {
                    FreeThrowCount = 2,
                    Reason = "Team in bonus situation"
                },
                Metadata = new Dictionary<string, object>
                {
                    ["TeamFouls"] = teamFouls,
                    ["InBonusSituation"] = true
                }
            };
        }

        return RuleEvaluationResult.NotTriggered;
    }
}

// Shot Clock Rule
public class ShotClockRule : IBasketballRule
{
    public string RuleId => "SHOT_CLOCK";
    public string RuleName => "Shot Clock Violation Rule";
    public string RuleVersion { get; set; }

    private int _shotClockSeconds = 24;

    public RuleEvaluationResult Evaluate(RuleContext context)
    {
        var elapsed = context.GetValue<TimeSpan>("PossessionDuration");

        if (elapsed.TotalSeconds > _shotClockSeconds)
        {
            return new RuleEvaluationResult
            {
                Triggered = true,
                RuleId = RuleId,
                Consequence = new TurnoverForced
                {
                    TurnoverType = TurnoverType.SECONDS_24,
                    PossessionChange = true
                }
            };
        }

        return RuleEvaluationResult.NotTriggered;
    }
}
```

### Events: Rule Evaluation

```csharp
// Event: Rule was evaluated
public class RuleEvaluated : IDomainEvent
{
    public string RuleId { get; set; }
    public string RuleName { get; set; }
    public string RuleVersion { get; set; }
    public bool Triggered { get; set; }
    public RuleContext Context { get; set; }
    public RuleConsequence? Consequence { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
    public DateTime EvaluatedAt { get; set; }
}

// Event: Foul with rule evaluation results
public class FoulCommitted : IDomainEvent
{
    public int MatchId { get; set; }
    public int PlayerWhoFouled { get; set; }
    public int? PlayerWhoWasFouled { get; set; }
    public FoulType FoulType { get; set; }

    // Rule evaluation results (Archetype!)
    public List<RuleEvaluationResult> RulesEvaluated { get; set; }

    // State after rule evaluation
    public int PlayerFoulCount { get; set; }
    public int TeamFoulsInQuarter { get; set; }
    public bool InBonusSituation { get; set; }
    public bool PlayerDisqualified { get; set; }
}
```

### Aggregate: FoulManagement with Rule Engine

```csharp
public class FoulManagement : AggregateRoot
{
    private readonly IRuleEngine _ruleEngine;
    private Dictionary<int, int> _playerFouls = new();
    private Dictionary<int, int> _teamFoulsPerQuarter = new();
    private int _currentQuarter = 1;
    private string _ruleVersion; // "NBA2024", "FIBA2024"

    public void CommitFoul(
        int playerWhoFouled,
        int? playerWhoWasFouled,
        FoulType foulType,
        bool isGuest)
    {
        var playerFoulCount = _playerFouls.GetValueOrDefault(playerWhoFouled, 0) + 1;
        var teamFouls = _teamFoulsPerQuarter.GetValueOrDefault(_currentQuarter, 0) + 1;

        // Build rule context
        var context = new RuleContext();
        context.SetValue("PlayerFoulCount", playerFoulCount);
        context.SetValue("TeamFoulsInQuarter", teamFouls);
        context.SetValue("FoulType", foulType);
        context.SetValue("Quarter", _currentQuarter);

        // Evaluate ALL applicable rules
        var rules = new List<IBasketballRule>
        {
            new PersonalFoulLimitRule(_ruleVersion),
            new TeamFoulBonusRule(_ruleVersion),
            new UnsportsmanlikeFoulRule(_ruleVersion),
            new DisqualifyingFoulRule(_ruleVersion)
        };

        var evaluationResults = new List<RuleEvaluationResult>();

        foreach (var rule in rules)
        {
            var result = rule.Evaluate(context);
            evaluationResults.Add(result);

            // Publish rule evaluation event
            ApplyEvent(new RuleEvaluated
            {
                RuleId = rule.RuleId,
                RuleName = rule.RuleName,
                RuleVersion = rule.RuleVersion,
                Triggered = result.Triggered,
                Context = context,
                Consequence = result.Consequence,
                Metadata = result.Metadata,
                EvaluatedAt = DateTime.UtcNow
            });

            if (result.Triggered)
            {
                ApplyConsequence(result.Consequence);
            }
        }

        // Main foul event
        var @event = new FoulCommitted
        {
            MatchId = Id,
            PlayerWhoFouled = playerWhoFouled,
            PlayerWhoWasFouled = playerWhoWasFouled,
            FoulType = foulType,
            Quarter = _currentQuarter,
            IsGuest = isGuest,
            RulesEvaluated = evaluationResults,
            PlayerFoulCount = playerFoulCount,
            TeamFoulsInQuarter = teamFouls,
            InBonusSituation = evaluationResults.Any(r => r.RuleId == "TEAM_FOUL_BONUS" && r.Triggered),
            PlayerDisqualified = evaluationResults.Any(r => r.RuleId == "PERSONAL_FOUL_LIMIT" && r.Triggered)
        };

        ApplyEvent(@event);
    }

    private void ApplyConsequence(RuleConsequence consequence)
    {
        switch (consequence)
        {
            case PlayerDisqualification disq:
                ApplyEvent(new PlayerDisqualified
                {
                    PlayerId = disq.PlayerId,
                    Reason = disq.Reason
                });
                break;

            case FreeThrowsAwarded freeThrows:
                ApplyEvent(new FreeThrowsAwarded
                {
                    PlayerId = freeThrows.PlayerId,
                    FreeThrowCount = freeThrows.FreeThrowCount
                });
                break;
        }
    }
}
```

### Why This is Portfolio GOLD

**1. Auditability** - see exactly which rules were evaluated:
```csharp
var ruleEvaluations = await _eventStore
    .QueryStreamAsync<RuleEvaluated>("violations-match-123")
    .Where(e => e.Triggered)
    .ToListAsync();

// Output:
// Rule: PERSONAL_FOUL_LIMIT, Triggered: YES, Player: #23, FoulCount: 6
// Rule: TEAM_FOUL_BONUS, Triggered: YES, TeamFouls: 5, Bonus: 2 FT
```

**2. Rule Versioning** - temporal correctness:
```csharp
// Match from 2020 used different rules
var match2020 = await _eventStore.LoadAggregateAsync<Match>("match-old-123");
// RuleVersion: "NBA2020"

// Match from 2024 uses current rules
var match2024 = await _eventStore.LoadAggregateAsync<Match>("match-new-456");
// RuleVersion: "NBA2024"
```

**3. Rule Testing**:
```csharp
[Fact]
public void PersonalFoulLimitRule_NBA_DisqualifiesAt6Fouls()
{
    var rule = new PersonalFoulLimitRule("NBA2024");
    var context = new RuleContext();
    context.SetValue("PlayerFoulCount", 6);

    var result = rule.Evaluate(context);

    Assert.True(result.Triggered);
    Assert.IsType<PlayerDisqualification>(result.Consequence);
}

[Fact]
public void PersonalFoulLimitRule_FIBA_DisqualifiesAt5Fouls()
{
    var rule = new PersonalFoulLimitRule("FIBA2024");
    var context = new RuleContext();
    context.SetValue("PlayerFoulCount", 5);

    var result = rule.Evaluate(context);

    Assert.True(result.Triggered);
}
```

---

## Complete System Architecture

```
┌─────────────────────────────────────────────────────────┐
│  ARCHETYPE LAYER (Contracts & Interfaces)               │
│  IParty, IQuantity, IRule, IEvent, IProduct             │
│  - Universal abstractions                               │
│  - Reusable across domains                              │
└──────────────────────────┬──────────────────────────────┘
                           │ Implemented by
                           ▼
┌─────────────────────────────────────────────────────────┐
│  DOMAIN LAYER (Basketball-Specific)                     │
│  Player : IParty                                        │
│  Points : IQuantity                                     │
│  PersonalFoulLimitRule : IRule                          │
│  BasketballMatch : IProduct                             │
└──────────────────────────┬──────────────────────────────┘
                           │ Persisted via
                           ▼
┌─────────────────────────────────────────────────────────┐
│  ES-D STREAMS (Event Sourcing)                          │
│  ┌───────────────┐  ┌───────────────┐  ┌─────────────┐│
│  │ game-flow-{id}│  │ scoring-{id}  │  │violations-  ││
│  │ GameStarted   │  │ ShotAttempted │  │{id}         ││
│  │ QuarterEnded  │  │ PointsScored  │  │FoulCommitted││
│  └───────────────┘  └───────────────┘  │RuleEvaluated││
│                                         └─────────────┘│
│  ┌───────────────┐  ┌───────────────┐  ┌─────────────┐│
│  │player-actions │  │ lineup-{id}   │  │party-role-  ││
│  │AssistAwarded  │  │ PlayerSubbed  │  │{id}         ││
│  │ReboundSecured │  │ LineupChanged │  │RoleAssigned ││
│  └───────────────┘  └───────────────┘  └─────────────┘│
└──────────────────────────┬──────────────────────────────┘
                           │ Projected to
                           ▼
┌─────────────────────────────────────────────────────────┐
│  READ MODELS (CQRS)                                     │
│  PostgreSQL: Structured queries                         │
│  Elasticsearch: Search & analytics                      │
│  Redis: Real-time data                                  │
└─────────────────────────────────────────────────────────┘
```

---

## Portfolio Benefits Summary

| Aspect | ES-D Only | ES-D + Archetypes |
|--------|-----------|-------------------|
| **Domain Understanding** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Reusability** | ⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Portfolio Value** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Testability** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Enterprise Credibility** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Cross-domain Learning** | ⭐⭐ | ⭐⭐⭐⭐⭐ |

---

## Interview Talking Points

When discussing this project in interviews:

1. **"I implemented Event Sourcing with domain-specific streams"**
   - Shows technical depth
   - Event Sourcing is advanced topic

2. **"I applied enterprise archetype patterns from Arlow's book"**
   - Shows theoretical knowledge
   - Familiar with academic/enterprise patterns

3. **"All business rules are first-class objects, version-controlled"**
   - Shows maintainability thinking
   - Temporal correctness (NBA2024 vs FIBA2024 rules)

4. **"Statistics follow the Quantity archetype with temporal queries"**
   - Shows abstraction skills
   - Can query any stat at any point in time

5. **"Player lifecycle uses Party archetype with role management"**
   - Shows understanding of complex domain modeling
   - Temporal tracking of player participation

---

## Blog Post Ideas

1. **"Implementing Party Archetype with Event Sourcing"**
   - How player roles work in basketball
   - Temporal queries for lineup management
   - Code examples

2. **"Rule Engine in Basketball: From Domain Rules to First-Class Objects"**
   - Why rules should be objects
   - Rule versioning (NBA vs FIBA)
   - Testing business logic

3. **"Quantity Archetype: Modeling Basketball Statistics"**
   - Countable, Ratio, and Temporal quantities
   - Derived metrics
   - Temporal queries for statistics

4. **"Event Sourcing + Archetypes = Portfolio Gold"**
   - Why this combination works
   - How to apply to other domains
   - Lessons learned

---

## Implementation Roadmap

### Phase 1: Setup Event Store (2 weeks)
- Install Marten
- Configure PostgreSQL
- Basic event store operations
- Simple aggregate (Match lifecycle)

### Phase 2: Implement ES-D Streams (6 weeks)
- game-flow-{matchId}
- scoring-{matchId}
- violations-{matchId}
- lineup-{matchId}
- player-actions-{matchId}

### Phase 3: Apply Archetypes (4 weeks)
- Party archetype for Player/Team
- Quantity archetype for all statistics
- Rule archetype for basketball rules
- Refactor events to use archetypes

### Phase 4: Projections (4 weeks)
- PostgreSQL projections
- Redis for real-time
- Elasticsearch for analytics
- Advanced statistics

### Phase 5: Polish & Documentation (2 weeks)
- Blog posts
- README updates
- Architecture diagrams
- Demo video

**Total: ~4-5 months**

---

## Next Steps

Ready to start implementation? See:
- `implementation-guide.md` for detailed step-by-step
- `marten-setup.md` for Event Store configuration
- `aggregate-examples.md` for complete aggregate code

This is your path to a **world-class portfolio project** that showcases senior-level skills! 🏀🔥

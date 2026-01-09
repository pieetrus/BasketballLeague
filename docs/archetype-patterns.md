# Enterprise Archetype Patterns for Basketball League

Based on "Enterprise Patterns and MDA: Building Better Software with Archetype Patterns" by Jim Arlow and Ila Neustadt.

## Overview

Archetypes are universal, reusable domain patterns that appear across different business domains. Applying these patterns to Basketball League provides:
- **Universal structure** - recognized patterns
- **Reusability** - applicable to other domains
- **Enterprise credibility** - shows theoretical knowledge
- **Cross-domain learning** - skills transfer to other projects

---

## 1. Party Archetype

### Definition
Represents people, organizations, or roles that participate in business processes.

### Application in Basketball League

```
Party (abstract)
├── Person
│   ├── Player
│   ├── Coach
│   ├── Referee
│   └── Official
├── Organization
│   ├── Team
│   ├── League
│   └── Sponsor
└── PartyRole
    ├── PlayerSeasonRole (Player in specific Season)
    ├── CoachRole (Coach assigned to Team)
    └── RefereeRole (Referee assigned to Match)
```

### Event Sourcing Implementation

#### Stream: party-role-{roleId}

```csharp
// Party Role Assigned
public class PartyRoleAssigned : IDomainEvent
{
    public int PartyId { get; set; }         // PlayerId
    public int ContextId { get; set; }       // SeasonId
    public RoleType RoleType { get; set; }   // "PlayerInSeason"
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}

// Party Role Changed
public class PartyRoleReassigned : IDomainEvent
{
    public int RoleId { get; set; }
    public int OldContextId { get; set; }    // Old TeamSeasonId
    public int NewContextId { get; set; }    // New TeamSeasonId
    public DateTime EffectiveDate { get; set; }
}

// Party Role Ended
public class PartyRoleEnded : IDomainEvent
{
    public int RoleId { get; set; }
    public DateTime EndDate { get; set; }
    public EndReason Reason { get; set; }    // Retired, Released, Traded
}
```

#### Stream: lineup-{matchId} (Party Role in Context)

```csharp
// Player enters match (Party Role activation)
public class PlayerEnteredMatch : IDomainEvent
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public bool IsStarter { get; set; }
    public DateTime Timestamp { get; set; }

    // Party Role attributes
    public string JerseyNumber { get; set; }
    public Position Position { get; set; }
}

// Player exits match (Party Role deactivation)
public class PlayerExitedMatch : IDomainEvent
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public DateTime Timestamp { get; set; }
    public SubstitutionReason Reason { get; set; } // Tactical, FoulOut, Injury
}

// Substitution (Role handoff)
public class PlayerSubstituted : IDomainEvent
{
    public int PlayerOutId { get; set; }  // Role A ended
    public int PlayerInId { get; set; }   // Role B started
    public LineupSnapshot CurrentLineup { get; set; }  // Collection of Party Roles
}
```

### Why This Matters

**Temporal Party Roles:**
```csharp
// Query: "Who played in this match?"
var lineup = await _eventStore
    .AggregateStreamAsync<LineupAggregate>("lineup-match-123");
var playersInMatch = lineup.GetAllPlayersWhoPlayed();

// "What was the lineup at start of Q4?"
var q4Lineup = lineup.GetLineupAtQuarter(4);

// "When did LeBron enter the game?"
var lebronEntry = lineup.GetPlayerEntryTime(lebronId);
```

**Cross-Stream Correlations:**
```csharp
// Player aggregate consuming events from multiple streams
public class PlayerMatchParticipation
{
    // From lineup-{matchId}
    public TimeSpan TimeOnCourt { get; private set; }

    // From scoring-{matchId}
    public int PointsScored { get; private set; }

    // From violations-{matchId}
    public int FoulsCommitted { get; private set; }

    // Plus/Minus (advanced Party Role metric)
    public int PlusMinus { get; private set; }
}
```

### Benefits
- Track historical party roles (player transfers, team changes)
- Role-based permissions and access control
- Temporal queries about participation
- Audit trail of role assignments

---

## 2. Quantity Archetype

### Definition
Represents measurable aspects of the system - countable, ratio-based, or temporal quantities.

### Application in Basketball League

```
Quantity (abstract)
├── CountableQuantity (discrete)
│   ├── Points
│   ├── Assists
│   ├── Rebounds
│   └── Fouls
├── RatioQuantity (derived)
│   ├── FieldGoalPercentage (FGM / FGA)
│   ├── ThreePointPercentage (FG3M / FG3A)
│   └── TrueShootingPercentage
└── TemporalQuantity (time-based)
    ├── TimeOnCourt
    ├── ShotClockRemaining
    └── PossessionDuration
```

### Event Sourcing Implementation

#### Stream: scoring-{matchId}

```csharp
// Quantity: Points (Countable)
public class PointsScored : IDomainEvent
{
    public int MatchId { get; set; }
    public int PlayerId { get; set; }

    // Quantity properties
    public int Quantity { get; set; }        // 2 or 3 points
    public string Unit { get; set; }         // "points"
    public QuantityType Type { get; set; }   // "FieldGoal" or "FreeThrow"

    // Context
    public ShotType ShotType { get; set; }
    public int Quarter { get; set; }
    public TimeSpan GameTime { get; set; }

    // Measurement metadata
    public MeasurementContext MeasurementContext { get; set; }
}

// Supporting class
public class MeasurementContext
{
    public DateTime Timestamp { get; set; }
    public string MeasuredBy { get; set; }      // "Official", "System"
    public double Confidence { get; set; }       // 0.0 to 1.0
    public string ValidationMethod { get; set; } // "Manual", "ShotTracking"
}

// Quantity: Field Goal Attempt
public class FieldGoalAttempted : IDomainEvent
{
    public Guid AttemptId { get; set; }
    public int PlayerId { get; set; }
    public int PotentialValue { get; set; }  // 2 or 3

    // Spatial quantity
    public ShotLocation Location { get; set; }
    public double DistanceFromBasket { get; set; }  // in feet

    // Temporal quantity
    public int ShotClockRemaining { get; set; }
    public TimeSpan GameTimeRemaining { get; set; }
}
```

#### Stream: statistics-derived-{matchId}

```csharp
// Ratio Quantity (derived from base quantities)
public class FieldGoalPercentageCalculated : IDomainEvent
{
    public int EntityId { get; set; }
    public EntityType EntityType { get; set; }  // Player or Team

    // Ratio Quantity archetype
    public decimal Numerator { get; set; }    // FGM
    public decimal Denominator { get; set; }  // FGA
    public decimal Ratio { get; set; }        // FGM / FGA
    public string Unit { get; set; }          // "percentage"

    // Calculation metadata
    public List<Guid> BaseEventIds { get; set; }  // Source events
}

// Complex derived quantity (PER)
public class PlayerEfficiencyRatingCalculated : IDomainEvent
{
    public int PlayerId { get; set; }

    // PER formula: (PTS + REB + AST + STL + BLK - MissedFG - MissedFT - TO) / MIN
    public decimal PER { get; set; }

    // Components (all Quantity archetypes)
    public Dictionary<string, decimal> Components { get; set; }
    /*
    {
        "PTS": 25.0,
        "REB": 8.0,
        "AST": 7.0,
        "STL": 2.0,
        "BLK": 1.0,
        "MissedFG": 8.0,
        "MissedFT": 2.0,
        "TO": 3.0,
        "MIN": 35.0
    }
    */
}

// Temporal Quantity
public class PossessionDurationMeasured : IDomainEvent
{
    public Guid PossessionId { get; set; }
    public int TeamId { get; set; }

    // Temporal Quantity
    public TimeSpan Duration { get; set; }
    public string Unit { get; set; }  // "seconds"

    // Outcome
    public PossessionOutcome Outcome { get; set; }
    public int? PointsScored { get; set; }
}
```

### Unified Quantity Model

```csharp
// All statistics inherit from Quantity
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

// Temporal query for any quantity
var query = new TemporalQuantityQuery
{
    EntityId = playerId,
    QuantityType = "PTS",
    AsOf = new DateTime(2024, 3, 15, 20, 30, 0), // 3Q, 6:30 remaining
    MatchId = matchId
};

var pointsAt3Q = await _quantityRepository.GetAsync(query);
// Returns: 18 points (at that moment in game)
```

### Derived Quantities (Projections)

```csharp
public class AdvancedStatisticsProjection :
    IHandleEvent<FieldGoalAttempted>,
    IHandleEvent<FieldGoalMade>
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

### Benefits
- Unified model for all statistics
- Temporal queries (stats at any point in time)
- Derived metrics calculated from base quantities
- Different projections for same data (per game, per 36 min, per 100 possessions)

---

## 3. Rule Archetype

### Definition
Encapsulates business logic as first-class objects. Rules can be versioned, tested, and modified independently.

### Application in Basketball League

```
Rule (abstract)
├── ScoringRule
│   ├── FieldGoalScoringRule (2pt vs 3pt)
│   ├── FreeThrowScoringRule
│   └── And1Rule (continuation)
├── FoulRule
│   ├── PersonalFoulLimitRule (6 fouls = foul out)
│   ├── TeamFoulBonusRule (5th foul = bonus FT)
│   └── TechnicalFoulRule
├── TimeRule
│   ├── ShotClockRule (24 seconds)
│   ├── QuarterLengthRule (12 min NBA, 10 min FIBA)
│   └── OvertimeRule
└── SubstitutionRule
    └── DeadBallSubstitutionRule
```

### Event Sourcing Implementation

#### Rule Interface

```csharp
public interface IBasketballRule
{
    string RuleId { get; }
    string RuleName { get; }
    string RuleVersion { get; set; }  // "NBA2024", "FIBA2024"

    RuleEvaluationResult Evaluate(RuleContext context);
}
```

#### Personal Foul Limit Rule

```csharp
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
```

#### Team Foul Bonus Rule

```csharp
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
            "NBA2024" => 5,   // 5th team foul = bonus
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
```

#### Shot Clock Rule

```csharp
public class ShotClockRule : IBasketballRule
{
    public string RuleId => "SHOT_CLOCK";
    public string RuleName => "Shot Clock Violation Rule";
    public string RuleVersion { get; set; }

    private int _shotClockSeconds = 24; // Same for NBA and FIBA

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

### Rule Evaluation Events

```csharp
// Event: Rule Evaluated
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

// Event: Foul with Rule Evaluation Results
public class FoulCommitted : IDomainEvent
{
    public int MatchId { get; set; }
    public int PlayerWhoFouled { get; set; }
    public FoulType FoulType { get; set; }

    // Rule evaluation results (Archetype!)
    public List<RuleEvaluationResult> RulesEvaluated { get; set; }

    // State after rule evaluation
    public int PlayerFoulCount { get; set; }
    public bool InBonusSituation { get; set; }
    public bool PlayerDisqualified { get; set; }
}
```

### Rule Engine (in Aggregate)

```csharp
public class FoulManagementAggregate : AggregateRoot
{
    private string _ruleVersion; // "NBA2024", "FIBA2024"

    public void CommitFoul(int playerWhoFouled, FoulType foulType)
    {
        // Build rule context
        var context = new RuleContext();
        context.SetValue("PlayerFoulCount", playerFoulCount);
        context.SetValue("TeamFoulsInQuarter", teamFouls);
        context.SetValue("FoulType", foulType);

        // Evaluate ALL applicable rules
        var rules = new List<IBasketballRule>
        {
            new PersonalFoulLimitRule(_ruleVersion),
            new TeamFoulBonusRule(_ruleVersion),
            new UnsportsmanlikeFoulRule(_ruleVersion)
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
                Triggered = result.Triggered,
                Context = context,
                Consequence = result.Consequence
            });

            if (result.Triggered)
            {
                ApplyConsequence(result.Consequence);
            }
        }

        // Main foul event with rule results
        var @event = new FoulCommitted
        {
            PlayerWhoFouled = playerWhoFouled,
            FoulType = foulType,
            RulesEvaluated = evaluationResults,
            InBonusSituation = evaluationResults.Any(r => r.RuleId == "TEAM_FOUL_BONUS"),
            PlayerDisqualified = evaluationResults.Any(r => r.RuleId == "PERSONAL_FOUL_LIMIT")
        };

        ApplyEvent(@event);
    }
}
```

### Rule Versioning

```csharp
// Rules can change between seasons!
public interface ISeasonRules
{
    int PersonalFoulLimit { get; }
    int QuarterLengthMinutes { get; }
    bool Has3PointLine { get; }
}

public class NBA2024Rules : ISeasonRules
{
    public int PersonalFoulLimit => 6;
    public int QuarterLengthMinutes => 12;
    public bool Has3PointLine => true;
}

public class FIBA2024Rules : ISeasonRules
{
    public int PersonalFoulLimit => 5;  // Different!
    public int QuarterLengthMinutes => 10;
    public bool Has3PointLine => true;
}

// In Event Store, save which rules were used
public class MatchStarted : IDomainEvent
{
    public int MatchId { get; set; }
    public string RuleSetVersion { get; set; }  // "NBA2024" or "FIBA2024"
}
```

### Why This is GOLD

**1. Auditability** - see exactly which rules were evaluated:
```csharp
var ruleEvaluations = await _eventStore
    .QueryStreamAsync<RuleEvaluated>("violations-match-123")
    .Where(e => e.Triggered)
    .ToListAsync();

// Output:
// Rule: PERSONAL_FOUL_LIMIT, Triggered: YES, FoulCount: 6
// Rule: TEAM_FOUL_BONUS, Triggered: YES, TeamFouls: 5
```

**2. Rule Versioning** - temporal correctness:
```csharp
// Match from 2020 used different rules
var match2020 = await _eventStore.LoadAggregateAsync<Match>("match-123");
// RuleVersion: "NBA2020"

// Match from 2024 uses current rules
var match2024 = await _eventStore.LoadAggregateAsync<Match>("match-456");
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
```

### Benefits
- Rules as first-class objects
- Version control for rules (temporal correctness)
- Easy testing of business logic
- Multi-league support (NBA vs FIBA rules)
- Audit trail of rule evaluations

---

## 4. Product Archetype

### Definition
Represents things or services that are the subject of transactions.

### Application in Basketball League

Match as a "product" (sporting event as a service):

```
Product (abstract)
├── SportingEvent
│   └── BasketballMatch
│       ├── RegularSeasonMatch
│       ├── PlayoffMatch
│       └── FriendlyMatch
└── Ticket
    └── MatchTicket
```

### Event Sourcing Implementation

```csharp
// Stream: product-{matchId}

// Match as a product is created
public class MatchOffered : IDomainEvent
{
    public int MatchId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int VenueCapacity { get; set; }
    public decimal? TicketPrice { get; set; }
}

// Tickets sold
public class TicketSold : IDomainEvent
{
    public int MatchId { get; set; }
    public int TicketCount { get; set; }
    public decimal TotalPrice { get; set; }
}

// Match delivered
public class MatchDelivered : IDomainEvent
{
    public int MatchId { get; set; }
    public int ActualAttendance { get; set; }
    public DateTime ActualStartTime { get; set; }
}
```

### Use Cases
- If you extend system with **ticket sales** (revenue tracking)
- **Attendance tracking** as metrics
- **Match scheduling** - availability as "inventory"

---

## 5. Event Archetype

### Definition
Things that happen in the domain with business significance.

**You already have this!** Event Sourcing naturally implements this pattern.

```
BusinessEvent (abstract)
├── ScheduledEvent
│   ├── MatchScheduled
│   └── PracticeScheduled
├── OperationalEvent
│   ├── MatchStarted
│   ├── MatchEnded
│   ├── IncidentOccurred
│   └── PlayerInjured
└── AdministrativeEvent
    ├── PlayerRegistered
    ├── PlayerTransferred
    └── SeasonEnded
```

---

## 6. Transaction/Contract Archetype

### Definition
Represents agreements and transactions between parties.

### Application in Basketball League

```
Transaction (abstract)
├── PlayerContract
│   ├── Duration (from, to)
│   ├── Team
│   └── Salary
├── MatchContract
│   ├── HomeTeam agreement
│   ├── AwayTeam agreement
│   └── Officials assignment
└── SeasonRegistration
    ├── TeamSeason
    └── PlayerSeason
```

### Event Sourcing Implementation

```csharp
// Stream: contract-{contractId}

public class ContractProposed : IDomainEvent
{
    public int PartyA { get; set; }  // Player
    public int PartyB { get; set; }  // Team
    public ContractTerms Terms { get; set; }
}

public class ContractSigned : IDomainEvent
{
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public class ContractTerminated : IDomainEvent
{
    public DateTime TerminationDate { get; set; }
    public TerminationReason Reason { get; set; }  // Trade, Release
}
```

### Use Cases
- Player **contract management**
- **Transfer logic** between teams
- **Eligibility rules** - can player play in this match?

---

## Summary: Archetype Benefits

| Archetype | Primary Benefit | Basketball Application |
|-----------|----------------|------------------------|
| **Party** | Role management, temporal tracking | Player roles, team rosters, lineup management |
| **Quantity** | Unified statistics model | All basketball stats, derived metrics |
| **Rule** | Testable business logic | Basketball rules, league differences (NBA/FIBA) |
| **Product** | Transaction tracking | Match as service, ticket sales |
| **Event** | Audit trail | Already implemented via Event Sourcing |
| **Transaction** | Agreements tracking | Player contracts, transfers |

## Portfolio Impact

Using archetypes in your project shows:
1. ✅ **Theoretical knowledge** - familiar with enterprise patterns
2. ✅ **Practical application** - not just theory, but implementation
3. ✅ **Cross-domain thinking** - patterns applicable beyond basketball
4. ✅ **Senior-level understanding** - architectural patterns

## Interview Talking Points

- "I used Arlow's Party archetype for player lifecycle management"
- "All statistics follow the Quantity archetype pattern"
- "Business rules are first-class objects, version-controlled"
- "The system applies enterprise patterns from Arlow's book"

## Next Steps

See `es-d-plus-archetypes.md` for how to combine Event Sourcing Variant D with these archetype patterns for maximum portfolio impact.

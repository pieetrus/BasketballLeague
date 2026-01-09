# Basketball League - Architecture Evolution Documentation

## Overview

This documentation describes the architectural evolution of Basketball League from a traditional Clean Architecture layered monolith to an advanced Event-Sourced system with Enterprise Archetype Patterns.

**Goal:** Transform this student project into a world-class portfolio showcase demonstrating senior-level architectural skills, Event Sourcing, DDD, and enterprise patterns.

---

## Documentation Structure

### 📋 [Plan](./plan.md)
Initial strategic vision and rationale for the architectural evolution project.
- Why this approach for portfolio
- Value proposition for senior developers
- Comparison with other reference projects
- Stage-by-stage evolution roadmap

### 🔄 [Event Sourcing Variants](./event-sourcing-variants.md)
Comprehensive analysis of 4 different Event Sourcing approaches, from minimalist to advanced.
- **ES-A:** Single Event Stream per Match (Minimalist) - ⭐⭐⭐☆☆
- **ES-B:** Multi-Stream Event Sourcing (Recommended) - ⭐⭐⭐⭐⭐
- **ES-C:** Hybrid Event Sourced Write + Traditional Read (Pragmatic) - ⭐⭐⭐☆☆
- **ES-D:** Domain-Specific Streams (Basketball-Optimized) - ⭐⭐⭐⭐⭐

Each variant includes:
- Architecture diagrams
- Event stream structures
- Projection examples
- Pros/cons analysis
- Implementation timelines

### 🎯 [Archetype Patterns](./archetype-patterns.md)
Enterprise patterns from "Enterprise Patterns and MDA" by Jim Arlow applied to Basketball League.
- **Party Archetype** - Player lifecycle, roles, temporal tracking
- **Quantity Archetype** - Unified statistics model, countable/ratio/temporal quantities
- **Rule Archetype** - Basketball rules as first-class objects, versioning
- **Product Archetype** - Match as service, ticket sales
- **Event Archetype** - Already implemented via Event Sourcing
- **Transaction/Contract Archetype** - Player contracts, transfers

Includes code examples, Event Sourcing implementations, and portfolio benefits.

### 🔥 [ES-D + Archetype Patterns](./es-d-plus-archetypes.md) ⭐ RECOMMENDED
**The ultimate combination** - Basketball-optimized Event Sourcing with Enterprise Archetypes.

This document shows:
- How ES-D streams map to Archetype patterns
- Complete implementation of Party + Quantity + Rule archetypes
- Basketball-specific aggregates with rich invariants
- Cross-stream correlations and Process Managers
- Why this combination is portfolio gold

**Portfolio Impact:** ⭐⭐⭐⭐⭐ (Maximum)

### 🏗️ [Bounded Contexts Analysis](./bounded-contexts-analysis.md)
Analysis of different bounded context strategies and domain boundaries.
- **Option 1:** Conservative Modular Monolith - Quick start
- **Option 2:** Domain-Driven Modular Monolith - Recommended first step
- **Option 3:** Event-Sourced Core + Traditional Supporting
- **Option 4:** Multi-Stream Event Sourcing - Advanced
- **Option 5:** Domain-Specific Streams - Basketball-optimized

Decision matrix, pros/cons, and phased evolution approach.

---

## Recommended Learning Path

### For Senior .NET Developer (6 years experience)

#### Phase 1: Modular Monolith (2-3 months)
**Start here:** Option 2 from Bounded Contexts Analysis
- Refactor to modular monolith with bounded contexts
- Implement domain events between modules
- Learn DDD tactical patterns

**Documents to read:**
- `bounded-contexts-analysis.md` - Option 2
- `plan.md` - Stage 1

**Deliverables:**
- MatchEngine, CompetitionManagement, Catalog modules
- Domain events via MediatR
- Blog post: "From Clean Architecture to Modular Monolith"

#### Phase 2: Event Sourcing (2-3 months)
**Evolve to:** ES-D (Event Sourcing Variant D)
- Implement domain-specific event streams
- Basketball-optimized aggregates
- Projections to PostgreSQL, Redis, Elasticsearch

**Documents to read:**
- `event-sourcing-variants.md` - Variant D
- `es-d-plus-archetypes.md` - Sections 1-3

**Deliverables:**
- Event Store (Marten on PostgreSQL)
- game-flow, scoring, violations, lineup, player-actions streams
- Blog post: "Event Sourcing Basketball Matches"

#### Phase 3: Enterprise Patterns (1-2 months)
**Apply:** Archetype Patterns
- Party archetype for player lifecycle
- Quantity archetype for statistics
- Rule archetype for basketball rules

**Documents to read:**
- `archetype-patterns.md` - Complete
- `es-d-plus-archetypes.md` - Complete

**Deliverables:**
- Rules as first-class objects (NBA2024 vs FIBA2024)
- Temporal queries for all statistics
- Blog post: "Applying Enterprise Archetypes to Event Sourcing"

#### Phase 4: Cloud-Native (ongoing)
**Deploy to:** Azure
- Bicep infrastructure as code
- AKS or Container Apps
- Application Insights, Redis Cache, Service Bus
- CI/CD pipelines

**Deliverables:**
- Production-grade deployment
- Monitoring and alerting
- Blog post: "Scaling Event-Sourced System on Azure"

---

## Quick Reference

### Technology Stack

**Current:**
- .NET Core 3.1
- SQL Server
- Entity Framework Core
- MediatR (CQRS)
- SignalR (real-time)
- ASP.NET Core Identity

**Target (After Evolution):**
- .NET 8+
- Event Store (Marten on PostgreSQL or EventStoreDB)
- PostgreSQL (structured queries)
- Elasticsearch (search & analytics)
- Redis (real-time data)
- Azure infrastructure (AKS, Service Bus, etc.)

### Key Patterns

- **Event Sourcing** - All match events stored as immutable event stream
- **CQRS** - Separate read models from write models
- **DDD** - Bounded contexts, aggregates, domain events
- **Saga/Process Manager** - Orchestrate cross-stream workflows
- **Archetype Patterns** - Party, Quantity, Rule, Product, Event, Transaction

### Portfolio Benefits

| Aspect | Before | After |
|--------|--------|-------|
| **Architecture** | Layered monolith | Event-Sourced modular system |
| **Patterns** | Clean Architecture | ES + CQRS + DDD + Archetypes |
| **Scalability** | Limited | High (stream-based) |
| **Audit Trail** | Basic | Full temporal history |
| **Domain Model** | Anemic | Rich with invariants |
| **Portfolio Value** | ⭐⭐ | ⭐⭐⭐⭐⭐ |

---

## Interview Talking Points

When discussing this project:

1. **"I evolved a student project into an Event-Sourced system"**
   - Shows initiative and continuous learning
   - Demonstrates architectural evolution skills

2. **"Implemented domain-specific event streams optimized for basketball"**
   - Deep domain understanding
   - Not generic e-commerce example

3. **"Applied enterprise archetype patterns from Arlow's book"**
   - Theoretical knowledge
   - Cross-domain thinking

4. **"Basketball rules are first-class objects, version-controlled"**
   - Testable business logic
   - Temporal correctness (NBA vs FIBA rules)

5. **"Full audit trail with temporal queries at any point in match history"**
   - Advanced Event Sourcing usage
   - Replay capabilities

---

## Blog Post Series Ideas

1. **"From Student Project to Portfolio Gold: Basketball League Evolution"**
   - Overview of the journey
   - Why this approach

2. **"Refactoring to Modular Monolith: Bounded Contexts in Practice"**
   - Phase 1 implementation
   - Domain events between modules

3. **"Event Sourcing Basketball Matches: Domain-Specific Streams"**
   - Phase 2 implementation
   - Projections and CQRS

4. **"Rule Engine in Basketball: Business Rules as First-Class Objects"**
   - Rule archetype implementation
   - Testing and versioning

5. **"Party and Quantity Archetypes in Event Sourcing"**
   - Player lifecycle management
   - Statistics as quantities

6. **"Deploying Event-Sourced System to Azure"**
   - Infrastructure as code
   - Production concerns

---

## Contributing

This is an evolutionary architecture project. As you implement each phase:
1. Document decisions in Architecture Decision Records (ADRs)
2. Update this documentation
3. Create blog posts for each milestone
4. Share code on GitHub
5. Present at meetups or conferences

---

## Resources

### Books
- "Domain-Driven Design" by Eric Evans
- "Implementing Domain-Driven Design" by Vaughn Vernon
- "Enterprise Patterns and MDA" by Jim Arlow and Ila Neustadt
- "Versioning in an Event Sourced System" by Greg Young

### Reference Projects
- eShopOnContainers (Microsoft)
- Modular Monolith with DDD (Kamil Grzybek)
- Practical Event Sourcing (Derek Comartin)

### Technologies
- [Marten](https://martendb.io/) - Event Store for .NET on PostgreSQL
- [EventStoreDB](https://www.eventstore.com/) - Purpose-built event store
- [MediatR](https://github.com/jbogard/MediatR) - CQRS/mediator pattern

---

## Status

**Current Phase:** Planning & Documentation ✅
**Next Phase:** Modular Monolith Implementation

**Timeline:**
- Phase 1 (Modular Monolith): 2-3 months
- Phase 2 (Event Sourcing): 2-3 months
- Phase 3 (Archetype Patterns): 1-2 months
- Phase 4 (Cloud-Native): Ongoing

**Total:** 4-6 months for complete evolution

---

## Contact & Portfolio

This project demonstrates:
- ✅ Senior-level architectural thinking
- ✅ Event Sourcing and CQRS expertise
- ✅ Domain-Driven Design application
- ✅ Enterprise pattern knowledge
- ✅ Evolution and refactoring skills
- ✅ Documentation and communication

Perfect for senior .NET developer positions focusing on distributed systems, Event Sourcing, and complex domain modeling.

---

**Last Updated:** January 2026
**Version:** 1.0
**Author:** Basketball League Evolution Project

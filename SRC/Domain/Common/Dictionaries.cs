using System.ComponentModel;

namespace BasketballLeague.Domain.Common
{
    public enum Postition
    {
        [Description("Point Guard")]
        PG = 1,
        [Description("Shooting Guard")]
        SG = 2,
        [Description("Small Forward")]
        SF = 3,
        [Description("Power Forward")]
        PF = 4,
        [Description("Center")]
        C = 5
    }

    public enum ReboundType
    {
        [Description("Offensive Rebound Player")]
        PLAYER_OFF = 1,
        [Description("Defensive Rebound Player")]
        PLAYER_DEF = 2,
        [Description("Offensive Rebound Team")]
        TEAM_OFF = 3,
        [Description("Defensive Rebound Team")]
        TEAM_DEF = 4
    }

    public enum TurnoverType
    {
        [Description("Bad Pass")]
        BAD_PASS = 1,
        [Description("Ball Handling")]
        BALL_HANDLING = 2,
        [Description("Travel")]
        TRAVEL = 3,
        [Description("Double Dribble")]
        DOUBLE_DRIBBLE = 4,
        [Description("Out Of Bounds")]
        OUT_OF_BOUNDS = 5,
        [Description("Back Court")]
        BACK_COURT = 6,
        [Description("Seconds 3")]
        SECONDS_3 = 7,
        [Description("Seconds 5")]
        SECONDS_5 = 8,
        [Description("Offensive Goal Tending")]
        OFFENSIVE_GOAL_TENDING = 9,
        [Description("Other")]
        OTHER = 10
    }

    public enum ShotType
    {
        [Description("Jumpshot")]
        JUMPSHOT = 1,
        [Description("Floating Jump Shot")]
        FLOATING_JUMP_SHOT = 2,
        [Description("Turnaround Jump Shot")]
        TURNAROUND_JUMP_SHOT = 3,
        [Description("Step Back Jump Shot")]
        STEP_BACK_JUMP_SHOT = 4,
        [Description("Pull Up Jump Shot")]
        PULL_UP_JUMP_SHOT = 5,
        [Description("Layoup")]
        LAYOUP = 6,
        [Description("Driving Layoup")]
        DRIVING_LAYOUP = 7,
        [Description("Dunk")]
        DUNK = 8,
        [Description("Alley Oop")]
        ALLEY_OOP = 9,
        [Description("Hook Shot")]
        HOOK_SHOT = 10
    }

    public enum FoulType
    {
        [Description("Personal")]
        PERSONAL = 1,
        [Description("Shooting")]
        SHOOTING = 2,
        [Description("Offensive")]
        OFFENSIVE = 3,
        [Description("Technical")]
        TECHNICAL = 4,
        [Description("Double")]
        DOUBLE = 5,
        [Description("Unsportsmanlike")]
        UNSPORTSMANLIKE = 6,
        [Description("Unsportsmalike Shooting")]
        UNSPORTSMALIKE_SHOOTING = 7,
        [Description("Disqualifying")]
        DISQUALIFYING = 8,
        [Description("Disqualifying Shooting")]
        DISQUALIFYING_SHOOTING = 9,
        [Description("Coach Disqualifying")]
        COACH_DISQUALIFYING = 10,
        [Description("Coach Technical")]
        COACH_TECHNICAL = 11,
        [Description("Bench Technical")]
        BENCH_TECHNICAL = 12
    }

    public enum JumpBallType
    {
        [Description("Lodged Ball")]
        LODGED_BALL = 1,
        [Description("Contested Rebound")]
        CONTESTED_REBOUND = 2,
        [Description("Out Of Bounds Rebound")]
        OUT_OF_BOUNDS_REBOUND = 3,
        [Description("Held ball")]
        HELD_BALL = 4,
        [Description("Out Of Bounds Loose Ball")]
        OUT_OF_BOUNDS_LOOSE_BALL = 5
    }

    public enum IncidentType
    {
        [Description("Foul")]
        FOUL = 1,
        [Description("Rebound")]
        REBOUND = 2,
        [Description("Shot")]
        SHOT = 3,
        [Description("Turnover")]
        TURNOVER = 4,
        [Description("Substitution")]
        SUBSTITUTION = 5,
        [Description("Timeout")]
        TIMEOUT = 6,
        [Description("Jump Ball")]
        JUMP_BALL = 7
    }

}

using System.ComponentModel;

namespace BasketballLeague.Domain.Common
{
    public enum Postition
    {
        [Description("Point Guard")]
        PG,
        [Description("Shooting Guard")]
        SG,
        [Description("Small Forward")]
        SF,
        [Description("Power Forward")]
        PF,
        [Description("Center")]
        C
    }

    public enum TurnoverType
    {
        [Description("Bad Pass")]
        BAD_PASS,
        [Description("Ball Handling")]
        BALL_HANDLING,
        [Description("Travel")]
        TRAVEL,
        [Description("Double Dribble")]
        DOUBLE_DRIBBLE,
        [Description("Out Of Bounds")]
        OUT_OF_BOUNDS,
        [Description("Back Court")]
        BACK_COURT,
        [Description("Seconds 3")]
        SECONDS_3,
        [Description("Seconds 5")]
        SECONDS_5,
        [Description("Offensive Goal Tending")]
        OFFENSIVE_GOAL_TENDING,
        [Description("Other")]
        OTHER
    }

    public enum ShotType
    {
        [Description("Jumpshot")]
        JUMPSHOT,
        [Description("Floating Jump Shot")]
        FLOATING_JUMP_SHOT,
        [Description("Turnaround Jump Shot")]
        TURNAROUND_JUMP_SHOT,
        [Description("Step Back Jump Shot")]
        STEP_BACK_JUMP_SHOT,
        [Description("Pull Up Jump Shot")]
        PULL_UP_JUMP_SHOT,
        [Description("Layoup")]
        LAYOUP,
        [Description("Driving Layoup")]
        DRIVING_LAYOUP,
        [Description("Dunk")]
        DUNK,
        [Description("Alley Oop")]
        ALLEY_OOP,
        [Description("Hook Shot")]
        HOOK_SHOT
    }

    public enum PlayerFoulType
    {
        [Description("Personal")]
        PERSONAL,
        [Description("Shooting")]
        SHOOTING,
        [Description("Offensive")]
        OFFENSIVE,
        [Description("Technical")]
        TECHNICAL,
        [Description("Double")]
        DOUBLE,
        [Description("Unsportsmanlike")]
        UNSPORTSMANLIKE,
        [Description("Unsportsmalike Shooting")]
        UNSPORTSMALIKE_SHOOTING,
        [Description("Disqualifying")]
        DISQUALIFYING,
        [Description("Disqualifying Shooting")]
        DISQUALIFYING_SHOOTING,
    }

    public enum CoachFoulType
    {
        [Description("Coach Disqualifying")]
        DISQUALIFYING,
        [Description("Coach Technical")]
        TECHNICAL,
    }

    public enum JumpBallType
    {
        [Description("Lodged Ball")]
        LODGED_BALL,
        [Description("Contested Rebound")]
        CONTESTED_REBOUND,
        [Description("Out Of Bounds Rebound")]
        OUT_OF_BOUNDS_REBOUND,
        [Description("Held ball")]
        HELD_BALL,
        [Description("Out Of Bounds Loose Ball")]
        OUT_OF_BOUNDS_LOOSE_BALL
    }
}

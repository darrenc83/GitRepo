using System;
using System.ComponentModel;
using YPMMS.Shared.Core.Extensions;

namespace YPMMS.Shared.Core.Enums
{
    /// <summary>
    /// Custom attribute used to mark status values that represent an error
    /// </summary>
    internal sealed class ErrorAttribute : Attribute { }

    /// <summary>
    /// Custom attribute used to mark status values that represent a full machine status
    /// </summary>
    internal sealed class FullAttribute : Attribute { }

    /// <summary>
    /// Status values reported by machines
    /// </summary>
    public enum SystemStatus
    {
        [Description("Initialising")]
        Initialising,

        [Description("Ready")]
        Ready,

        [Description("Bill Jam")]
        [Error]
        BillJam,

        [Description("Coin Jam")]
        [Error]
        CoinJam,

        [Description("TicketsLow")]
        TicketsLow,

        [Description("Tickets Jam")]
        [Error]
        TicketsJam,

        [Description("Power Removed")]
        PowerRemoved,

        [Description("Coins Full")]
        [Full]
        CoinsFull,

        [Description("Bills Full")]
        [Full]
        BillsFull,

        [Description("Coin Mechanism Error")]
        [Error]
        CoinMechError,

        [Description("Calibration Error")]
        [Error]
        CalibrationError,

        [Description("Offline")]
        Offline,

        [Description("Stacker Full")]
        [Full]
        StackerFull,

        [Description("Payout Out Of Service")]
        [Error]
        PayoutOutofService,

        [Description("Payout Jam")]
        [Error]
        PayoutJam,

        [Description("Bill Fraud Detected")]
        BillFraudDetected,

        [Description("Coin Fraud Detected")]
        CoinFraudDetected,

        [Description("System Movement Detected")]
        SystemMovementDetected,

        [Description("Security Loop Break")]
        SecurityLoopBreak,

        [Description("Bill System Updated")]
        BillSystemUpdated,

        [Description("Coin System Updated")]
        CoinSystemUpdated,

        [Description("LC System Updated")]
        LcSystemUpdated,

        [Description("Bill System Software Error")]
        [Error]
        BillSystemSoftwareError,

        [Description("Coin System Software Error")]
        [Error]
        CoinSystemSoftwareError,

        [Description("LC System Software Error")]
        [Error]
        LcSystemSoftwareError,

        [Description("Bill Cashbox Removed")]
        BillCashboxRemoved,

        [Description("Bill Cashbox Replaced")]
        BillCashboxReplaced,

        [Description("Bill Cashbox Unlocked")]
        BillCashboxUnlocked,

        [Description("TEBS Code Error")]
        [Error]
        TebsCodeError,

        [Description("Bill System Inhibited")]
        BillSystemInhibited,

        [Description("Coin System Inhibited")]
        CoinSystemInhibited,

        [Description("Bill Rejected")]
        BillRejected,

        [Description("Cash Credit")]
        CashCredit,

        [Description("Bill Path Open")]
        BillPathOpen,

        [Description("Bill Path Jam")]
        [Error]
        BillPathJam,

        [Description("Bill Stacker Jam")]
        [Error]
        BillStackerJam,

        [Description("Bill System Disabled")]
        BillSystemDisabled,

        [Description("Coin System Disabled")]
        CoinSystemDisabled,

        [Description("Coin System Reset")]
        CoinSystemReset,

        [Description("Bill System Reset")]
        BillSystemReset,

        [Description("Adjustment Credit")]
        AdjustmentCredit,

        [Description("Socket Hold")]
        SocketHold,

        [Description("TEBS Unlock enabled")]
        BillUnlockEnabled,

        [Description("TEBS Unlock disabled")]
        BillUnlockDisabled,

        [Description("System Online")]
        Online,

        [Description("Power Restored")]
        PowerRestored,

        [Description("System Door Opened")]
        DoorOpen,
        [Description("System Door Closed")]
        DoorClosed,

        [Description("Bill validitor disconnected")]
        BillValidatorDisconnected = 60,

    }

    /// <summary>
    /// Convenience methods for SystemStatus
    /// </summary>
    public static class SystemStatusExtensions
    {
        /// <summary>
        /// Does this status represent a full machine?
        /// </summary>
        public static bool IsFull(this SystemStatus status)
        {
            return status.HasAttribute<FullAttribute>();
        }

        /// <summary>
        /// Does this status represent an error in the machine?
        /// </summary>
        public static bool IsError(this SystemStatus status)
        {
            return status.HasAttribute<ErrorAttribute>();
        }
    }
}
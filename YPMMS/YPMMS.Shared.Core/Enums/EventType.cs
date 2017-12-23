using System.ComponentModel;

namespace YPMMS.Shared.Core.Enums
{
    /// <summary>
    /// Types of event that can be raised by the system and displayed in the system notifications UI
    /// </summary>
    public enum EventType
    {
        [Description("Machine Status Added")]
        MachineStatusAdded,
        [Description("Machine Status Removed")]
        MachineStatusRemoved,
        [Description("Machine Added")]
        MachineAdded,
        [Description("Machine Removed")]
        MachineRemoved,
        [Description("Machine Details Edited")]
        MachineDetailsEdited,
        [Description("Machine Update Available")]
        MachineUpdateAvailable,
        [Description("Machine Collected")]
        MachineCollected,
        [Description("Machine Refilled")]
        MachineRefilled,
        [Description("Machine Assigned To Site")]
        MachineAssignedToSite,

        [Description("Site Added")]
        SiteAdded,
        [Description("Site Removed")]
        SiteRemoved,

        [Description("Collector Added")]
        CollectorAdded,
        [Description("Collector Assigned To Machine")]
        CollectorAssignedToMachine,

        [Description("Bill Cashbox Full")]
        BillsFull,
        [Description("Bill System Jam")]
        BillJam,
        [Description("Coin System Jam")]
        CoinJam,
        [Description("LC Power removed")]
        PowerRemoved,
        [Description("Coin Cashbox Full")]
        CoinsFull,
        [Description("System Offline")]
        Offline,
        [Description("Bill Cashbox Full")]
        StackerFull,
        [Description("Movement Detected")]
        SystemMovementDetected,
        [Description("Security Loop Break")]
        SecurityLoopBreak,
        [Description("Bill System Update")]
        BillSystemUpdated,
        [Description("Coin System Update")]
        CoinSystemUpdated,
        [Description("LC System Update")]
        LcSystemUpdated,
        [Description("Bill System Software Error")]
        BillSystemSoftwareError,
        [Description("Coinm System Software Error")]
        CoinSystemSoftwareError,
        [Description("LC System Software Error")]
        LcSystemSoftwareError,
        [Description("Bill Path Jam")]
        BillPathJam,
        [Description("Bill Stack Jam")]
        BillStackerJam,
        [Description("System Online")]
        Online,
        [Description("LC Power Restored")]
        PowerRestored,

        [Description("Tickets Level low")]
        TicketsLow,
        [Description("Tickets jammed")]
        TicketsJam,
        [Description("Coin Mech Error")]
        CoinMechError,
        [Description("Calibration error")]
        CalibrationError,
        [Description("Payout out of service")]
        PayoutOutofService,
        [Description("Payout jam")]
        PayoutJam,
        [Description("Bill fraud detected")]
        BillFraudDetected,
        [Description("Coin fraud detected")]
        CoinFraudDetected,
        [Description("Bill system inhibited")]
        BillSystemInhibited,
        [Description("Coin system inhibited")]
        CoinSystemInhibited,
        [Description("Coin system reset")]
        CoinSystemReset,
        [Description("Bill system reset")]
        BillSystemReset,

    }
}
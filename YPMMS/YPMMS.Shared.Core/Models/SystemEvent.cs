using System;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Extensions;

namespace YPMMS.Shared.Core.Models
{
    public sealed class SystemEvent
    {
        public long Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventType EventType { get; set; }
        public SystemStatus? MachineStatus { get; set; }
        public long? MachineId { get; set; }
        public string MachineName { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public long? CollectorId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public long? CollectionEventId { get; set; }
        public decimal? CollectionTotalValue { get; set; }
        public bool IsNew { get; set; }

        #region Calculated properties

        public string EventTypeDescription
        {
            get
            {
                switch (EventType)
                {
                    case EventType.MachineStatusAdded:
                        return
                            MachineStatusOffline ? "Machine Offline" :
                            MachineStatusError ? "Error" :
                            MachineStatusFull ? "Warning" :
                            "Alert";
                    case EventType.MachineStatusRemoved:
                        return
                            MachineStatusOffline ? "Machine Online" :
                            MachineStatusError ? "Error resolved" :
                            MachineStatusFull ? "Warning resolved" :
                            "Alert resolved";
                    default:
                        return EventType.GetDescription();
                }
            }
        }

        public string EventCaption
        {
            get
            {
                switch (EventType)
                {
                    case EventType.MachineAdded:
                    case EventType.MachineRemoved:
                    case EventType.MachineCollected:
                    case EventType.MachineRefilled:
                    case EventType.MachineDetailsEdited:
                    case EventType.MachineUpdateAvailable:
                        return MachineName;

                    case EventType.BillsFull:
                    case EventType.BillJam:
                    case EventType.CoinJam:
                    case EventType.PowerRemoved:
                    case EventType.CoinsFull:
                    case EventType.Offline:
                    case EventType.StackerFull:
                    case EventType.SystemMovementDetected:
                    case EventType.SecurityLoopBreak:
                    case EventType.BillSystemUpdated:
                    case EventType.CoinSystemUpdated:
                    case EventType.LcSystemUpdated:
                    case EventType.BillSystemSoftwareError:
                    case EventType.CoinSystemSoftwareError:
                    case EventType.LcSystemSoftwareError:
                    case EventType.BillPathJam:
                    case EventType.BillStackerJam:
                    case EventType.Online:
                    case EventType.PowerRestored:


                    case EventType.TicketsLow:

                    case EventType.TicketsJam:

                    case EventType.CoinMechError:

                    case EventType.CalibrationError:

                    case EventType.PayoutOutofService:

                    case EventType.PayoutJam:

                    case EventType.BillFraudDetected:

                    case EventType.CoinFraudDetected:

                    case EventType.BillSystemInhibited:

                    case EventType.CoinSystemInhibited:

                    case EventType.CoinSystemReset:

                    case EventType.BillSystemReset:


                    case EventType.MachineAssignedToSite:
                        return $"{MachineName} at {SiteName}";
                    case EventType.MachineStatusAdded:
                    case EventType.MachineStatusRemoved:
                        // For machine offline/online we just show the machine name.
                        // For any other status, we show the machine status and machine name.
                        if (MachineStatus != null && !MachineStatusOffline)
                        {
                            return $"{MachineStatus.GetDescription()} - {MachineName}";
                        }
                        return MachineName;
                    case EventType.SiteAdded:
                    case EventType.SiteRemoved:
                        return SiteName;
                    case EventType.CollectorAdded:
                    case EventType.CollectorAssignedToMachine:
                        return UserName;
                    default:
                        return string.Empty;
                }
            }
        }

        public bool MachineStatusFull => MachineStatus?.IsFull() ?? false;
        public bool MachineStatusError => MachineStatus?.IsError() ?? false;
        public bool MachineStatusOffline => (MachineStatus != null && MachineStatus == SystemStatus.Offline);

        #endregion
    }
}

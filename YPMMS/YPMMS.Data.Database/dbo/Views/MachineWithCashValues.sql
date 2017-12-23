CREATE VIEW dbo.[MachineWithCashValues] AS

SELECT
    M.Id,
    M.Name,
    M.Currency,
    M.SystemType,
    M.[Status],
    M.UpdateAvailable,
    M.SiteId,
    M.LastConnectedTime,
    M.LastCollectionTime,
    M.LastCollectionAmount,
    M.[Timestamp],
    ISNULL(SUM(MD.Value * MD.CashboxLevel), 0) AS 'CashboxValue',
    ISNULL(SUM(MD.Value * MD.StoredLevel), 0) AS 'StoredValue',
    ISNULL(SUM(MD.Value * MD.FloatLevel), 0) AS 'FloatValue',
    ISNULL((SUM(MD.Value * MD.CashboxLevel) + sum(MD.Value * MD.StoredLevel)) - sum(MD.Value * MD.FloatLevel), 0) AS 'CollectableValue'
FROM Machine M
LEFT JOIN MachineDenomination MD on M.Id = MD.MachineId
GROUP BY
    M.Id,
    M.Name,
    M.Currency,
    M.SystemType,
    M.[Status],
    M.UpdateAvailable,
    M.SiteId,
    M.LastConnectedTime,
    M.LastCollectionTime,
    M.LastCollectionAmount,
    M.[Timestamp]
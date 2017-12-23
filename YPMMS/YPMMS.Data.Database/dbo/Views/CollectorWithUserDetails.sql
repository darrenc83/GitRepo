
CREATE VIEW [dbo].[CollectorWithUserDetails] AS
SELECT
  C.Id,
  C.UserId,
  C.KeyId,
  ANU.UserName,
  ANU.Email,
  ANU.Name,
  ANU.PhoneNumber,
  C.Country,
  C.CollectStatus,
  C.MachineId,
  C.LastUpdate,
  COUNT(CTM.MachineId) AS 'AssignedMachines'
FROM Collector C
JOIN AspNetUsers ANU ON C.UserId = ANU.Id
LEFT JOIN CollectorToMachine CTM ON C.Id = CTM.CollectorId
GROUP BY
  C.Id,
  C.UserId,
  C.KeyId,
  ANU.UserName,
  ANU.Email,
  ANU.Name,
  ANU.PhoneNumber,
  C.Country,
  C.CollectStatus,
  C.MachineId,
  C.LastUpdate
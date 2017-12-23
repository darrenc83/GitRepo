CREATE PROCEDURE [dbo].[GetStatusEventPagedData]
(
    @pageNum INT,   
    @pageSize INT    
)
AS
BEGIN
    WITH Paging AS
      (
        SELECT Id, MachineId, EventType, [Timestamp], ROW_NUMBER() OVER (ORDER BY  [Timestamp] desc) AS RowNumber FROM [dbo].[StatusEvent] WITH(NOLOCK)
      )
      SELECT RowNumber, Id, MachineId, EventType, [Timestamp] FROM Paging  WHERE RowNumber > @pageNum  AND RowNumber < @pageSize  
END
CREATE PROCEDURE [dbo].[AllAccumByMONTH]
  @Year int
AS
BEGIN
 SET NOCOUNT ON;
 create table #months( month int, accum money )
 
declare @row1 money ,@row2 money ,@row3 money,@row4 money  ,@row5 money  ,@row6 money,@row7 money  , @row8 money ,
@row9 money  ,@row10 money ,@row11 money, @row12 money, @row13 money  ,@row14 money  ,@row15 money  , 
@row16 money ,@row17 money ,@row18 money, @row19 money, @row20 money  ,@row21 money  ,@row22 money  , 
@row23 money ,@row24 money ,@row25 money, @row26 money, @row27 money  ,@row28 money , 
@row29 money ,@row30 money ,@row31 money 
 
EXEC [dbo].[sumMachineMONTH]  @Year, 1, @row1 OUTPUT--SELECT @row1 as ' tot'
exec [dbo].[sumMachineMONTH]  @Year, 2, @row2 OUTPUT--SELECT @row2 as ' tot'
exec [dbo].[sumMachineMONTH]  @Year, 3, @row3 OUTPUT--SELECT @row3 as ' tot'
exec [dbo].[sumMachineMONTH]  @Year, 4, @row4 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 5, @row5 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 6, @row6 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 7, @row7 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 8, @row8 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 9, @row9 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 10, @row10 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 11, @row11 OUTPUT
exec [dbo].[sumMachineMONTH]  @Year, 12, @row12 OUTPUT
  
insert into #months(month, accum) values(1,0),(2,0),(3,0),(4,0),(5,0),(6,0),(7,0),(8,0),(9,0),(10,0),(11,0),(12,0) 
 
update #months set accum =  @row1 where month = 1
update #months set accum =  @row2 where month = 2
update #months set accum =  @row3 where month = 3
update #months set accum =  @row4 where month = 4
update #months set accum =  @row5 where month = 5
update #months set accum =  @row6 where month = 6
update #months set accum =  @row7 where month = 7
update #months set accum =  @row8 where month = 8
update #months set accum =  @row9 where month = 9
update #months set accum =  @row10 where month = 10
update #months set accum =  @row11 where month = 11
update #months set accum =  @row12 where month = 12
 
select * from  #months
drop table #months
  
END
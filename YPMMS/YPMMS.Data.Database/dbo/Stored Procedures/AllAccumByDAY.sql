
CREATE PROCEDURE [dbo].[AllAccumByDAY]
  @Year int,
  @month int
AS
BEGIN
 	SET NOCOUNT ON;
create table #days( day int, accum money )

declare @row1 money ,@row2 money ,@row3 money,@row4 money  ,@row5 money  ,@row6 money,@row7 money  , @row8 money ,
@row9 money  ,@row10 money ,@row11 money, @row12 money, @row13 money  ,@row14 money  ,@row15 money  , 
@row16 money ,@row17 money ,@row18 money, @row19 money, @row20 money  ,@row21 money  ,@row22 money  , 
@row23 money ,@row24 money ,@row25 money, @row26 money, @row27 money  ,@row28 money , 
@row29 money ,@row30 money ,@row31 money 

EXEC [dbo].[sumMachineDAY]  @Year, @month, 1 , @row1 OUTPUT--SELECT @row1 as ' tot'
EXEC [dbo].[sumMachineDAY]  @Year, @month, 2 , @row2 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 3 , @row3 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 4 , @row4 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 5 , @row5 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 6 , @row6 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 7 , @row7 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 8 , @row8 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 9 , @row9 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 10 , @row10 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 11 , @row11 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 12 , @row12 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 13 , @row13 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 14 , @row14 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 15 , @row15 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 16 , @row16 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 17 , @row17 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 18 , @row18 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 19 , @row19 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 20 , @row20 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 21 , @row21 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 22 , @row22 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 23 , @row23 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 24 , @row24 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 25 , @row25 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 26 , @row26 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 27 , @row27 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 28 , @row28  OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 29 , @row29 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 30 , @row30 OUTPUT
EXEC [dbo].[sumMachineDAY]  @Year, @month, 31 , @row31 OUTPUT
 

insert into #days(day, accum) values(1,0), (2,0), (3,0), (4,0), (5,0),(6,0),(7,0),(8,0),(9,0),(10,0),(11,0),(12,0), (13,0)
,(14,0),(15,0),(16,0),(17,0),(18,0),(19,0),(20,0),(21,0),(22,0),(23,0),(24,0),(25,0),(26,0),(27,0),(28,0),(29,0),(30,0),(31,0)
 
update #days set accum =  @row1  where day = 1
update #days set accum =  @row2  where day = 2
update #days set accum =  @row3  where day = 3
update #days set accum =  @row4  where day = 4
update #days set accum =  @row5  where day = 5
update #days set accum =  @row6  where day = 6
update #days set accum =  @row7 where day = 7
update #days set accum =  @row8  where day = 8
update #days set accum =  @row9  where day = 9
update #days set accum =  @row10  where day = 10
update #days set accum =  @row11  where day = 11
update #days set accum =  @row12  where day = 12
update #days set accum =  @row13  where day = 13
update #days set accum =   @row14  where day = 14
update #days set accum =   @row15  where day = 15
update #days set accum =   @row16  where day = 16
update #days set accum =   @row17  where day = 17
update #days set accum =   @row18  where day = 18
update #days set accum =   @row19  where day = 19
update #days set accum =   @row20  where day = 20
update #days set accum =   @row21  where day = 21
update #days set accum =   @row22  where day = 22
update #days set accum =   @row23  where day = 23
update #days set accum =   @row24  where day = 24
update #days set accum =   @row25  where day = 25
update #days set accum =  @row26  where day = 26
update #days set accum =   @row27  where day = 27
update #days set accum =   @row28  where day = 28
update #days set accum =   @row29  where day = 29
update #days set accum =  @row30  where day = 30
update #days set accum =   @row31  where day = 31
 
select * from  #days

drop table #days




 
END
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[LogSocketConnection]
       @IpAddress                      VARCHAR(200)             , 
       @Data                           VARCHAR(MAX)             , 
       @IsValid                        TINYINT                 
AS 
BEGIN 
     SET NOCOUNT ON 

     INSERT INTO SocketConnection
          ( 
            IpAddress                   ,
            Data                     ,
            IsValid                  
          ) 
     VALUES 
          ( 
            @IpAddress                   ,
            @Data                     ,
            @IsValid                  
          ) 

END 




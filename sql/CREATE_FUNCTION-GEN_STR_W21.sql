-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		M.Tor
-- Create date: 02.12.2016
-- Description:	Generation string for SQL command
-- =============================================
CREATE FUNCTION GEN_STR_W21 
(
	-- Add the parameters for the function here
	@pWord varchar(1000),
	@pWNum int
)
RETURNS varchar(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(1000);
	DECLARE @T1 varchar(16), @T2 varchar(16);
	
	SET @T1 = 'T'+CAST(@pWNum-1 AS varchar(16));
	SET @T2 = 'T'+CAST(@pWNum AS varchar(16));
	
	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = ' INNER JOIN dbo.[SELECT_ADDROBJNAME]('''+@pWord + ''') AS '+@T2
		+' ON '+@T1+'.aRegCode = '+@T2+'.aRegCode AND '+@T1+'.aLevel < '+@T2+'.aLevel'
		+' AND dbo.FF_IS_MY_PARENT('+@T2+'.aGUID, '+@T1+'.aGUID) = 1';

	-- Return the result of the function
	RETURN @Result

END
GO


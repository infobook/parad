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
-- Description:	Generate SQL statment for join various address object
-- and find one.
-- =============================================
CREATE FUNCTION GEN_SQL_FIND_ADDROBJ 
(
	-- Add the parameters for the function here
	@SRCSTR varchar(1000)
)
RETURNS varchar(2000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @SQLCmd varchar(2000); 
	DECLARE @II int, @PP int
	DECLARE @CountWord int;

	SET @SQLCmd = 'SELECT * FROM dbo.[SELECT_FORMALNAME](''';
	SET @II = CHARINDEX(';',@SRCSTR,0);

	IF @II = 0
		SET @SQLCmd = @SQLCmd + @SRCSTR + ''') AS T1';
	ELSE
		SET @SQLCmd = @SQLCmd + SUBSTRING(@SRCSTR, 1, @II-1) + ''') AS T1';
			
	SET @CountWord = 1;

	WHILE @II != 0
	BEGIN
		SET @PP = @II+1;
		SET @II = CHARINDEX(';',@SRCSTR,@PP);
		SET @CountWord = @CountWord + 1;
		
		IF @II = 0
			SET @SQLCmd = @SQLCmd + dbo.GEN_STR_W21(SUBSTRING(@SRCSTR, @PP, LEN(@SRCSTR)-@PP+1), @CountWord);
		ELSE
			SET @SQLCmd = @SQLCmd + dbo.GEN_STR_W21(SUBSTRING(@SRCSTR, @PP, @II-@PP), @CountWord);
	END

	-- Return the result of the function
	RETURN @SQLCmd + ';'

END
GO


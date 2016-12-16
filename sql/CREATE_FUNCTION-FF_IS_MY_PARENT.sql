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
-- Create date: 30.11.2016
-- Description:	
-- =============================================
CREATE FUNCTION FF_IS_MY_PARENT 
(
	-- Add the parameters for the function here
	@pGUID varchar(36),
	@pGUIDParent varchar(36)
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result bit
	DECLARE @cGUID varchar(36)
	DECLARE @nGUID varchar(36)

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = 0
	SELECT @cGUID = @pGUID
	
	WHILE (@Result = 0) AND LEN(@cGUID) > 0
	BEGIN
		SELECT @nGUID = [PARENTGUID] FROM ADDROBJ WHERE [AOGUID] = @cGUID
		IF @nGUID = @pGUIDParent 
		BEGIN
			SELECT @Result = 1
		END
		SELECT @cGUID = @nGUID 
	END

	-- Return the result of the function
	RETURN @Result

END
GO


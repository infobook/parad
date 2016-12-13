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
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.FF_COMP 
(
	-- Add the parameters for the function here
	@param1 INT, 
	@param2 INT
)
RETURNS INT
AS
BEGIN
	-- Return the result of the function
	RETURN @param1 * @param2 * (@param1 - @param2)

END
GO


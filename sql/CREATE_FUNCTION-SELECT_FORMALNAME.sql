-- ================================================
-- Template generated from Template Explorer using:
-- Create Inline Function (New Menu).SQL
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
-- Modified: 20.12.2016
-- Description:	SELECT from ADDROBJ like param
-- =============================================
CREATE FUNCTION SELECT_ADDROBJNAME 
(	
	-- Add the parameters for the function here
	@fName varchar(120) 
--	,@p2 char
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT
		AO.[AOGUID] AS [aGUID]

		,AO.[LIVESTATUS] AS [aLive]
		,AO.[ACTSTATUS]  AS [aAct]
		,CASE WHEN AO.[AOLEVEL] < 10 THEN AO.[AOLEVEL]*10 ELSE AO.[AOLEVEL] END AS [aLevel]

		,AO.[SHORTNAME]  AS [aSName]
		,AO.[OFFNAME] AS [aFName]

		,CAST(AO.[REGIONCODE] AS INT) AS [aRegCode] 
		,CAST(AO.[AREACODE] AS INT) AS [aAreaCode]	
		,CAST(AO.[CITYCODE] AS INT) AS [aCityCode]	
		,CAST(AO.[PLACECODE] AS INT) AS [aPlaceCode]	 
	--	,[PLANCODE] AS [aPlanCode]
		,CAST(AO.[STREETCODE] AS INT) AS [aStreetCode]	
	FROM
		[ADDROBJ] AS AO 
		INNER JOIN
		(SELECT AOGUID, MAX(LIVESTATUS) AS aLive, MAX(ACTSTATUS) AS aAct  
			FROM [ADDROBJ] WHERE [OFFNAME] LIKE @fName
			GROUP BY AOGUID) AS AO2 
		ON AO.AOGUID=AO2.AOGUID AND AO.LIVESTATUS = AO2.aLive AND AO.ACTSTATUS=AO2.aAct
	WHERE
		[OFFNAME] LIKE @fName
)
GO

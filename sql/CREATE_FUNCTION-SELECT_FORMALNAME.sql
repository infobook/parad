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
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION SELECT_FORMALNAME 
(	
	-- Add the parameters for the function here
	@fName varchar(120) 
--	,<@param2, sysname, @p2> <Data_Type_For_Param2, , char>
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
		,AO.[FORMALNAME] AS [aFName]

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
			FROM [ADDROBJ] WHERE [FORMALNAME] LIKE @fName
			GROUP BY AOGUID) AS AO2 
		ON AO.AOGUID=AO2.AOGUID AND AO.LIVESTATUS = AO2.aLive AND AO.ACTSTATUS=AO2.aAct
	WHERE
		[FORMALNAME] LIKE @fName
)
GO

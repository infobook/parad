--DECLARE @WT1 TABLE(
--	_guid  varchar(36)
--	,_type varchar(10)
--	,_fname varchar(120)
--	,_regCode varchar(2)
--	,_areaCode varchar(3)
--	,_cityCode varchar(3)
--	,_placeCode varchar(3)
--	,_streetCode varchar(4)
--)

--INSERT @WT1 

SELECT
	[AOGUID] AS [aGUID]
	,[PARENTGUID] AS [aPARENT]
	,[SHORTNAME]  AS [aSName]
    ,[FORMALNAME] AS [aFName]

--	,[ACTSTATUS]  AS [aStatus]
--	,[LIVESTATUS] AS [cLive]
--	,[AOLEVEL] AS [aLevel]
	
     ,[REGIONCODE] AS [aRegCode] 
     ,[AREACODE] AS [aAreaCode]	
     ,[CITYCODE] AS [aCityCode]	
     ,[PLACECODE] AS [aPlaceCode]	 
--	 ,[PLAINCODE] AS [aPlainCode]
     ,[STREETCODE] AS [aStreetCode]	

--     ,[CTARCODE]	
--     ,[EXTRCODE]	
--     ,[SEXTCODE]

FROM
	[ADDROBJ]
WHERE
	[FORMALNAME] LIKE 'Маршала Рыбалко'

--SELECT * FROM @WT1

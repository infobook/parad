SELECT top 1000 
     [CODE]
 	
     ,[REGIONCODE]
     ,[AUTOCODE]
     ,[AREACODE]
     ,[CITYCODE]
     ,[CTARCODE]
     ,[PLACECODE]
     ,[STREETCODE]
     ,[EXTRCODE]
     ,[SEXTCODE]
     
     ,[SHORTNAME]
     ,[FORMALNAME]
--     ,[OFFNAME]
     
--      ,[AOGUID]
--      ,[AOID]
      ,[AOLEVEL]
      ,[CENTSTATUS]
	  ,[ACTSTATUS]
      ,[CURRSTATUS]
 --     ,[ENDDATE]
 --     ,[IFNSFL]
 --     ,[IFNSUL]
 --     ,[NEXTID]
--      ,[OKATO]
--      ,[OKTMO]
      ,[OPERSTATUS]
--      ,[PARENTGUID]
 --     ,[PLAINCODE]
 --     ,[POSTALCODE]
 --     ,[PREVID]
 --     ,[STARTDATE]
 --     ,[TERRIFNSFL]
 --     ,[TERRIFNSUL]
 --     ,[UPDATEDATE]
 --     ,[LIVESTATUS]
 --     ,[NORMDOC]
  FROM [KB].[FIAS].[ADDROBJ]
  WHERE
	--[ACTSTATUS] = 0
	[AOLEVEL] = 1 
--  [FORMALNAME] LIKE 'Маршала Жукова'
--	AND [FORMALNAME] LIKE 'Москва'
--
ORDER BY 
	[REGIONCODE]

--SELECT COUNT(*) FROM [FIAS].[ADDROBJ]

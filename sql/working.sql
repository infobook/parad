--SELECT 
--	* 
--FROM 
--	dbo.[SELECT_FORMALNAME]('��������') AS T1
--	INNER JOIN dbo.[SELECT_FORMALNAME]('�����') AS T2
--		ON T1.aRegCode = T2.aRegCode 
--		AND T1.aLevel < T2.aLevel 
--		AND dbo.FF_COMP(T1.aAreaCode,T2.aAreaCode) = 0
--		AND dbo.FF_COMP(T1.aCityCode,T2.aCityCode) = 0
--	INNER JOIN dbo.[SELECT_FORMALNAME]('������� ������') AS T3
--		ON T2.aRegCode = T3.aRegCode 
--		AND T2.aLevel < T3.aLevel
--		AND dbo.FF_COMP(T2.aAreaCode,T3.aAreaCode) = 0
--		AND dbo.FF_COMP(T2.aCityCode,T3.aCityCode) = 0
/*
SELECT DISTINCT
	* 
FROM 
	dbo.[SELECT_FORMALNAME]('��������') AS T1
	INNER JOIN dbo.[SELECT_FORMALNAME]('�������������') AS T2
		ON T1.aRegCode = T2.aRegCode 
		AND T1.aLevel < T2.aLevel
		AND dbo.FF_IS_MY_PARENT(T2.aGUID, T1.aGUID) = 1 
	INNER JOIN dbo.[SELECT_FORMALNAME]('���������') AS T3
		ON T2.aRegCode = T3.aRegCode 
		AND T2.aLevel < T3.aLevel
		AND dbo.FF_IS_MY_PARENT(T3.aGUID, T2.aGUID) = 1 
*/
--SELECT * FROM ADDROBJ WHERE PARENTGUID is null 	
--SELECT * FROM dbo.[SELECT_FORMALNAME]('�������������')

DECLARE @SQLStm varchar(2000);
--SET @SQLStm = dbo.GEN_SQL_FIND_ADDROBJ('��������;����;������');
--SET @SQLStm = dbo.GEN_SQL_FIND_ADDROBJ('��������;��������;������');
--SET @SQLStm = dbo.GEN_SQL_FIND_ADDROBJ('��������;�������������;���������');
SET @SQLStm = dbo.GEN_SQL_FIND_ADDROBJ('������;%�������');
--PRINT @SQLStm
--PRINT '----------------------------'
EXEC (@SQLStm);

--SELECT * FROM dbo.[SELECT_FORMALNAME]('����')

DECLARE	@fName varchar(120) 
SET @fName = '%��������%'
--SET @fName = '������� ��������'
--SELECT * FROM dbo.SELECT_FORMALNAME(@fName)
--SELECT * FROM ADDROBJ WHERE FORMALNAME LIKE @fName AND regioncode='77' AND LIVESTATUS=1	

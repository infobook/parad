/*
SELECT 
	* 
FROM 
	dbo.[SELECT_FORMALNAME]('тверская') AS T1
	INNER JOIN dbo.[SELECT_FORMALNAME]('вышневолоцкий') AS T2
		ON T1.aRegCode = T2.aRegCode 
		AND T1.aLevel < T2.aLevel
		AND dbo.FF_IS_MY_PARENT(T2.aGUID, T1.aGUID) = 1 
	INNER JOIN dbo.[SELECT_FORMALNAME]('деревково') AS T3
		ON T2.aRegCode = T3.aRegCode 
		AND T2.aLevel < T3.aLevel
		AND dbo.FF_IS_MY_PARENT(T3.aGUID, T2.aGUID) = 1 
*/

DECLARE @SRCSTR varchar(1000);
DECLARE @SQLCmd varchar(1000); 
DECLARE @II int, @PP int
DECLARE @CountWord int;

SET @SQLCmd = 'SELECT * FROM dbo.[SELECT_FORMALNAME](''';
SET @SRCSTR = 'тверская;вышневолоцкий;деревково';
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

SET @SQLCmd = @SQLCmd + ';'

------------------------------------------------------------------------------

PRINT @SQLCmd
PRINT '----------------------------'
EXEC (@SQLCmd)
--EXEC sp_executeSQL @SQLCmd, ''
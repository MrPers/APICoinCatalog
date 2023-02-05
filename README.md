# APICoinCatalog - my learning project

 UICoin is a backend addon to APICoinCatalog

## Applications use:
Mailkit, Tests, Newtonsoft.Json

## Add in SQL:

CREATE FUNCTION dbo.GetCoins
(
    @id INT,
    @stepTime INT
)
RETURNS @res TABLE
(
	Id INT,
	CoinId INT,
	[Time] DATETIME2(7),
	VolumeTraded REAL,
	Prices REAL
)
AS
BEGIN
	DECLARE @startDate datetime2, @endDate datetime2;

	SELECT @startDate = MIN([Time]) FROM CoinRate
		WHERE [CoinId] = @id

	SELECT @endDate = MAX([Time]) FROM CoinRate
		WHERE [CoinId] = @id

	DECLARE @startTime datetime2 = @startDate;
	DECLARE @endTime datetime2 = DATEADD(hour, @stepTime, @startTime);
	DECLARE @number INT = 0;
	DECLARE @testTime datetime2;


	WHILE @endTime <= @endDate
	BEGIN
		SET @startTime = DATEADD(hour, @stepTime, @startTime);
		SET @endTime = DATEADD(hour, @stepTime, @endTime);
		SELECT @testTime = MIN([Time]) FROM CoinRate
			WHERE [CoinId] = @id
			AND [Time] BETWEEN @startTime AND @endTime

		IF (@testTime IS NOT NULL)
		BEGIN
			SET @number = @number + 1;
			INSERT INTO @res
			SELECT 
				@number,
				@id,
				MIN([Time]),
				AVG([VolumeTraded]),
				AVG([Prices])   
			FROM [MailGraphAnalysisDb].[dbo].[CoinRate]
			WHERE [CoinId] = @id
			AND [Time] BETWEEN @startTime AND @endTime
			END 
		END RETURN
END

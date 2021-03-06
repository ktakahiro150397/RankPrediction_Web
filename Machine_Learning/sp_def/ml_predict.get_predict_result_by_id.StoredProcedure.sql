USE [RankPrediction]
GO
/****** Object:  StoredProcedure [ml_predict].[get_predict_result_by_id]    Script Date: 8/25/2021 3:05:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [ml_predict].[get_predict_result_by_id]
	@dataId int
AS
BEGIN

	Declare @season_id int
	Declare @isInputDataHasMatchCount bit
	
	-- Obtain season_id
	SET @season_id = (
	SELECT
		season_id
	FROM ml_predict.prediction_data
	WHERE
		Id = @dataId
	)

	-- Check does data has match counts
	SET @isInputDataHasMatchCount = (
	SELECT
		CASE
			WHEN match_counts <> -1 THEN 'TRUE'
			ELSE 'FALSE'
		END AS isInputDataHasMatchCount
	FROM ml_predict.prediction_data
	WHERE
		Id = @dataId
	)
	--Generate prediction model
	EXEC ml_predict.call_generate_rank_py_model_by_id @dataId,@season_id,@isInputDataHasMatchCount

	--Temporary table for storing prediction result
	CREATE TABLE #rank_predict_result_temp(
		rank_id int NOT NULL
	)

	--Insert the results of the predictions for test set into a temp table
	INSERT INTO #rank_predict_result_temp
	EXEC ml_predict.py_predict_rank @dataId,@season_id,@isInputDataHasMatchCount

	--Insert prediction result into a table by using temp table set.
	INSERT INTO ml_predict.py_rank_predictions (
		source_data_id,
		predict_result_rank_id
	) VALUES (
		@dataId,
		(SELECT TOP 1 rank_id FROM #rank_predict_result_temp)
	)

	--Get inserted id
	Declare @insertedId int = @@Identity

	--Return result rank set
	SELECT
		*
	FROM ml_predict.ranks r
	INNER JOIN ml_predict.py_rank_predictions prp
	ON
		r.rank_id = prp.predict_result_rank_id
	WHERE
		prp.id = @insertedId

END
GO

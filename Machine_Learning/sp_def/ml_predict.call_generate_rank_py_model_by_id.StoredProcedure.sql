USE [RankPrediction]
GO
/****** Object:  StoredProcedure [ml_predict].[call_generate_rank_py_model_by_id]    Script Date: 8/25/2021 3:05:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [ml_predict].[call_generate_rank_py_model_by_id] (@p_id int,@season_id int,@isInputDataHasMatchCount bit)
AS
BEGIN
	Declare @trained_model varbinary(max)
	Declare @p_selectStr nvarchar(max)

	
	
	IF @isInputDataHasMatchCount = 'TRUE'
	BEGIN
		-- Data has match counts
		Set @p_selectStr = N'
			select "id", 
				"kill_death_ratio", 
				"average_damage", 
				"match_counts", 
				"is_party", 
				"rank_id"  
			from [ml_predict].[prediction_data] 
			WHERE
				match_counts <> -1
			AND season_id = '  + CONVERT(NVARCHAR,@season_id ) + N'
			AND id <> ' + CONVERT(NVARCHAR,@p_id )
	END
	ELSE
	BEGIN
		-- Data has no match counts
		Set @p_selectStr = N'
			select "id", 
				"kill_death_ratio", 
				"average_damage",
				"is_party", 
				"rank_id"  
			from [ml_predict].[prediction_data] 
			WHERE
				season_id = '  + CONVERT(NVARCHAR,@season_id ) + N'
				AND id <> ' + CONVERT(NVARCHAR,@p_id )
	END

	EXEC ml_predict.generate_rank_py_model @p_selectStr,@trained_model OUTPUT;

	--UPDATE model
	UPDATE ml_predict.rank_py_models
	SET
		model_data = @trained_model
	WHERE
		season_id = @season_id
	AND is_matchcounts_contain = @isInputDataHasMatchCount


END



--
GO

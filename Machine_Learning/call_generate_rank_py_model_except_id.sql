USE [RankPrediction]
GO

/****** Object:  StoredProcedure [ml_predict].[call_generate_rank_py_model_except_id]    Script Date: 8/21/2021 6:02:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [ml_predict].[call_generate_rank_py_model_except_id] (@p_id int)
AS
BEGIN
	Declare @trained_model varbinary(max)
	Declare @p_selectStr nvarchar(max) = N'
	select "id", 
		"kill_death_ratio", 
		"average_damage", 
		"match_counts", 
		"is_party", 
		"rank_id"  
	from [ml_predict].[prediction_data] 
	WHERE
		id <> ' + CONVERT(NVARCHAR,@p_id )

	EXEC ml_predict.generate_rank_py_model @p_selectStr,@trained_model OUTPUT;

	TRUNCATE TABLE ml_predict.rank_py_models;

	INSERT INTO ml_predict.rank_py_models (model_name, model) VALUES('rbf_model', @trained_model);

END



--
GO



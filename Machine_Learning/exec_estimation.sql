TRUNCATE TABLE py_rank_predictions;
--Insert the results of the predictions for test set into a table
INSERT INTO py_rank_predictions
EXEC py_predict_rank 'rbf_model';
-- Select contents of the table
SELECT * FROM py_rank_predictions;
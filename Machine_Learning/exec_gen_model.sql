TRUNCATE TABLE rank_py_models;

DECLARE @model VARBINARY(MAX);
EXEC generate_rank_py_model @model OUTPUT;

INSERT INTO rank_py_models (model_name, model) VALUES('rbf_model', @model);

SELECT * FROM rank_py_models;
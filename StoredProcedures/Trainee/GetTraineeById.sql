
DELIMITER $$
CREATE PROCEDURE GetTraineeById (IN trainee_id int)
BEGIN
  select * from trainee
  where id=trainee_id;
END $$
DELIMITER ;

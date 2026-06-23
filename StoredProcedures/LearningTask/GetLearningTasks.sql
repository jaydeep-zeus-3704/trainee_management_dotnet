
DELIMITER $$
CREATE PROCEDURE GetLearningTasks(
 IN p_search VARCHAR(100),
 IN p_status INT,
 IN p_pageNo INT,
 IN p_pageSize INT
)
BEGIN 
DECLARE v_offset INT;
SET v_offset=(p_pageNo-1)*p_pageSize;
select * from learningtask
where p_search IS NULL 
OR Title Like CONCAT('%',p_search ,'%')
OR Description Like CONCAT('%',p_search ,'%') 
OR ExpectedTechStack Like CONCAT('%',p_search ,'%')
and (p_status is null OR status=p_status)
limit v_offset,p_pageSize;
END $$
DELIMITER ;
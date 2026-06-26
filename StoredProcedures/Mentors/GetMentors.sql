/* Filters Mentors by firstname, lastname, email , expertise and status*/
DROP PROCEDURE IF EXISTS GetMentors;
DELIMITER $$
CREATE PROCEDURE GetMentors(
 IN p_search VARCHAR(100),
 IN p_status INT,
 IN p_pageNo INT,
 IN p_pageSize INT
)
BEGIN 
    DECLARE v_offset INT;
    DECLARE v_search_pattern VARCHAR(102);
    
    SET v_offset = (p_pageNo - 1) * p_pageSize;

    IF p_search IS NOT NULL AND p_search != '' THEN
        SET v_search_pattern = CONCAT('%', p_search, '%');
    ELSE
        SET v_search_pattern = NULL;
    END IF;

    SELECT * FROM Mentor
    WHERE (
        v_search_pattern IS NULL 
        OR FirstName LIKE v_search_pattern
        OR LastName LIKE v_search_pattern 
        OR Email LIKE v_search_pattern
        OR Expertise LIKE v_search_pattern
    )
    AND (p_status IS NULL OR status = p_status)
    ORDER BY Id ASC
    LIMIT v_offset, p_pageSize;
END $$

DELIMITER ;

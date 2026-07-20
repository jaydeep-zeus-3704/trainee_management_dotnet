DELIMITER $$
DROP PROCEDURE IF EXISTS GetSubmissionsByMentorId;
CREATE PROCEDURE GetSubmissionsByMentorId(IN p_MentorId INT)
BEGIN
    SELECT 
        s.Id,
        s.TaskAssignmentId,
        s.TraineeId,
        s.SubmissionUrl,
        s.Notes,
        s.SubmittedDate,
        s.Status
    FROM Submission s
    INNER JOIN TaskAssignment t 
        ON t.TraineeId = s.TraineeId 
        AND t.Id = s.TaskAssignmentId
    WHERE t.MentorId = p_MentorId;
END$$

DELIMITER ;

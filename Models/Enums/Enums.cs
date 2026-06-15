namespace trainee_management.Enums
{
    public enum StatusValues
    {
        ACTIVE,
        INACTIVE,
        COMPLETED
    }

    public enum UserRole
    {
        ADMIN,
        MENTOR,
        TRAINEE,
    }

    public enum MentorStatus
    {
        ACTIVE,
        INACTIVE
    }

    public enum LearningTaskStatus
    {
        DRAFT,
        PUBLISHED,
        CLOSED
    }

    public enum TaskAssignmentStatus
    {
        ASSIGNED,
        INPROGRESS,
        SUBMITTED,
        REVIEWED,
        COMPLETED
    }
    

    public enum SubmittedStatus
    {
        SUBMITTED,
        RESUBMITTED
    }
}
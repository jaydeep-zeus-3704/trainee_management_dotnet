
public interface IReviewService
{
    public  Task CreateReview(ReviewRequest request);
    public  Task<List<ReviewResponse>> GetAllReviews();
    public  Task<ReviewResponse> GetReview(int id);



}
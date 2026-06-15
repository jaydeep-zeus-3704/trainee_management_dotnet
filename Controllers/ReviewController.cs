
using Microsoft.AspNetCore.Mvc;
using trainee_management.Services;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
   private readonly IReviewService _review_service;
   public ReviewController(IReviewService review_service)
    {
        _review_service=review_service;
    }
    [HttpPost]
    public async Task<IActionResult> CreateReview(ReviewRequest request)
    {
        await _review_service.CreateReview(request);
        return StatusCode(201,new {message="Review Created"});
    } 

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ReviewResponse>reviews=await _review_service.GetAllReviews();
        return StatusCode(200,new {reviews,message="reviews fetched sucessfully"});
    } 

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        ReviewResponse review=await _review_service.GetReview(id);
        return StatusCode(200,new {review,message="review fetched sucessfully"});
    } 
}
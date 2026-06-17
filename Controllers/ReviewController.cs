
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
   private readonly IReviewService _review_service;
   private readonly ILogger<ReviewController> _logger;
   public ReviewController(IReviewService review_service,ILogger<ReviewController> logger)
    {
        _review_service=review_service;
        _logger=logger;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateReview(ReviewRequest request)
    {
        await _review_service.CreateReview(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: review created Sucessfully");
        return StatusCode(201,new {message="Review Created"});
    } 

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ReviewResponse>reviews=await _review_service.GetAllReviews();
        _logger.LogInformation($"\nStatus Code:200\nmessage: review List Fetched Sucessfully");
        return StatusCode(200,new {reviews,message="reviews fetched sucessfully"});
    } 

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        ReviewResponse review=await _review_service.GetReview(id);
        _logger.LogInformation($"\nStatus Code:200\nmessage: review for id ${id} fetched sucessfully");
        return StatusCode(200,new {review,message="review fetched sucessfully"});
    } 
}
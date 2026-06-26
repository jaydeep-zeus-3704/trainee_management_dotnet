

using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;

public class ReviewService:IReviewService
{
     private readonly AppDBContext _context;
     public ReviewService(AppDBContext context)
     {
        _context=context;
     }

    public async Task CreateReview(ReviewRequest request)
    {
        bool mentorIdExists=await _context.Review.AsNoTracking().AnyAsync(t=>t.MentorId==request.MentorId);
        if(!mentorIdExists) throw new NotFoundException("mentor with this id  doesn't exist");
    
        bool Submission=await _context.Review.AsNoTracking().AnyAsync(t=>t.SubmissionId==request.SubmissionId);
        if(!mentorIdExists) throw new NotFoundException("submission with this id  doesn't exist");
        
        Review review=new Review(request);

        await _context.Review.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ReviewResponse>> GetAllReviews()
    {
        List<ReviewResponse>reviews= await _context.Review.Select(r=>new ReviewResponse(r)).ToListAsync();
        return reviews;
    }
    public async Task<ReviewResponse> GetReview(int id)
    {
        Review review=await _context.Review.FindAsync(id) 
        ?? throw new NotFoundException("Id not found");
        ReviewResponse response=new ReviewResponse(review);
        return response;
    }



}
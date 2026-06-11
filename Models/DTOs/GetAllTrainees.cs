namespace trainee_management.Models.DTOs;

public class GetAllTrainees
{
    public int pageNumber {get;set;}
    public int pageSize {get;set;}
    public int totalCount {get;set;}

    public List<TraineeResponse> data {get;set;}=[];
}
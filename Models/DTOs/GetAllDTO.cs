namespace trainee_management.Models.DTOs;

public class GetAllDTO<T>
{
    public int pageNumber {get;set;}
    public int pageSize {get;set;}
    public int totalCount {get;set;}

    public List<T> data {get;set;}=[];
}
using Microsoft.EntityFrameworkCore;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Validator;
using trainee_management.Exceptions;
using trainee_management.Database;
using trainee_management.Enums;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace trainee_management.Services
{

    public class TraineeService : ItraineeService
    {
        private readonly AppDBContext _context;
        private readonly ICacheService _cache_service;
        public TraineeService(ICacheService cache_service, AppDBContext context)
        {
            _context = context;
            _cache_service = cache_service;
        }

        public async Task<Trainee?> GetTraineeById(int id)
        {
            Trainee? trainee = await _cache_service.GetDataAsync<Trainee>($"trainee_{id}");
            if (trainee == null)
            {
                trainee = await _context.Trainee.FindAsync(id) ??
                throw new NotFoundException($"Trainee with id {id} not found");
                await _cache_service.SetDataAsync($"trainee_{id}", trainee);
            }
            return trainee;
        }


        public async Task<GetAllDTO<TraineeResponse>> ReturnTrainees(string searchParams, string status, int pageNumber, int pageSize)
        {
            List<Trainee> trainees = [];
            if (Enum.TryParse(status, true, out StatusValues result))
            {
                trainees = _context.Trainee.FromSqlInterpolated($"CALL GetTrainees({searchParams},{(int)result},{pageNumber},{pageSize})").ToList();
            }
            else
            {
                throw new ValidationException("Invalid Status");
            }
            List<TraineeResponse> traineesList = trainees.Select(trainee => new TraineeResponse(trainee)).ToList();
            GetAllDTO<TraineeResponse> response = new GetAllDTO<TraineeResponse>
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = traineesList.Count,
                data = traineesList
            };
            return response;
        }


        public async Task CheckIfTraineeExists(string email)
        {
            bool exists = await _context.Trainee.AnyAsync(trainee => trainee.Email == email);
            if (exists) throw new DuplicateEmailException("User with this email already exists");
        }

        public async Task CreateTrainee(CreateTraineeRequest request)
        {
            Trainee trainee = new Trainee(request);
            TraineeValidator validator = new TraineeValidator(trainee);
            if (!validator.Validate()) throw new ValidationException("Invalid Input");
            await _context.Trainee.AddAsync(trainee);
            await _context.SaveChangesAsync();
        }

        public TraineeResponse GetTraineeResponse(Trainee trainee)
        {
            TraineeResponse response = new TraineeResponse(trainee);
            return response;
        }

        public async Task UpdateTrainee(UpdateTraineeRequest request, int id)
        {
            Trainee trainee = await GetTraineeById(id) ?? throw new NotFoundException("Trainee Not found");
            Trainee? cached_trainee = await _cache_service.GetDataAsync<Trainee>($"trainee_{id}");
            trainee.FirstName = request.FirstName;
            trainee.LastName = request.LastName;
            trainee.Email = request.Email;
            trainee.TechStack = request.TechStack;
            trainee.Status = request.Status;
            trainee.UpdatedDate = DateTime.Today;

            if (cached_trainee != null)
            {
                await _cache_service.SetDataAsync($"trainee_{id}", trainee);
            }
            await _context.SaveChangesAsync();

        }

        public async Task DeleteTrainee(Trainee trainee)
        {
            _context.Remove(trainee);
            await _cache_service.DeleteAsync($"trainee_{trainee.Id}");
            await _context.SaveChangesAsync();
        }
    }
}
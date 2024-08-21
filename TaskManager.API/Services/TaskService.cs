using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskManager.Api.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Task;

namespace TaskManager.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Desk> _deskRepo;
        private readonly IRepository<Project> _projectRepo;
        private readonly IRepository<WorkTask> _taskRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public TaskService(IRepository<Desk> deskRepo, IRepository<WorkTask> taskRepo, IRepository<Project> projectRepo, IRepository<User> userRepo, IMapper mapper)
        {
            _taskRepo = taskRepo;
            _deskRepo = deskRepo;
            _projectRepo = projectRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<BaseResponce<bool>> Create(TaskCreateDto createDto, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().FirstAsync(u => u.Email == username);
                User? contractorUser = null;
                if (createDto.ContractorId is not null)
                {
                    contractorUser = await _userRepo.GetAll().FirstOrDefaultAsync(u => u.Id == createDto.ContractorId);
                }

                var desk = await _deskRepo.GetAll().FirstOrDefaultAsync(d => d.Id == createDto.DeskId);
                if (desk is null)
                {
                    return new BaseResponce<bool>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Desk for creating task not found"
                    };
                }

                var deskCols = JsonSerializer.Deserialize<List<string>>(desk.DeskColumns) ?? new List<string>();
                if (!deskCols.Contains(createDto.ColumnOfDesk))
                {
                    return new BaseResponce<bool>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Column of desk not exist"
                    };
                }

                var task = _mapper.Map<WorkTask>(createDto);
                task.CreatorId = currentUser.Id;
                await _taskRepo.Create(task);
                return new BaseResponce<bool>
                {
                    IsOkay = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<bool>> Delete(int id)
        {
            try
            {
                var task = await _taskRepo.GetAll().FirstOrDefaultAsync(t => t.Id == id);
                if (task is null)
                {
                    return new BaseResponce<bool>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Task not found"
                    };
                }

                await _taskRepo.Delete(task);
                return new BaseResponce<bool>
                {
                    IsOkay = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<TaskBaseDto>> Update(int id, TaskUpdateDto updateDto)
        {
            try
            {
                var task = await _taskRepo.GetAll().FirstOrDefaultAsync(t => t.Id == id);
                if (task is null)
                {
                    return new BaseResponce<TaskBaseDto>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Task not found"
                    };
                }
                var desk = await _deskRepo.GetAll().FirstOrDefaultAsync(d => d.Id == updateDto.DeskId);
                if (desk is null)
                {
                    return new BaseResponce<TaskBaseDto>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Desk for updating task not found"
                    };
                }

                _mapper.Map(updateDto, task);
                WorkTask newTask = await _taskRepo.Update(task);
                return new BaseResponce<TaskBaseDto>
                {
                    IsOkay = true,
                    Data = _mapper.Map<TaskGetDto>(newTask)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<TaskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<TaskBaseDto>> Get(int id)
        {
            try
            {
                var task = await _taskRepo.GetAll().AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
                if (task is null)
                {
                    return new BaseResponce<TaskBaseDto>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Task not found"
                    };
                }
                return new BaseResponce<TaskBaseDto>
                {
                    IsOkay = true,
                    Data = _mapper.Map<TaskGetDto>(task)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<TaskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetByDesk(int deskId)
        {
            try
            {
                var tasks = await _taskRepo.GetAll().AsNoTracking().Where(t => t.DeskId == deskId).ToListAsync();
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = true,
                    Data = _mapper.Map<IEnumerable<TaskGetDto>>(tasks)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetAssigningUser(string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking().Include(u => u.AssigningTasks).FirstAsync(u => u.Email == username);
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = true,
                    Data = _mapper.Map<IEnumerable<TaskGetDto>>(currentUser.AssigningTasks)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<TaskBaseDto>>> GetCreatingUser(string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking().Include(u => u.CreatingTasks).FirstAsync(u => u.Email == username);
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = true,
                    Data = _mapper.Map<IEnumerable<TaskGetDto>>(currentUser.CreatingTasks)
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<TaskBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }
    }
}

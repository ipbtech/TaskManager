using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskManager.Api.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Desk;
using TaskManager.DTO.Desk.ColumnDesk;

namespace TaskManager.Api.Services
{
    public class DeskService : IDeskService
    {
        private readonly IRepository<Desk> _deskRepo;
        private readonly IRepository<Project> _projectRepo;
        private readonly IRepository<WorkTask> _taskRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public DeskService(IRepository<Desk> deskRepo, IRepository<WorkTask> taskRepo, IRepository<Project> projectRepo, IRepository<User> userRepo, IMapper mapper)
        {
            _taskRepo = taskRepo;
            _deskRepo = deskRepo;
            _projectRepo = projectRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }   


        public async Task<BaseResponce<bool>> Create(DeskCreateDto createDto, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking().FirstAsync(u => u.Email == username);
                var project = await _projectRepo.GetAll().Include(p => p.Desks)
                    .FirstOrDefaultAsync(p => (p.Id == createDto.ProjectId && p.AdminId == currentUser.Id) || currentUser.Role == UserRole.SystemOwner);
                if (project is not null)
                {
                    if (!project.Desks.Any(d => d.Name == createDto.Name))
                    {
                        var desk = _mapper.Map<Desk>(createDto);
                        await _deskRepo.Create(desk);
                        return new BaseResponce<bool>
                        {
                            IsOkay = true,
                            Data = true
                        };
                    }
                    return new BaseResponce<bool>
                    {
                        IsOkay = false,
                        StatusCode = 400,
                        Description = "Desk with passed name already exist in the project"
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Project for created desk not found or current user is not admin of the project"
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


        public async Task<BaseResponce<bool>> Delete(int id, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking()
                    .Include(u => u.AdminProjects).FirstAsync(u => u.Email == username);
                var desk = await _deskRepo.GetAll().Include(d => d.Project).FirstOrDefaultAsync(d => d.Id == id);
                if (desk is not null && (desk.Project?.AdminId == currentUser.Id || currentUser.Role == UserRole.SystemOwner))
                {
                    await _deskRepo.Delete(desk);
                    return new BaseResponce<bool>
                    {
                        IsOkay = true,
                        Data = true
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found or current user is not admin of the project"
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

        public async Task<BaseResponce<DeskBaseDto>> Update(int id, DeskUpdateDto updateDto, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking()
                    .Include(u => u.AdminProjects).FirstAsync(u => u.Email == username);
                var desk = await _deskRepo.GetAll().Include(p => p.Project).FirstOrDefaultAsync(p => p.Id == id);
                if (desk is not null && (desk.Project?.AdminId == currentUser.Id || currentUser.Role == UserRole.SystemOwner))
                {
                    var project = await _projectRepo.GetAll()
                        .Include(p => p.Desks).FirstOrDefaultAsync(p => p.Id == updateDto.ProjectId);
                    if (project is not null)
                    {
                        if (!project.Desks.Any(d => d.Name == updateDto.Name))
                        {
                            _mapper.Map(updateDto, desk);
                            Desk newDesk = await _deskRepo.Update(desk);
                            return new BaseResponce<DeskBaseDto>
                            {
                                IsOkay = true,
                                Data = _mapper.Map<DeskGetDto>(newDesk)
                            };
                        }
                        return new BaseResponce<DeskBaseDto>
                        {
                            IsOkay = false,
                            StatusCode = 400,
                            Description = "Desk with passed name already exist in the project"
                        };
                    }
                    return new BaseResponce<DeskBaseDto>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Project for created desk not found"
                    };
                }
                return new BaseResponce<DeskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found or current user is not admin of the project"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<DeskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<DeskBaseDto>> Get(int id)
        {
            try
            {
                var desk = await _deskRepo.GetAll().AsNoTracking()
                    .Include(d => d.Project).Include(d => d.Tasks).FirstOrDefaultAsync(d => d.Id == id);
                if (desk is not null)
                {
                    return new BaseResponce<DeskBaseDto>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<DeskGetDto>(desk)
                    };
                }
                return new BaseResponce<DeskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<DeskBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<IEnumerable<DeskBaseDto>>> GetByProjectId(int projectId)
        {
            try
            {
                var desks = await _deskRepo.GetAll().AsNoTracking()
                    .Include(d => d.Project).Include(d => d.Tasks).Where(d => d.ProjectId == projectId).ToListAsync();
                if (desks is not null)
                {
                    return new BaseResponce<IEnumerable<DeskBaseDto>>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<IEnumerable<DeskGetDto>>(desks)
                    };
                }
                return new BaseResponce<IEnumerable<DeskBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Project for getting desks not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<DeskBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<bool>> AddColumnDesk(AddingColumnDto entity, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking()
                    .Include(u => u.AdminProjects).FirstAsync(u => u.Email == username);
                var desk = await _deskRepo.GetAll().Include(d => d.Project).FirstOrDefaultAsync(d => d.Id == entity.DeskId);
                if (desk is not null && (desk.Project?.AdminId == currentUser.Id || currentUser.Role == UserRole.SystemOwner))
                {
                    var columns = JsonSerializer.Deserialize<List<string>>(desk.DeskColumns);
                    columns?.Add(entity.Name);
                    desk.DeskColumns = JsonSerializer.Serialize(columns);
                    await _deskRepo.Update(desk);
                    return new BaseResponce<bool>
                    {
                        IsOkay = true,
                        Data = true
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found or current user is not admin of the project"
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

        public async Task<BaseResponce<bool>> UpdateColumnDesk(UpdatingColumnDto entity, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking()
                    .Include(u => u.AdminProjects).FirstAsync(u => u.Email == username);
                var desk = await _deskRepo.GetAll().Include(d => d.Project).Include(d => d.Tasks).FirstOrDefaultAsync(d => d.Id == entity.DeskId);
                if (desk is not null && (desk.Project?.AdminId == currentUser.Id || currentUser.Role == UserRole.SystemOwner))
                {
                    var columns = JsonSerializer.Deserialize<List<string>>(desk.DeskColumns);

                    var ind = columns.FindIndex(e => e == entity.OldName);
                    columns?.Remove(entity.OldName);
                    columns?.Insert(ind, entity.NewName);

                    desk.DeskColumns = JsonSerializer.Serialize(columns);
                    await _deskRepo.Update(desk);

                    var tasksUpdating = desk.Tasks.Where(t => t.ColumnOfDesk == entity.OldName)
                        .Select(t => UpdateDeskColumnsTasks(t, entity.NewName)).ToArray();
                    await Task.WhenAll(tasksUpdating);

                    return new BaseResponce<bool>
                    {
                        IsOkay = true,
                        Data = true
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found or current user is not admin of the project"
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

        public async Task<BaseResponce<bool>> DeleteColumnDesk(DeletingColumnDto entity, string username)
        {
            try
            {
                var currentUser = await _userRepo.GetAll().AsNoTracking()
                    .Include(u => u.AdminProjects).FirstAsync(u => u.Email == username);
                var desk = await _deskRepo.GetAll().Include(d => d.Project).Include(d => d.Tasks).FirstOrDefaultAsync(d => d.Id == entity.DeskId);
                if (desk is not null && (desk.Project?.AdminId == currentUser.Id || currentUser.Role == UserRole.SystemOwner))
                {
                    var columns = JsonSerializer.Deserialize<List<string>>(desk.DeskColumns);
                    columns?.Remove(entity.Name);
                    var defaultCol = columns?.FirstOrDefault();
                    if (defaultCol is not null)
                    {
                        desk.DeskColumns = JsonSerializer.Serialize(columns);
                        await _deskRepo.Update(desk);

                        var tasksUpdating = desk.Tasks.Where(t => t.ColumnOfDesk == entity.Name)
                            .Select(t => UpdateDeskColumnsTasks(t, defaultCol)).ToArray();
                        await Task.WhenAll(tasksUpdating);

                        return new BaseResponce<bool>
                        {
                            IsOkay = true,
                            Data = true
                        };
                    }
                    return new BaseResponce<bool>
                    {
                        IsOkay = false,
                        StatusCode = 400,
                        Description = "Can't delete last column"
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Desk not found or current user is not admin of the project"
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

        private async Task UpdateDeskColumnsTasks(WorkTask task, string newName)
        {
            task.ColumnOfDesk = newName;
            await _taskRepo.Update(task);
        }
    }
}

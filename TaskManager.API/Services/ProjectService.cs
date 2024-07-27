using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Project;
using TaskManager.DTO.User;

namespace TaskManager.API.Services
{
    public class ProjectService : IService<ProjectBaseDto>
    {
        private readonly IRepository<Project> _projectRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projRepo, IRepository<User> userRepo, IMapper mapper)
        {
            _projectRepo = projRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<BaseResponce<bool>> Create(ProjectBaseDto entity)
        {
            try
            {
                if (!_projectRepo.GetAll().Any(proj => proj.Name == entity.Name))
                {
                    var admin = await _userRepo.GetAll().Where(u => u.Role == UserRole.Admin || u.Role == UserRole.SystemOwner)
                        .FirstOrDefaultAsync(u => u.Id == ((ProjectCreateDto)entity).AdminId);
                    if (admin is not null)
                    {
                        var proj = _mapper.Map<Project>(entity);
                        proj.ProjectUsers.Add(admin);
                        await _projectRepo.Create(proj);
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
                        Description = "User with passed id is not admin or system owner"
                    };
                }
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 400,
                    Description = "Project with passed name already exists"
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
                var proj = _projectRepo.GetAll().FirstOrDefault(p => p.Id == id);
                if (proj is not null)
                {
                    await _projectRepo.Delete(proj);
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
                    Description = "Project not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<bool>()
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<ProjectBaseDto>> Update(int id, ProjectBaseDto entity)
        {
            try
            {
                var project = await _projectRepo.GetAll()
                    .Include(p => p.Desks).FirstOrDefaultAsync(p => p.Id == id);
                if (project is not null)
                {
                    if (entity is ProjectUpdateDto updateDto)
                    {
                        var admin = await _userRepo.GetAll().Where(u => u.Role == UserRole.Admin || u.Role == UserRole.SystemOwner)
                            .AsNoTracking().FirstOrDefaultAsync(u => u.Id == updateDto.AdminId);
                        if (admin is not null)
                        {
                            _mapper.Map(updateDto, project);
                            Project newProj = await _projectRepo.Update(project);
                            return new BaseResponce<ProjectBaseDto>
                            {
                                IsOkay = true,
                                Data = _mapper.Map<ProjectGetDto>(newProj)
                            };
                        }
                        return new BaseResponce<ProjectBaseDto>
                        {
                            IsOkay = false,
                            StatusCode = 400,
                            Description = "User with passed id is not admin or system owner"
                        };
                    }
                    return new BaseResponce<ProjectBaseDto>
                    {
                        IsOkay = false,
                        StatusCode = 404,
                        Description = "Project not found"
                    };
                }
                return new BaseResponce<ProjectBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 400,
                    Description = "Passed model is invalid"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<ProjectBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal Server Error"
                };
            }
        }

        public async Task<BaseResponce<ProjectBaseDto>> Get(int id)
        {
            try
            {
                var project = await _projectRepo.GetAll()
                    .Include(p => p.Admin).Include(p => p.Desks).Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.Id == id);
                if (project is not null)
                {
                    return new BaseResponce<ProjectBaseDto>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<ProjectGetDto>(project)
                    };
                }
                return new BaseResponce<ProjectBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 404,
                    Description = "Project not found"
                };
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<ProjectBaseDto>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal Server Error"
                };
            }
        }

        public Task<BaseResponce<IEnumerable<ProjectBaseDto>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Helpers;
using TaskManager.Dal.Repository;
using TaskManager.DAL.Models;
using TaskManager.DTO.Project;

namespace TaskManager.Api.Services
{
    public class ProjectService : IProjectService
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

        public async Task<BaseResponce<bool>> Create(ProjectCreateDto createDto)
        {
            try
            {
                if (!_projectRepo.GetAll().AsNoTracking().Any(proj => proj.Name == createDto.Name))
                {
                    var admin = await _userRepo.GetAll()
                        .Where(u => u.Role == UserRole.Admin || u.Role == UserRole.SystemOwner)
                        .FirstOrDefaultAsync(u => u.Id == createDto.AdminId);
                    if (admin is not null)
                    {
                        var proj = _mapper.Map<Project>(createDto);
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

        public async Task<BaseResponce<ProjectBaseDto>> Update(int id, ProjectUpdateDto updateDto)
        {
            try
            {
                var project = await _projectRepo.GetAll()
                    .Include(p => p.Admin).Include(p => p.Desks)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (project is not null)
                {
                    var admin = await _userRepo.GetAll()
                        .Where(u => u.Role == UserRole.Admin || u.Role == UserRole.SystemOwner)
                        .FirstOrDefaultAsync(u => u.Id == updateDto.AdminId);
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
                var project = await _projectRepo.GetAll().AsNoTracking()
                    .Include(p => p.Admin).Include(p => p.Desks).FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<BaseResponce<IEnumerable<ProjectBaseDto>>> GetAll(string username)
        {
            try
            {
                var user = await _userRepo.GetAll().AsNoTracking().FirstAsync(u => u.Email == username);
                if (user.Role == UserRole.SystemOwner)
                {
                    var projects = await _projectRepo.GetAll().AsNoTracking()
                        .Include(p => p.Admin).Include(p => p.Desks).ToListAsync();
                    return new BaseResponce<IEnumerable<ProjectBaseDto>>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<IEnumerable<ProjectGetDto>>(projects)
                    };
                }
                else
                {
                    var projects = await _projectRepo.GetAll().AsNoTracking()
                        .Include(p => p.Admin).Include(p => p.Desks).Include(p => p.ProjectUsers)
                        .Where(p => p.ProjectUsers.Any(u => u.Id == user.Id)).ToListAsync();
                    return new BaseResponce<IEnumerable<ProjectBaseDto>>
                    {
                        IsOkay = true,
                        Data = _mapper.Map<IEnumerable<ProjectGetDto>>(projects)
                    };
                }
            }
            catch (Exception ex)
            {
                //TODO logging
                return new BaseResponce<IEnumerable<ProjectBaseDto>>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<bool>> AddUsersToProject(int projectId, List<int> userIds)
        {
            try
            {
                var project = await _projectRepo.GetAll()
                    .Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.Id == projectId);
                if (project is not null)
                {
                    foreach (var id in userIds)
                    {
                        var user = await _userRepo.GetAll().FirstOrDefaultAsync(u => u.Id == id);
                        if (user is not null && !project.ProjectUsers.Any(u => u.Id == id))
                            project.ProjectUsers.Add(user);
                    }
                    await _projectRepo.Update(project);
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
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }

        public async Task<BaseResponce<bool>> RemoveUsersFromProject(int projectId, List<int> userIds)
        {
            try
            {
                var project = await _projectRepo.GetAll()
                    .Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.Id == projectId);
                if (project is not null)
                {
                    foreach (var id in userIds)
                    {
                        var user = await _userRepo.GetAll().FirstOrDefaultAsync(u => u.Id == id);
                        if (user is not null && project.ProjectUsers.Any(u => u.Id == id))
                            project.ProjectUsers.Remove(user);
                    }
                    await _projectRepo.Update(project);
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
                return new BaseResponce<bool>
                {
                    IsOkay = false,
                    StatusCode = 500,
                    Description = "Internal server error"
                };
            }
        }
    }
}

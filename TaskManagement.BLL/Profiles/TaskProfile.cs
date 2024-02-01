using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;

namespace TaskManagement.BLL.Profiles
{
    public class TaskProfile: Profile
    {
        public TaskProfile()
        {
            CreateMap<DAL.Entities.Task, TaskResponse>();
            CreateMap<TaskCreateRequest, DAL.Entities.Task>();
            CreateMap<TaskUpdateRequest, DAL.Entities.Task>();
        }
    }
}

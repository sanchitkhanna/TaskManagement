using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.Profiles;
using TaskManagement.BLL.Services.Task;
using FluentValidation;
using TaskManagement.BLL.Validators.Task;
using FluentValidation.AspNetCore;

namespace TaskManagement.BLL
{
    public static class BLLServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(TaskCreateValidator));
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddAutoMapper(typeof(TaskProfile));
            services.AddTransient<ITaskService, TaskService>();
            
            return services;
        }
    }
}

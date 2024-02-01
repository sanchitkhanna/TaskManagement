using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL.Repositories.Task;

namespace TaskManagement.DAL
{
    public static class DALServiceRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddDbContext<TaskManagementDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
            return services;
        }
    }
}

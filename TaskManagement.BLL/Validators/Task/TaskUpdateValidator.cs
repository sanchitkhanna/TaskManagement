using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;

namespace TaskManagement.BLL.Validators.Task
{
    public class TaskUpdateValidator: AbstractValidator<TaskUpdateRequest>
    {
        public TaskUpdateValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Deadline).Must(BeAValidDate).WithMessage("Deadline should be greater than or equal to today's date");
        }
        private bool BeAValidDate(DateTime? date)
        {
            if (date != null)
            {
                if (date >= DateTime.Today)
                {
                    return true;
                }
                return false;
            }
            return true;

        }
    }
}

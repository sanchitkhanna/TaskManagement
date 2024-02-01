using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.BLL.DTO.Task
{
    public class TaskCreateRequest
    {
        //[Required]
        //[StringLength(50)]
        public string Name { get; set; }
        //[Required]
        //[StringLength(200)]
        public string Description { get; set; }
        //[RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$",
        //    ErrorMessage="Date format should be in dd/MM/yyyy")]
        public DateTime? Deadline { get; set; }
    }
}

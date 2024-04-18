using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Domain.Entities.Frameworks
{
    public class Result
    {
        public ResultCodes Code { get; set; }

        public string Message { get; set; }
    }

    public enum ResultCodes
    {
            Success = 0,
            Failure = 1,
            NotFound = -1
    }

    public class Result<T> : Result
    {
        public T Entity { get; set; }
    }
}

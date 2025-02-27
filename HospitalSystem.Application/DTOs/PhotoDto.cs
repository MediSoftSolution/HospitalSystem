using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.DTOs
{
    public sealed record PhotoDto(string Url, bool IsMain = false);
}

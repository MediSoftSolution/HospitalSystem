using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Beheviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Log.Information("Handling {RequestName} with data: {@Request}", requestName, request);

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();

            Log.Information("Handled {RequestName} in {ElapsedMilliseconds}ms with response: {@Response}",
                requestName, stopwatch.ElapsedMilliseconds, response);

            return response;
        }
    }
}

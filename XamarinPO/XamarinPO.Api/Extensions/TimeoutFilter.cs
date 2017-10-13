using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace XamarinPO.Api.Extensions
{
    public class TimeoutFilter : ActionFilterAttribute
    {
        public int Timeout { get; set; }

        public TimeoutFilter()
        {
            Timeout = int.MaxValue;
        }
        public TimeoutFilter(int timeout)
        {
            Timeout = timeout;
        }


        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            var controller = actionContext.ControllerContext.Controller;
            var controllerType = controller.GetType();
            var action = controllerType.GetMethod(actionContext.ActionDescriptor.ActionName);
            var tokenSource = new CancellationTokenSource();
            var timeout = TimeoutTask(Timeout);
            object result = null;

            var work = Task.Run(() =>
            {
                result = action.Invoke(controller, actionContext.ActionArguments.Values.ToArray());
            }, tokenSource.Token);

            var finishedTask = await Task.WhenAny(timeout, work);

            if (finishedTask == timeout)
            {
                tokenSource.Cancel();

                var auxResponse = actionContext.Request.CreateResponse(HttpStatusCode.RequestTimeout);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.RequestTimeout, auxResponse.ReasonPhrase);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        private async Task TimeoutTask(int timeoutValue)
        {
            await Task.Delay(timeoutValue);
        }
    }
}
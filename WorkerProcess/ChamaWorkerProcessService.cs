using Chama.WebApi.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Chama.WebApi.ServiceBus;
using Chama.WebApi.Controllers;
using Chama.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Chama.WebApi.Utils;

namespace Chama.WebApi.WorkerProcess
{
    public class ChamaWorkerProcessService : BackgroundService
    {
        private readonly ICoursesController _controller;
        public ChamaWorkerProcessService(ICoursesController controller)
        {
            _controller = controller;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Log("ChamaWorkerProcessService is starting.");

            stoppingToken.Register(() =>
                    Logger.Log("ChamaWorkerProcessService task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Logger.Log("ChamaWorkerProcessService task doing background work.");

                    IChamaQueueReceiver<SignUpDTO> receiver = new ChamaQueueReceiver<SignUpDTO>();
                    receiver.Receive(
                        message =>
                        {
                            var result = _controller.SignUp(message) as JsonResult;
                            if (result.Value != null && result.Value is RequestResult && (result.Value as RequestResult).State == RequestState.Success)
                            {
                                //send email
                            }

                            return MessageProcessResponse.Complete;
                        },
                        ex => Logger.Log(ex.Message),
                        () => Logger.Log("Receiving Queue Message..."));

                    await Task.Delay(5000, stoppingToken);
                }

                catch(Exception ex)
                {
                    Logger.Log(ex);
                }
            }

            Logger.Log("ChamaWorkerProcessService task is stopping.");
        }

    }
}

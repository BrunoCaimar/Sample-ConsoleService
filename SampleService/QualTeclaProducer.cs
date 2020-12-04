using MassTransit;
using Microsoft.Extensions.Hosting;
using SampleService.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleService
{
    public class QualTeclaProducer :
        IHostedService
    {
        private readonly IPublishEndpoint _client;

        public QualTeclaProducer(IPublishEndpoint client)
        {
            this._client = client;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            return new TaskFactory()
                .StartNew(
                () =>
                {
                    while (true)
                    {
                        Console.WriteLine("Pressione uma tecla para produzir e enviar uma nova mensagem...");
                        var k = Console.ReadKey();
                        this.Enviar(k.KeyChar);
                    }
                });
        }

        private void Enviar(char keyChar)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"Produzindo... {keyChar}"));

            this._client.Publish<IQualTecla>(new { Message = $"Tecla: {keyChar}" });
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"Stoping..."));
            return Task.CompletedTask;
        }
    }
}
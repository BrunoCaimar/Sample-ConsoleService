using MassTransit;
using SampleService.Contracts;
using System;
using System.Threading.Tasks;

namespace SampleService
{
    public class QualTeclaConsumer :
        IConsumer<IQualTecla>
    {
        public Task Consume(ConsumeContext<IQualTecla> context)
        {
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Consumindo... "));
            Console.WriteLine(Figgle.FiggleFonts.Bubble.Render($"{context.Message.Message}"));

            if (context.Message.Message == "Tecla: e")
            {
                Console.WriteLine(Figgle.FiggleFonts.Bubble.Render("Tecla: e NAO permitida!"));
                throw new Exception("Tecla: e não permitida!");
            }

            return Task.CompletedTask;
        }
    }
}
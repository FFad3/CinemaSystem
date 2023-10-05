namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal sealed class PipelineConfiguration
    {
        public const string SectionName = "MediatRPieline";
        public bool PereformenceLogging { get; set; } = false;
        public bool RequestPayloadLogging { get; set; } = false;
        public bool RequestResultLogging { get; set; } = false;
    }
}
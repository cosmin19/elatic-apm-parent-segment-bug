
using Elastic.Apm;
using Elastic.Apm.Api;

namespace ElasticApm.ParentSegmentBug.Services;

public class ThridService
{
    internal async Task DoWorkWithoutSpanAsync(IExecutionSegment? executionSegment)
    {
        var transaction = Agent.Tracer.StartTransaction("ThridService.DoWorkWithoutSpanAsync", "custom", executionSegment?.OutgoingDistributedTracingData);

        var httpClient = new HttpClient();
        await httpClient.GetAsync("https://google.com");

        transaction?.End();
    }

    internal async Task DoWorkWithSpanAsync(IExecutionSegment? executionSegment)
    {
        var transaction = Agent.Tracer.StartTransaction("ThridService.DoWorkWithSpanAsync", "custom", executionSegment?.OutgoingDistributedTracingData);

        transaction?.CaptureSpan("ThridService.DummySpan", "custom", async () =>
        {
            await Task.Delay(10);
        });

        var httpClient = new HttpClient();
        await httpClient.GetAsync("https://google.com");

        transaction?.End();
    }
}

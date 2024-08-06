using Elastic.Apm;
using Elastic.Apm.Api;

namespace ElasticApm.ParentSegmentBug.Services;

public class SecondService
{
    private readonly ThridService _thirdService;

    public SecondService(ThridService thirdService)
    {
        _thirdService = thirdService;
    }

    internal async Task WrongCaseAsync(ITransaction? parentTransaction)
    {
        var span = parentTransaction?.StartSpan("SecondService.WrongCaseAsync", "custom");

        await Task.Delay(10);

        await _thirdService.DoWorkWithoutSpanAsync(span);

        span?.End();
    }

    internal async Task BypassTransactionCaseAsync(ITransaction? parentTransaction)
    {
        var transaction = Agent.Tracer.StartTransaction("SecondService.BypassTransactionCaseAsync", "custom", parentTransaction?.OutgoingDistributedTracingData);

        await Task.Delay(10);

        await _thirdService.DoWorkWithoutSpanAsync(transaction);

        transaction?.End();
    }

    internal async Task BypassSpanCaseAsync(ITransaction? parentTransaction)
    {
        // Create a dummy span
        parentTransaction?.CaptureSpan("SecondService.DummySpan", "custom", async () =>
        {
            await Task.Delay(10);
        });

        var span = parentTransaction?.StartSpan("SecondService.BypassSpanCaseAsync", "custom");

        await Task.Delay(10);

        await _thirdService.DoWorkWithSpanAsync(span);

        span?.End();
    }
}

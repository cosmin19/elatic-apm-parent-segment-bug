
using Elastic.Apm;
using Elastic.Apm.Api;

namespace ElasticApm.ParentSegmentBug.Services;

public class FirstService
{
    private readonly SecondService _secondService;

    public FirstService(SecondService secondService)
    {
        _secondService = secondService;
    }

    internal async Task WrongCaseAsync(ITransaction? parentTransaction)
    {
        var transaction = Agent.Tracer.StartTransaction("FirstService.WrongCaseAsync", "custom", parentTransaction?.OutgoingDistributedTracingData);

        await Task.Delay(10);

        await _secondService.WrongCaseAsync(transaction);

        transaction?.End();
    }

    internal async Task BypassTransactionCaseAsync(ITransaction? parentTransaction)
    {
        var transaction = Agent.Tracer.StartTransaction("FirstService.BypassTransactionCaseAsync", "custom", parentTransaction?.OutgoingDistributedTracingData);

        await Task.Delay(10);

        await _secondService.BypassTransactionCaseAsync(transaction);

        transaction?.End();
    }

    internal async Task BypassSpanCaseAsync(ITransaction? parentTransaction)
    {
        var transaction = Agent.Tracer.StartTransaction("FirstService.BypassSpanCaseAsync", "custom", parentTransaction?.OutgoingDistributedTracingData);

        await Task.Delay(10);

        await _secondService.BypassSpanCaseAsync(transaction);

        transaction?.End();
    }
}

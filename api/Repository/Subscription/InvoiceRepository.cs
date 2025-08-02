using api.Data;
using api.Models;
using api.Repository.interfaces;
using api.Repository.Subscription.interfaces;

namespace api.Repository.Subscription;

public class InvoiceRepository:GenericRepository<Invoice,Guid>,IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}

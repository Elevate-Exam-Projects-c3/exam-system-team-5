using exam_system.Persistence.Context;

namespace exam_system.Common.Middleware
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;
        public TransactionMiddleware(AppDbContext context)
        {
            _context = context;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //for get endpoints
            if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                // Skip transaction for GET requests
                await next(context);
                return;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await next(context);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

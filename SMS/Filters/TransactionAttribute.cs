using Common.CurrentUser;
using DataContext;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class TransactionAttribute : ActionFilterAttribute, IAsyncActionFilter
    {
        

    protected SmsDbContext Context { get; private set; }
        protected ICurrentUser CurrentUser { get; private set; }

        public TransactionAttribute(SmsDbContext context, ICurrentUser currentUser)
        {
            Context = context;
            CurrentUser = currentUser;
        }

        public new async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                var executed = await next();
                if (executed.Exception == null)
                    await transaction.CommitAsync();
            }
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class FilterKeywordActionFilter
        : IActionFilter
    {
        //runs after the execution goes into the controller's method
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        //runs before the execution goes into the controller's method
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool stop = true;

            //string login = context.HttpContext.User.Identity.Name; //email address

            if(context.ActionArguments.Count == 0)
            {
                //problem - cannot continue
                context.Result = new ForbidResult();
                return;
            }

            var userInput = context.ActionArguments.SingleOrDefault(x => x.Key == "keyword");
            if(userInput.Value != null)
            {
                if(userInput.Value.ToString().Trim() != "")
                {
                    //fine unless its some kind of malicious injection
                    return;
                }
            }

            //problem - cannot continue
            context.Result = new ForbidResult();
            return;


        }
    }
}

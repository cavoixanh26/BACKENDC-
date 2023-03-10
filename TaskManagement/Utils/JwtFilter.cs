using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace SWP_Login.Utils
{
    public class JwtFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Token ", "");
                var jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                //Console.WriteLine(DateTime.Parse(jwt.Payload.Claims.ToList()[3].Value));
                //Console.WriteLine(DateTime.UtcNow);
                
                Console.WriteLine(jwt.Claims.ToList()[0].Value);
                Console.WriteLine(jwt.Claims.ToList()[1].Value);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine(jwt.ValidTo);

                if (jwt == null || jwt.ValidTo< DateTime.Now )
                {
                    context.Result = new UnauthorizedResult();
                    return  ;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                context.Result = new UnauthorizedResult();
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}

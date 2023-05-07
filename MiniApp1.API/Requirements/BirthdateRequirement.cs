using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace MiniApp1.API.Requirements
{
    public class BirthdateRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }

        public BirthdateRequirement(int age)
        {
            Age = age;
        }

        public class BirthdateRequirementHandler : AuthorizationHandler<BirthdateRequirement>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BirthdateRequirement requirement)
            {
                var birthdate = context.User.FindFirst("birth-date");
                if (birthdate == null)
                {
                    //endpointe istek gondere bilmirsen
                    context.Fail();
                    return Task.CompletedTask;
                }

                var toDay = DateTime.Now;
                var age = toDay.Year - Convert.ToDateTime(birthdate.Value).Year;

                if (age >= requirement.Age)
                    context.Succeed(requirement);
                else
                    context.Fail();

                //bos task classi qaytarir.
                return Task.CompletedTask;
            }
        }
    }
}

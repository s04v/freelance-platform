using Core.Auth;
using Core.Orders.Requests;
using Core.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.DeliveryDays).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        }
    }
}

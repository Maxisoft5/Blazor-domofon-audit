using ApplicationsAudit.Core.DTOs;
using FluentValidation;

namespace ApplicationsAudit.Core.Validators
{
    public class ManagerInfoValidator : AbstractValidator<ManagerInfoDTO>
    {
        private const int _minPasswordLength = 8;
        private const int _maxPasswordLength = 24;

        public ManagerInfoValidator()
        {
            RuleFor(x => x.ContactInfo).NotNull();
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Введите пароль")
                .NotEmpty()
                .WithMessage("Введите пароль")
                .MinimumLength(_minPasswordLength)
                .WithMessage($"Длина пароля минимум {_minPasswordLength} символов.")
                .MaximumLength(_maxPasswordLength)
                .WithMessage($"Длина пароля максимум {_maxPasswordLength} символов.")
                //.Matches(@"\W")
                //.WithMessage("Пароль должен содержать 1 не числовой символ")
                .Must(password => !password.Contains(' '))
                .WithMessage("Пароль должен быть без пробелов")
                .Matches(@".*[A-Z]").WithMessage("Пароль должен содержать как минимум 1 символ в верхнем регистре")
                .Matches(@".*\d").WithMessage("Пароль должен содержать 1 или более цифр")
                .Matches(@".*[a-z]").WithMessage("Пароль должен содержать как минимум 1 символ в нижнем регистре");

        }
    }
}

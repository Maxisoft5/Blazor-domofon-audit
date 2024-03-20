using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Helpers;
using FluentValidation;

namespace ApplicationsAudit.Core.Validators
{
    public class ContactInfoValidator : AbstractValidator<ContactInfoDTO>
    {
        private const int _maxNameLength = 48;
        private const int _minNameLength = 3;
        public ContactInfoValidator()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().WithMessage("Введите почту")
                .NotEmpty().WithMessage("Введите почту")
                 .Must(EmailHelper.IsEmailValid).WithMessage("Введите валидную почту");

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotNull().WithMessage("Введите имя")
                .NotEmpty().WithMessage("Введите имя")
                .MinimumLength(_minNameLength).WithMessage($"Минимальная длина имени {_minNameLength}")
                .MaximumLength(_maxNameLength).WithMessage($"Максимальная длина имени {_maxNameLength}");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotNull().WithMessage("Введите фамилию")
                .NotEmpty().WithMessage("Введите фамилию")
               .MinimumLength(_minNameLength).WithMessage($"Минимальная длина фамилии {_minNameLength}")
               .MaximumLength(_maxNameLength).WithMessage($"Максимальная длина фамилии {_maxNameLength}");

            RuleFor(x => x.Patronymic)
              .MaximumLength(_maxNameLength).WithMessage($"Максимальная длина отчества {_maxNameLength}");
        }
    }
}

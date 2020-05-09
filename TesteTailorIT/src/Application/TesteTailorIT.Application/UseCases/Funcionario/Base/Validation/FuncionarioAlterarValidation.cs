using FluentValidation;
using TesteTailorIT.Application.UseCases.Funcionario.Request;

namespace TesteTailorIT.Application.UseCases.Funcionario.Base.Validations
{
    public class FuncionarioAlterarValidation : AbstractValidator<AlterarFuncionarioRequest>
    {
        public FuncionarioAlterarValidation()
        {
            ValidateIdNulo();
            ValidateNomeNulo();
            ValidateNomeTamanho();
            ValidateSemSobrenome();
            ValidateEmailNulo();
            ValidateEmailTamanho();
            ValidateSexoNulo();
            ValidateSexoTamanho();
            ValidateDataNascimentoNula();
            ValidateHabilidadesRange();
        }

        protected void ValidateIdNulo()
        {
            RuleFor(r => r.Id)
                .NotNull()
                .WithMessage("O ID do funcionário é obrigatório para a alteração!");
        }

        private void ValidateHabilidadesRange()
        {
            RuleFor(r => r.Habilidades.Count)
                .NotEqual(0)
                .WithMessage("O funcionário deve conter ao menos uma habilidade!");
        }

        protected void ValidateNomeNulo()
        {
            RuleFor(r => r.Nome)
                .NotNull()
                .NotEqual(string.Empty)
                .WithMessage("O nome do funcionário é obrigatório!");
        }

        protected void ValidateSemSobrenome()
        {
            RuleFor(r => verificarSeTemSobrenome(r.Nome))
                .NotEqual(false)
                .WithMessage("O sobrenome do funcionário é obrigatório!");
        }

        protected void ValidateNomeTamanho()
        {
            RuleFor(r => r.Nome)
               .MaximumLength(100)
               .WithMessage("O nome do funcionário deve conter no máximo 100 caractéres!");
        }

        protected void ValidateEmailNulo()
        {
            RuleFor(r => r.Email)
               .NotNull()
               .NotEqual(string.Empty)
               .WithMessage("O email do funcionário é obrigatório!");
        }

        protected void ValidateEmailTamanho()
        {
            RuleFor(r => r.Email)
               .MaximumLength(200)
               .WithMessage("O email do funcionário deve conter no máxiomo 200 dígitos!");
        }

        protected void ValidateSexoNulo()
        {
            RuleFor(r => r.Sexo)
               .NotNull()
               .WithMessage("O sexo do funcionário é obrigatório!");
        }

        protected void ValidateSexoTamanho()
        {
            RuleFor(r => r.Sexo.ToString())
               .MaximumLength(1)
               .WithMessage("O sexo do funcionário deve conter somente M ou F!");
        }

        protected void ValidateDataNascimentoNula()
        {
            RuleFor(r => r.DataNascimento)
               .NotNull()
               .WithMessage("A data de nascimento do funcionário é obrigatória!");
        }

        public bool verificarSeTemSobrenome(string nome)
        {
            var stringArray = nome.Split(" ");

            if (stringArray.Length > 1)
            {
                return true;
            }

            return false;
        }
    }
}
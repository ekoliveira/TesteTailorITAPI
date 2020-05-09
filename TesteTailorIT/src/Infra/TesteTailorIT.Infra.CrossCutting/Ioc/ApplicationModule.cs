using Autofac;
using TesteTailorIT.Application.Interfaces.UseCases;
using TesteTailorIT.Application.UseCases.Funcionario.Base;
using TesteTailorIT.Infra.Data.Repositories;

namespace TesteTailorIT.Infra.CrossCutting.Ioc
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(FuncionarioUseCase).Assembly)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(FuncionarioRepository).Assembly)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<FuncionarioUseCase>().As<IFuncionarioUseCase>().InstancePerLifetimeScope();
        }
    }
}
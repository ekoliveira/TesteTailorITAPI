using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TesteTailorIT.Domain.Models;
using TesteTailorIT.Infra.CrossCutting.AutoMapper;
using TesteTailorIT.Infra.Data.Models;
using TesteTailorIT.Infra.Data.Repositories;

namespace TesteTailorIT.Test
{
    public class CarregarBancoInMemory
    {
        private IMapper mapper;

        public CarregarBancoInMemory()
        {
            var config = new MapperConfigurationExpression();
            config.ForAllMaps((map, expression) =>
            {
                foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                    expression.ForMember(unmappedPropertyName,
                        configurationExpression => configurationExpression.Ignore());
            });

            config.AddProfiles(typeof(ApplicationProfile).Assembly);

            var mapperConfig = new MapperConfiguration(config);
            mapper = new Mapper(mapperConfig);
        }

        public FuncionarioRepository DataInMemory()
        {
            var options = new DbContextOptionsBuilder<FuncionarioRepository>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid
                .NewGuid().ToString()).Options;

            var context = new FuncionarioRepository(options, mapper);

            var habilidades = new HabilidadeDataModel[]
            {
                new HabilidadeDataModel{ Descricao = "C#"},
                new HabilidadeDataModel{ Descricao = "Java"},
                new HabilidadeDataModel{ Descricao = "Angular"},
                new HabilidadeDataModel{ Descricao = "SQL"},
                new HabilidadeDataModel{ Descricao = "ASP"},
            };

            var funcionarios = new FuncionarioModel[]
            {
                new FuncionarioModel
                {
                    Id = 1,
                    Nome = "Erick Henrique de Oliveira",
                    Email ="email@email.com",
                    Sexo = "M",
                    DataNascimento = new DateTime(1994,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{ Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 2, Descricao = "Java"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                        new HabilidadeModel{ Id = 4, Descricao = "SQL"},
                        new HabilidadeModel{ Id = 5, Descricao = "ASP"},
                    }
                },

                new FuncionarioModel
                {
                    Id = 2,
                    Nome = "Claudia Carvalho dos Santos",
                    Email ="email@email.com",
                    Sexo = "F",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                    }
                },
            };

            context.AddRange(habilidades);
            context.SaveChanges();

            foreach (var item in funcionarios)
            {
                context.InsertFuncionario(item);
            }

            return context;
        }
    }
}
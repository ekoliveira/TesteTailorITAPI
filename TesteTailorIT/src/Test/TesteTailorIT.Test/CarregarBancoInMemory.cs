//using AutoMapper;
//using AutoMapper.Configuration;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using TesteTailorIT.Infra.CrossCutting.AutoMapper;
//using TesteTailorIT.Infra.Data.Models;
//using TesteTailorIT.Infra.Data.Repositories;

//namespace TesteTailorIT.Test
//{
//    public class CarregarBancoInMemory
//    {
//        private IMapper mapper;

//        public CarregarBancoInMemory()
//        {
//            var config = new MapperConfigurationExpression();
//            config.ForAllMaps((map, expression) =>
//            {
//                foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
//                    expression.ForMember(unmappedPropertyName,
//                        configurationExpression => configurationExpression.Ignore());
//            });

//            config.AddProfiles(typeof(ApplicationProfile).Assembly);

//            var mapperConfig = new MapperConfiguration(config);
//            mapper = new Mapper(mapperConfig);
//        }

//        public FuncionarioRepository DataInMemory()
//        {
//            var options = new DbContextOptionsBuilder<FuncionarioRepository>()
//                .EnableSensitiveDataLogging()
//                .UseInMemoryDatabase(Guid
//                .NewGuid().ToString()).Options;

//            var context = new FuncionarioRepository(options, mapper);

//            context.Funcionario.AddRange(new List<FuncionarioDataModel>()
//            {
//                new FuncionarioDataModel{Id =1, Nome = "Erick Henrique de Oliveira", Cpf="44243792801", Rg="460743181", DataNascimento= new DateTime(1994,03,19)},
//                new FuncionarioDataModel{Id =2, Nome = "Paulo Rodrigo de Oliveira", Cpf="52406000079", Rg="195756551", DataNascimento= new DateTime(1990,11,19)},
//                new FuncionarioDataModel{Id =3, Nome = "Maria de Fatima Santos Oliveira", Cpf="72711992098", Rg="371240955", DataNascimento= new DateTime(1969,07,01)},
//            });

//            context.SaveChanges();

//            return context;
//        }
//    }
//}
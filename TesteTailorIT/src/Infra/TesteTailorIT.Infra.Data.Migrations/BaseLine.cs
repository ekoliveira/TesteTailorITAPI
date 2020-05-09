using TesteTailorIT.Infra.Migration.Base;

namespace TesteTailorIT.Infra.Data.Migrations
{
    [MigrationBase(1, "BaseLine")]
    public class BaseLine : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Funcionario")
                  .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                  .WithColumn("Nome").AsAnsiString(100).NotNullable()
                  .WithColumn("Email").AsAnsiString(200).Nullable()
                  .WithColumn("Sexo").AsAnsiString(1).NotNullable()
                  .WithColumn("DataNascimento").AsDateTime().NotNullable();

            Create.Table("Habilidade")
                  .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                  .WithColumn("Descricao").AsAnsiString(100).NotNullable();

            Create.Table("FuncionarioHabilidade")
                  .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                  .WithColumn("FuncionarioId").AsInt32().NotNullable()
                  .WithColumn("HabilidadeId").AsInt32().NotNullable();

            Create.ForeignKey("FK_FuncionarioHabilidade_Funcionario")
                .FromTable("FuncionarioHabilidade")
                .ForeignColumns("FuncionarioId")
                .ToTable("Funcionario")
                .PrimaryColumn("Id");

            Create.ForeignKey("FK_FuncionarioHabilidade_Habilidade")
                .FromTable("FuncionarioHabilidade")
                .ForeignColumns("HabilidadeId")
                .ToTable("Habilidade")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Cliente");
        }
    }
}
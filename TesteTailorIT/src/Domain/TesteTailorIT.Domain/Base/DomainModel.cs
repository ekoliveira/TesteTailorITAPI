namespace TesteTailorIT.Domain.Base
{
    public abstract class DomainModel : IDomainModel
    {
        public virtual int Id { get; set; }
    }
}
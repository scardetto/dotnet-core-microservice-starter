using System;
using Dapper;

namespace __NAME__.App.Domain
{
    public enum ExampleStatus
    {
        Open = 10000,
        Closed = 20000
    }

    [Table("ExampleEntities")]
    public class ExampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("ExampleStatusID")]
        public ExampleStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public ExampleEntity() { }

        public ExampleEntity(string name)
        {
            Name = name;
            Status = ExampleStatus.Open;
            DateCreated = DateUpdated = DateTime.Now.ToTheSecond();
        }

        public virtual void Close()
        {
            Status = ExampleStatus.Closed;
            DateUpdated = DateTime.Now.ToTheSecond();
        }
    }
}
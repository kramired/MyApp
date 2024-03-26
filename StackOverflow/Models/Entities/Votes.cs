using System;

namespace StackOverflow.Models.Entities
{
    public class Votes
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int userId { get; set; }
        public int BountyAmount { get; set; }
        public int VoteTypeId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

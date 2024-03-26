namespace App_MVC.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int OwnerUserId { get; set; }


        public int AnswerCount { get; set; }

        public int CommentCount { get; set; }

        public int PostTypeId { get; set; }

        public int Score { get; set; }
        public int VoteCount { get; set; }

        public string Tags { get; set; }

        public string viewCount { get; set; }

        public Posts()
        {

        }
    }
}

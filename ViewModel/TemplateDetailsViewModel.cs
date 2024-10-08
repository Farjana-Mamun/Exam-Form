namespace ExamForms.ViewModel
{
    public class TemplateDetailsViewModel
    {
        public TemplateViewModel Template { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int LikesCount { get; set; }
    }
}

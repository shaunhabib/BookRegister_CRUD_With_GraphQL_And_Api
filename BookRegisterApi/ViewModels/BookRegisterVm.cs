namespace BookRegisterApi.ViewModels
{
    public class BookRegisterVm
    {
        public int Id { get; set; }
        public UserVm User { get; set; }
        public BookVm Book { get; set; }
    }
}

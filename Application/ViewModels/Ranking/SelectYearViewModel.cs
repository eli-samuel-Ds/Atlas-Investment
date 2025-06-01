namespace Application.ViewModels.Ranking
{
    public class SelectYearViewModel
    {
        public List<int> AvailableYears { get; set; } = new List<int>();

        public int SelectedYear { get; set; }
    }
}

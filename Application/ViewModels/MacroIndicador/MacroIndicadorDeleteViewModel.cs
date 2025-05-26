namespace Application.ViewModels.MacroIndicador
{
    public class MacroIndicadorDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public bool IsHigherBetter { get; set; }
    }
}

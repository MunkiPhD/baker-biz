namespace baker_biz_interfaces
{
    public interface IPantry
    {
        public int GetAmountRemaining(string ingredient_Name);
        public void UseIngredient(string ingredient_Name, int amount);
        public void ReportLeftOvers();
    }
}
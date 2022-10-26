namespace baker_biz_interfaces
{
    public interface IPantry
    {
        public uint GetAmountRemaining(string ingredient_Name);
        public void UseIngredient(string ingredient_Name, uint amount);
        public void ReportLeftOvers();
    }
}
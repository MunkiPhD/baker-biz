namespace baker_biz_interfaces
{
    public interface IRecipe
    {
        public void Calc(IPantry pantry);
        public void Report();
    }
}
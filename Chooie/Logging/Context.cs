namespace Chooie.Logging
{
    public class Context : IContext
    {
        public Context(string caption)
        {
            Caption = caption;
        }

        public string Caption { get; private set; }
    }
}
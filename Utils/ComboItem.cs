namespace App2.Utils
{
    internal class ComboItem
    {
        private string value;
        private string text;

        public ComboItem(string Value, string Text)
        {
            this.text = Text;
            this.Value = Value;
        }

        public string Value { get => Value; set => Value = value; }
        public string Text { get => text; set => text = value; }


        public override string ToString()
        {
            return Text;
        }

    }
}
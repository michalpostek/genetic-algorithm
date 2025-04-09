namespace Sinusie.View;

public delegate void OnSubmit(int generations);

public partial class FormView : Form
{
    private readonly TextBox _generationsTextBox;
    private readonly OnSubmit _onSubmit;
    private readonly Button _startButton;

    public FormView(OnSubmit onSubmit)
    {
        Form = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(10)
        };
        _generationsTextBox = CreateTextBox("Number of generations");

        _startButton = new Button
        {
            Text = "Start",
            AutoSize = true
        };
        _startButton.Click += HandleSubmit;

        _generationsTextBox.KeyPress += AllowOnlyNumberInput;

        _onSubmit = onSubmit;

        Form.Controls.Add(CreateLabel("Generations:"));
        Form.Controls.Add(_generationsTextBox);
        Form.Controls.Add(_startButton);
    }

    public FlowLayoutPanel Form { get; }

    private void HandleSubmit(object? sender, EventArgs e)
    {
        if (!int.TryParse(_generationsTextBox.Text, out var generationsParsed)) return;

        _generationsTextBox.Clear();
        _onSubmit(generationsParsed);
    }

    private static Label CreateLabel(string text)
    {
        return new Label
        {
            Text = text,
            AutoSize = true
        };
    }

    private static TextBox CreateTextBox(string placeholderText)
    {
        return new TextBox
        {
            Width = 250,
            PlaceholderText = placeholderText
        };
    }

    private static void AllowOnlyNumberInput(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
    }
}
namespace MutatedRug.View;

public delegate void OnSubmit(int generations, int populationSize);

public partial class FormView : Form
{
    private readonly TextBox _generationsTextBox;
    private readonly OnSubmit _onSubmit;
    private readonly TextBox _populationSizeTextBox;
    private readonly Button _startButton;

    public FormView(OnSubmit onSubmit)
    {
        Form = CreateForm();
        _generationsTextBox = CreateTextBox("Number of generations");
        _populationSizeTextBox = CreateTextBox("Population size");

        _startButton = CreateStartButton();
        _startButton.Click += HandleSubmit;

        _generationsTextBox.KeyPress += AllowOnlyNumberInput;
        _populationSizeTextBox.KeyPress += AllowOnlyNumberInput;

        _onSubmit = onSubmit;

        Form.Controls.Add(CreateLabel("Generations:"));
        Form.Controls.Add(_generationsTextBox);
        Form.Controls.Add(CreateLabel("Population size:"));
        Form.Controls.Add(_populationSizeTextBox);
        Form.Controls.Add(_startButton);
    }

    public FlowLayoutPanel Form { get; }

    private void HandleSubmit(object? sender, EventArgs e)
    {
        if (!int.TryParse(_generationsTextBox.Text, out var generationsParsed) ||
            !int.TryParse(_populationSizeTextBox.Text, out var populationSizeParsed))
        {
            return;
        }

        ClearTextBoxes();
        _onSubmit(generationsParsed, populationSizeParsed);
    }

    private void ClearTextBoxes()
    {
        _generationsTextBox.Clear();
        _populationSizeTextBox.Clear();
    }

    private static Button CreateStartButton()
    {
        return new Button
        {
            Text = "Start",
            AutoSize = true
        };
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

    private static FlowLayoutPanel CreateForm()
    {
        return new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            Padding = new Padding(10)
        };
    }

    private static void AllowOnlyNumberInput(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
    }
}
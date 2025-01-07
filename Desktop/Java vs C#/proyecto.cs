using System;
using System.Drawing;
using System.Windows.Forms;

public class TextEditor : Form
{
    private RichTextBox textArea;
    private CheckBox boldCheckBox, italicCheckBox;
    private ComboBox fontComboBox;
    private NumericUpDown fontSizeSpinner;
    private TrackBar fontSizeSlider;

    public TextEditor()
    {
        // Configurar la ventana principal
        this.Text = "Editor de Texto Básico";
        this.Size = new Size(800, 600);

        // Crear el área de texto
        textArea = new RichTextBox
        {
            Dock = DockStyle.Fill
        };
        this.Controls.Add(textArea);

        // Crear la barra de menú
        MenuStrip menuStrip = new MenuStrip();
        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);

        // Menú Archivo
        ToolStripMenuItem fileMenu = new ToolStripMenuItem("Archivo");
        menuStrip.Items.Add(fileMenu);

        ToolStripMenuItem newMenuItem = new ToolStripMenuItem("Nuevo");
        newMenuItem.Click += (sender, e) => textArea.Clear();
        fileMenu.DropDownItems.Add(newMenuItem);

        ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Salir");
        exitMenuItem.Click += (sender, e) => Application.Exit();
        fileMenu.DropDownItems.Add(exitMenuItem);

        // Menú Estilo
        ToolStripMenuItem styleMenu = new ToolStripMenuItem("Estilo");
        menuStrip.Items.Add(styleMenu);

        ToolStripMenuItem boldMenuItem = new ToolStripMenuItem("Negrita")
        {
            CheckOnClick = true
        };
        boldMenuItem.Click += (sender, e) => {
            boldCheckBox.Checked = boldMenuItem.Checked;
            UpdateFontStyle();
        };
        styleMenu.DropDownItems.Add(boldMenuItem);

        ToolStripMenuItem italicMenuItem = new ToolStripMenuItem("Cursiva")
        {
            CheckOnClick = true
        };
        italicMenuItem.Click += (sender, e) => {
            italicCheckBox.Checked = italicMenuItem.Checked;
            UpdateFontStyle();
        };
        styleMenu.DropDownItems.Add(italicMenuItem);

        // Panel inferior para opciones de formato
        FlowLayoutPanel formatPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true
        };
        this.Controls.Add(formatPanel);

        // CheckBox para negrita
        boldCheckBox = new CheckBox
        {
            Text = "Negrita"
        };
        boldCheckBox.CheckedChanged += (sender, e) => {
            boldMenuItem.Checked = boldCheckBox.Checked;
            UpdateFontStyle();
        };
        formatPanel.Controls.Add(boldCheckBox);

        // CheckBox para cursiva
        italicCheckBox = new CheckBox
        {
            Text = "Cursiva"
        };
        italicCheckBox.CheckedChanged += (sender, e) => {
            italicMenuItem.Checked = italicCheckBox.Checked;
            UpdateFontStyle();
        };
        formatPanel.Controls.Add(italicCheckBox);

        // ComboBox para seleccionar la fuente
        fontComboBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 150
        };
        foreach (FontFamily font in FontFamily.Families)
        {
            fontComboBox.Items.Add(font.Name);
        }
        fontComboBox.SelectedItem = "Microsoft Sans Serif";
        fontComboBox.SelectedIndexChanged += (sender, e) => UpdateFontStyle();
        formatPanel.Controls.Add(fontComboBox);

        // NumericUpDown para el tamaño de la fuente
        fontSizeSpinner = new NumericUpDown
        {
            Minimum = 8,
            Maximum = 72,
            Value = 12
        };
        fontSizeSpinner.ValueChanged += (sender, e) => {
            fontSizeSlider.Value = (int)fontSizeSpinner.Value;
            UpdateFontStyle();
        };
        formatPanel.Controls.Add(new Label { Text = "Tamaño:" });
        formatPanel.Controls.Add(fontSizeSpinner);

        // TrackBar para el tamaño de la fuente
        fontSizeSlider = new TrackBar
        {
            Minimum = 8,
            Maximum = 72,
            Value = 12,
            TickFrequency = 8,
            LargeChange = 2
        };
        fontSizeSlider.ValueChanged += (sender, e) => {
            fontSizeSpinner.Value = fontSizeSlider.Value;
            UpdateFontStyle();
        };
        formatPanel.Controls.Add(fontSizeSlider);
    }

    private void UpdateFontStyle()
    {
        FontStyle style = FontStyle.Regular;
        if (boldCheckBox.Checked) style |= FontStyle.Bold;
        if (italicCheckBox.Checked) style |= FontStyle.Italic;

        string fontName = fontComboBox.SelectedItem.ToString();
        float fontSize = (float)fontSizeSpinner.Value;

        textArea.Font = new Font(fontName, fontSize, style);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new TextEditor());
    }
}

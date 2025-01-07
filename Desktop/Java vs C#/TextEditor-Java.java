import javax.swing.*;
import javax.swing.text.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TextEditor extends JFrame {
    private JTextPane textArea;
    private JCheckBox boldCheckBox, italicCheckBox;
    private JComboBox<String> fontComboBox;
    private JSpinner fontSizeSpinner;
    private JSlider fontSizeSlider;

    public TextEditor() {
        // Configure the main window
        setTitle("Basic Text Editor");
        setSize(800, 600);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        // Create the text area
        textArea = new JTextPane();
        JScrollPane scrollPane = new JScrollPane(textArea);
        add(scrollPane, BorderLayout.CENTER);

        // Create the menu bar
        JMenuBar menuBar = new JMenuBar();
        setJMenuBar(menuBar);

        // File menu
        JMenu fileMenu = new JMenu("File");
        menuBar.add(fileMenu);

        JMenuItem newMenuItem = new JMenuItem("New");
        newMenuItem.addActionListener(e -> textArea.setText(""));
        fileMenu.add(newMenuItem);

        JMenuItem exitMenuItem = new JMenuItem("Exit");
        exitMenuItem.addActionListener(e -> System.exit(0));
        fileMenu.add(exitMenuItem);

        // Style menu
        JMenu styleMenu = new JMenu("Style");
        menuBar.add(styleMenu);

        JCheckBoxMenuItem boldMenuItem = new JCheckBoxMenuItem("Bold");
        boldMenuItem.addActionListener(e -> {
            boldCheckBox.setSelected(boldMenuItem.isSelected());
            updateTextStyle();
        });
        styleMenu.add(boldMenuItem);

        JCheckBoxMenuItem italicMenuItem = new JCheckBoxMenuItem("Italic");
        italicMenuItem.addActionListener(e -> {
            italicCheckBox.setSelected(italicMenuItem.isSelected());
            updateTextStyle();
        });
        styleMenu.add(italicMenuItem);

        // Bottom panel for formatting options
        JPanel formatPanel = new JPanel();
        formatPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
        add(formatPanel, BorderLayout.SOUTH);

        // Bold checkbox
        boldCheckBox = new JCheckBox("Bold");
        boldCheckBox.addActionListener(e -> {
            boldMenuItem.setSelected(boldCheckBox.isSelected());
            updateTextStyle();
        });
        formatPanel.add(boldCheckBox);

        // Italic checkbox
        italicCheckBox = new JCheckBox("Italic");
        italicCheckBox.addActionListener(e -> {
            italicMenuItem.setSelected(italicCheckBox.isSelected());
            updateTextStyle();
        });
        formatPanel.add(italicCheckBox);

        // Font selection combo box
        fontComboBox = new JComboBox<>();
        GraphicsEnvironment ge = GraphicsEnvironment.getLocalGraphicsEnvironment();
        String[] fonts = ge.getAvailableFontFamilyNames();
        for (String font : fonts) {
            fontComboBox.addItem(font);
        }
        fontComboBox.setSelectedItem("SansSerif");
        fontComboBox.addActionListener(e -> updateTextStyle());
        formatPanel.add(new JLabel("Font:"));
        formatPanel.add(fontComboBox);

        // Font size spinner
        fontSizeSpinner = new JSpinner(new SpinnerNumberModel(12, 8, 72, 1));
        fontSizeSpinner.addChangeListener(e -> {
            fontSizeSlider.setValue((int) fontSizeSpinner.getValue());
            updateTextStyle();
        });
        formatPanel.add(new JLabel("Size:"));
        formatPanel.add(fontSizeSpinner);

        // Font size slider
        fontSizeSlider = new JSlider(8, 72, 12);
        fontSizeSlider.setMajorTickSpacing(8);
        fontSizeSlider.setPaintTicks(true);
        fontSizeSlider.addChangeListener(e -> {
            fontSizeSpinner.setValue(fontSizeSlider.getValue());
            updateTextStyle();
        });
        formatPanel.add(fontSizeSlider);
    }

    private void updateTextStyle() {
        // Get selected font name and size
        String fontName = (String) fontComboBox.getSelectedItem();
        int fontSize = (int) fontSizeSpinner.getValue();

        // Determine font style
        int style = Font.PLAIN;
        if (boldCheckBox.isSelected()) style |= Font.BOLD;
        if (italicCheckBox.isSelected()) style |= Font.ITALIC;

        // Apply font settings to the text area
        Font font = new Font(fontName, style, fontSize);
        textArea.setFont(font);
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            TextEditor editor = new TextEditor();
            editor.setVisible(true);
        });
    }
}

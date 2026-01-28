using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawPaint;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool isDrawing;
    private Polyline? currentStroke;


    public MainWindow()
    {
        InitializeComponent();

#if !DEBUG
        DebugTextBox.Visibility = Visibility.Collapsed; // hide in Release
#endif
    }

    // Mouse events
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        isDrawing = true;
        Point startPoint = e.GetPosition(DrawingCanvas);

        currentStroke = new Polyline();
        currentStroke.Stroke = Brushes.Black;
        currentStroke.StrokeThickness = 2;

        currentStroke.Points.Add(startPoint);
        DrawingCanvas.Children.Add(currentStroke);


        Debug($"Mouse down at {startPoint.X}, {startPoint.Y}");
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDrawing || currentStroke == null)
            return;

        Point position = e.GetPosition(DrawingCanvas);
        currentStroke.Points.Add(position);


        Debug($"Mouse move at {position.X}, {position.Y}");
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        isDrawing = false;
        currentStroke = null;


        Debug($"Drawing {isDrawing}");
    }

    // Log information for debugging
    private void Debug(string message)
    {
        DebugTextBox.AppendText(message + "\n");
        DebugTextBox.ScrollToEnd();
    }
}
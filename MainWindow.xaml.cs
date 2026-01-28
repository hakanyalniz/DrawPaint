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
        // This is required for later on MouseMove
        isDrawing = true;
        Point startPoint = e.GetPosition(DrawingCanvas);

        // After getting the position of mouse, we create a new Polyline and set brush properties
        currentStroke = new Polyline();
        currentStroke.Stroke = Brushes.Black;
        currentStroke.StrokeThickness = 2;

        // We add a point on the mouse point, then add the current polyline object to the canvas
        // From now on, when we make changes to it, it will reflect on the canvas until we remove the reference for it
        currentStroke.Points.Add(startPoint);
        DrawingCanvas.Children.Add(currentStroke);


        Debug($"Mouse down at {startPoint.X}, {startPoint.Y}");
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDrawing || currentStroke == null) // If we are not drawing, return
            return;

        // Get position, add a point to the current mouse position
        Point position = e.GetPosition(DrawingCanvas);
        currentStroke.Points.Add(position);


        Debug($"Mouse move at {position.X}, {position.Y}");
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        // We are not drawing and remove the reference
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
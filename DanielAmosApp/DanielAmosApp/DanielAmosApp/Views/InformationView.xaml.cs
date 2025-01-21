using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DanielAmosApp.Views
{
    /// <summary>
    /// Interaction logic for InformationView.xaml
    /// </summary>
    public partial class InformationView : UserControl
    {
        public InformationView()
        {
            InitializeComponent();

            // Creating and defining the document
            FlowDocument document = CreateFlowDocument();
            DocumentViewer.Document = document;

            // Activating the animation on the title
            StartFadeInAnimation();
        }

        private FlowDocument CreateFlowDocument()
        {
            // Creating the FlowDocument
            FlowDocument doc = new FlowDocument
            {
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 14,
                Foreground = Brushes.DarkSlateGray
            };

            // The opening title of the document
            Paragraph intro = new Paragraph(new Run("🤩 Welcome to - TrackTrix 🥳"))
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };

            // A paragraph explaining the purpose of the application
            Paragraph description = new Paragraph(new Run("🤯 Our goal is to provide an efficient and advanced solution for comprehensive management " +
                                                          "of machines and their operations."))
            {
                FontSize = 14,
                TextAlignment = TextAlignment.Left,
            };

            // Adding an image to the document with animation
            var image = CreateAnimatedImage();

            // Anchoring the image in the center of the document with padding
            var figure = new Figure
            {
                Width = new FigureLength(300),
                Height = new FigureLength(200),
                HorizontalAnchor = FigureHorizontalAnchor.PageCenter,
                VerticalAnchor = FigureVerticalAnchor.ContentTop,
                Margin = new Thickness(0, 20, 0, 20)
            };

            figure.Blocks.Add(new BlockUIContainer(image));
            var imageParagraph = new Paragraph();
            imageParagraph.Inlines.Add(figure);

            // A list of features
            List features = new List
            {
                MarkerStyle = TextMarkerStyle.Circle
            };
            features.ListItems.Add(new ListItem(new Paragraph(new Run("💡 Machine Status Tracking: With us, you can easily see the status of any machine and take action accordingly."))));
            features.ListItems.Add(new ListItem(new Paragraph(new Run("✍️ Data Analysis: Only here can you manage " +
                "                                                         comprehensive records of machine statuses, edit, add, and even search by specific conditions."))));
            features.ListItems.Add(new ListItem(new Paragraph(new Run("🤖 Visual Response: With every update and search, we can instantly know whether " +
                "                                                         the information has been updated or if we need to clean a few factory chimneys."))));

            var contact = new Paragraph(new Run("👀 Contact Information :"))
            {
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Teal,
                TextAlignment = TextAlignment.Left
            };

            var contactDetails = new Paragraph();
            contactDetails.Inlines.Add(new Run("🤠 Email : "));
            contactDetails.Inlines.Add(new Hyperlink(new Run("danielamosdev@gmail.com"))
            {
                NavigateUri = new Uri("mailto:danielamosdev@gmail.com")
            });
            contactDetails.Inlines.Add(new LineBreak());
            contactDetails.Inlines.Add(new Run("🐋 Phone : 052-356-5655"));
            contactDetails.Inlines.Add(new LineBreak());
            contactDetails.Inlines.Add(new Run("🥹 Address : Somewhere in the universe, wild dreams 3. "));


            // A motivational quote
            Paragraph motivation = new Paragraph(new Run("🌟 The only way to succeed is to believe that you can. ☄️"))
            {
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Crimson,
                TextAlignment = TextAlignment.Center
            };

            // Adding content to the document
            doc.Blocks.Add(intro);
            doc.Blocks.Add(description);
            doc.Blocks.Add(imageParagraph);
            doc.Blocks.Add(features);
            doc.Blocks.Add(contact);
            doc.Blocks.Add(contactDetails);
            doc.Blocks.Add(motivation);

            return doc;
        }

        private Image CreateAnimatedImage()
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Info.png")),
                Width = 300,
                Height = 200,
                Stretch = Stretch.Uniform,
                RenderTransform = new ScaleTransform(1, 1),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Opacity = 0
            };

            StartImageAnimation(image);

            return image;
        }

        private void StartImageAnimation(Image image)
        {
            // Retrieving the animation from XAML resources
            Storyboard storyboard = (Storyboard)Resources["ImageScaleAnimation"];
            Storyboard.SetTarget(storyboard, image);
            storyboard.Begin();
        }

        private void StartFadeInAnimation()
        {
            // Retrieving the animation from XAML resources
            Storyboard fadeIn = (Storyboard)Resources["FadeInAnimation"];
            Storyboard.SetTarget(fadeIn, TitleText);
            fadeIn.Begin();
        }
    }
}

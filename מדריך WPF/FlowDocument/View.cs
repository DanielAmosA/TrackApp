public partial class InfoDoco : UserControl
{
    public InfoDoco()
    {
        InitializeComponent();

        // יצירת המסמך והגדרתו
        FlowDocument document = CreateFlowDocument();
        DocumentViewer.Document = document;

        // הפעלת האנימציה על הכותרת
        StartFadeInAnimation();
    }

    private FlowDocument CreateFlowDocument()
    {
        // יצירת FlowDocument
        FlowDocument doc = new FlowDocument
        {
            FontFamily = new FontFamily("Segoe UI"),
            FontSize = 14,
            Foreground = Brushes.DarkSlateGray
        };

        // כותרת הפתיחה של המסמך
        Paragraph intro = new Paragraph(new Run("ברוכים הבאים ליישום שלנו!"))
        {
            FontSize = 16,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center
        };

        // פסקה שמסבירה את מטרת היישום
        Paragraph description = new Paragraph(new Run("היישום שלנו מיועד לשדרג את חוויית ההימורים על סדרות וסרטים. כאן תוכלו לנחש, להתחרות, ולגלות מי באמת הגאון שחוזה את הסוף."))
        {
            FontSize = 14,
            TextAlignment = TextAlignment.Left
        };

        // הוספת תמונה למסמך עם אנימציה
        var image = CreateAnimatedImage();

        //// עיגון התמונה במרכז המסמך עם מרווחים
        var figure = new Figure
        {
            Width = new FigureLength(300),
            Height = new FigureLength(200),
            HorizontalAnchor = FigureHorizontalAnchor.PageCenter,
            VerticalAnchor = FigureVerticalAnchor.ContentTop, // עיגון התמונה לראש המסמך
            Margin = new Thickness(0, 20, 0, 20) // הוספת מרווחים
        };

        figure.Blocks.Add(new BlockUIContainer(image));
        var imageParagraph = new Paragraph();
        imageParagraph.Inlines.Add(figure);

        // רשימה של פיצ'רים
        List features = new List
        {
            MarkerStyle = TextMarkerStyle.Circle
        };
        features.ListItems.Add(new ListItem(new Paragraph(new Run("💡 חיפוש אנשים: מצאו חברים חדשים או כאלה שהפסדתם בתחרות."))));
        features.ListItems.Add(new ListItem(new Paragraph(new Run("💡 סטטיסטיקות: ניתוח נתונים מעמיק על הביצועים שלכם."))));
        features.ListItems.Add(new ListItem(new Paragraph(new Run("💡 טבלת אלופים: גלו מי באמת האלוף הבלתי מעורער!"))));

        var contact = new Paragraph(new Run("דרכי התקשרות:"))
        {
            FontSize = 18,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Teal,
            TextAlignment = TextAlignment.Left
        };

        var contactDetails = new Paragraph();
        contactDetails.Inlines.Add(new Run("אימייל: "));
        contactDetails.Inlines.Add(new Hyperlink(new Run("danielamosdev@gmail.com"))
        {
            NavigateUri = new Uri("mailto:danielamosdev@gmail.com")
        });
        contactDetails.Inlines.Add(new LineBreak());
        contactDetails.Inlines.Add(new Run("טלפון: טלפון: 052-356-5655"));
        contactDetails.Inlines.Add(new LineBreak());
        contactDetails.Inlines.Add(new Run("כתובת: איפשהו ביקום, חלומות פרועים"));


        // משפט מוטיבציה
        Paragraph motivation = new Paragraph(new Run("כי לנחש זה אומנות, ולא רק מזל!"))
        {
            FontStyle = FontStyles.Italic,
            Foreground = Brushes.Crimson,
            TextAlignment = TextAlignment.Center
        };

        // הוספת התוכן למסמך
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
        // יצירת תמונה
        var image = new Image
        {
            Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Logo.png")), // החלף בנתיב התמונה שלך
            Width = 300,
            Height = 200,
            Stretch = Stretch.Uniform,
            RenderTransform = new ScaleTransform(1, 1), // הכנה לאנימציה
            RenderTransformOrigin = new Point(0.5, 0.5), // מרכז הסקייל
            Opacity = 0 // הכנה לאנימציית Fade-In
        };

        // הפעלת האנימציה
        StartImageAnimation(image);

        return image;
    }

    private void StartImageAnimation(Image image)
    {
        // שליפת האנימציה מתוך משאבי ה-XAML
        Storyboard storyboard = (Storyboard)Resources["ImageScaleAnimation"];
        Storyboard.SetTarget(storyboard, image);
        storyboard.Begin();
    }

    private void StartFadeInAnimation()
    {
        // שליפת האנימציה מתוך משאבי ה-XAML
        Storyboard fadeIn = (Storyboard)Resources["FadeInAnimation"];
        // הגדרת היעד לאנימציה (הכותרת)
        Storyboard.SetTarget(fadeIn, TitleText);
        fadeIn.Begin();
    }
}


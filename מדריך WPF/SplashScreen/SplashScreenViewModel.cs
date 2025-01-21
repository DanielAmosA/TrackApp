    public class SplashScreenViewModel : INotifyPropertyChanged
    {
        private double _loadingProgress;
        private string _loadingMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public double LoadingProgress
        {
            get => _loadingProgress;
            set
            {
                _loadingProgress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingProgress)));
            }
        }

        public string LoadingMessage
        {
            get => _loadingMessage;
            set
            {
                _loadingMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingMessage)));
            }
        }

        public async Task StartLoadingAsync()
        {
            var messages = new List<string>
            {
                "כותבים קוד כמו יצירת אמנות 🎨🖥️",
                "קוד גרוע? זה לא באג, זה פיצ'ר 🐛✨",
                "הקוד שלנו רץ, אבל המוח שלך נתקע 🧠💥",
                "הפעם נכתוב דוקומנטציה! (לא באמת) 📜😅",
                "מעלים קוד לפרודקשן ומקווים לטוב 🤞🚀",
                "בודקים כמה אפס ועוד אפס יוצא...💥🚀",
                "כשהחבר לצוות אומר 'זה יעבוד על השרת שלי' 🤦‍ ♂️🔧",
                "🚀אני שחקן נפלא ! נפלא !! רק תן לי צ'אנס להוכיח לך ואני אעשה את זה! 💥",
                "מתניעים את המנועים... 📜🖥️",
                "הקוד שלך נראה יפה יותר בשתיים בלילה 🌌🖋️.",
                "שומרים את כל הסודות של האפליקציה...",
                "הכיף מתחיל ממש עכשיו!🎉✨",
                "📜🐛 האתר עולה ואנחנו נותנים מענה",
                "שוברים את השגרה עם משחק של פרייזבי 🥏🍕",
                "פצפצים מתפוצצים בפיצוצים מפוצצים 💥🥞",
                "הבוס: 'תסיים עד היום', הקוד: 'לא היום' 🙃⌛",
                "❤️‍🔥✨ היי יוסטון זה אהבה זה החיים !",
                "כשהקוד שלך עובר ביקורת כמו חלום 🎉🧑‍ 💻",
            };

            Random random = new Random(); // יצירת מחולל מספרים אקראיים

            for (int i = 0; i <= 100; i++)
            {
                LoadingProgress = i;

                if (i % 20 == 0) // לשנות הודעה בכל שלב משמעותי
                {
                    int randomIndex = random.Next(0, messages.Count); // בוחר אינדקס אקראי
                    LoadingMessage = messages[randomIndex];
                }

                await Task.Delay(40); // לדמות טעינה
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                // Show main window and close splash screen
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.MainWindow = mainWindow;
                Application.Current.Windows[0]?.Close();
            });
        }
    }
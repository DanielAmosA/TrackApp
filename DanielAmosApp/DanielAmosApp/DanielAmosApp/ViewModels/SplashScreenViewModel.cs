using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace DanielAmosApp.ViewModels
{
    public class SplashScreenViewModel : INotifyPropertyChanged
    {
        private double _loadingProgress;
        private string? _loadingMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

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
            get => _loadingMessage!;
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
                "🎨 Writing code like a work of art 🖥️",
                "🐛 Bad code? It's not a bug, it's a feature ✨",
                "🧠 Our code is running, but your mind is stuck 💥",
                "📜 This time we’ll write documentation! (Not really) 😅",
                "🤞 Deploying code to production and hoping for the best 🚀",
                "💥 Checking how much zero plus zero equals... 🚀",
                "🤦‍ When the team member says 'It will work on my server' 🔧",
                "🚀 I'm an amazing player! Amazing!! Just give me a chance to prove it and I’ll do it! 💥",
                "📜 Starting the engines... 🖥️",
                "🌌 Your code looks better at two in the morning 🖋️",
                "We keep all the secrets of the application...",
                "🎉 The fun starts right now! ✨",
                "📜 The website is up and we’re responding 🐛",
                "🥏 Breaking the routine with a frisbee game 🍕",
                "💥 Popcorns popping in explosive explosions 🥞",
                "🙃 The boss: 'Finish by today,' the code: 'Not today' ⌛",
                "🔥✨ Hey Houston, it's love, it's life! ❤️‍",
                "🎉 When your code passes the review like a dream 🧑‍💻",            
            };

            Random random = new Random();

            //At every critical step, meaning a progress of 20, display a different message.
            for (int i = 0; i <= 100; i++)
            {
                LoadingProgress = i;

                if (i % 20 == 0)
                {
                    int randomIndex = random.Next(0, messages.Count);
                    LoadingMessage = messages[randomIndex];
                }

                await Task.Delay(1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DanielAmosApp.Utills.Common
{
    /// <summary>
    /// The class responsible for sound operation.
    /// </summary>

    public class MusicPlayerModel
    {
        private readonly MediaPlayer mediaPlayer;

        public MusicPlayerModel()
        {
            mediaPlayer = new MediaPlayer();
        }

        public void PlayMusic()
        {
            string audioURL = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                @"..\..\..\Assets\Audio\BackgroundMusic.mp3"));
            mediaPlayer.Open(new Uri(audioURL, UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();

            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                Console.WriteLine("The music has been loaded successfully.");
            }
            else
            {
                Console.WriteLine("The music did not load..");
            }
        }

        public void StopMusic()
        {
            mediaPlayer.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Game.Core
{
    public class AudioManager
    {
        private static AudioManager instance;

        private Dictionary<string, WaveOutEvent> audioOutputs = new Dictionary<string, WaveOutEvent>();
        private Dictionary<string, AudioFileReader> audioFiles = new Dictionary<string, AudioFileReader>();

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        public void LoadTrack(string name, string filePath)
        {
            if (!audioFiles.ContainsKey(name))
            {
                audioFiles[name] = new AudioFileReader(filePath);
            }
        }

        public void PlayTrack(string name, bool loop = false)
        {
            if (!audioFiles.ContainsKey(name))
            {
                throw new Exception($"Track {name} not found.");
            }

            if (audioOutputs.ContainsKey(name))
            {
                audioOutputs[name].Stop();
                audioOutputs[name].Dispose();
                audioOutputs.Remove(name);
            }

            var waveOut = new WaveOutEvent();
            var audioFile = audioFiles[name];

            
            audioFile.Position = 0;

            waveOut.Init(audioFile);

            waveOut.PlaybackStopped += (sender, args) =>
            {
                try
                {
                    if (loop)
                    {
                        audioFile.Position = 0;
                        PlayTrack(name, true);
                    }
                    else
                    {
                        waveOut.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al reproducir el audio en loop: {ex.Message}");
                    waveOut.Dispose();
                }
            };

            audioOutputs[name] = waveOut;
            waveOut.Play();
        }

        public void StopTrack(string name)
        {
            if (audioOutputs.ContainsKey(name))
            {
                audioOutputs[name].Stop();
                audioOutputs[name].Dispose();
                audioOutputs.Remove(name);
            }
        }
    }
}

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

        private Dictionary<string, AudioFileReader> tracks = new Dictionary<string, AudioFileReader>();
        private WaveOutEvent outputDevice;
        private AudioFileReader currentTrack;


        private AudioManager()
        {
            outputDevice = new WaveOutEvent();
        }

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
            if (!tracks.ContainsKey(name))
            {
                var track = new AudioFileReader(filePath);
                tracks.Add(name, track);
            }
        }

        public void PlayTrack(string name, bool loop = true)
        {
            if (tracks.ContainsKey(name))
            {
                Stop(); // Detener la pista actual si hay una reproduciendo
                currentTrack = tracks[name];
                outputDevice.Init(currentTrack);
                outputDevice.Play();
                if (loop)
                {
                    outputDevice.PlaybackStopped += (sender, e) => PlayTrack(name, true);
                }
            }
        }

        public void Pause()
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
            }
        }

        public void Stop()
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
                if (currentTrack != null)
                {
                    currentTrack.Position = 0;
                }
            }
        }

        public void UnloadAll()
        {
            foreach (var track in tracks.Values)
            {
                track.Dispose();
            }
            tracks.Clear();
            outputDevice.Dispose();
        }
    }
}

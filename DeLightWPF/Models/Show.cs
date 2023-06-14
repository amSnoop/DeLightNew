﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DeLightWPF.Models
{
    public class Show
    {
        string Name { get; set; }
        string Path { get; set; }
        List<Cue> Cues { get; set; }

        public Cue? this[int i]
        {
            get => Cues[i];
            set
            {
                if (Cues.Count > i)
                    Cues[i] = value ?? throw new ArgumentNullException(nameof(value));
                else if(Cues.Count == i)
                    Cues.Add(value ?? throw new ArgumentNullException(nameof(value)));
                else
                    throw new Exception("Tried to set value at index " + i + "/" + Cues.Count);
            }
        }

        public Show(string name, string path, List<Cue> cues)
        {
            Name = name;
            Path = path;
            Cues = cues;
        }
        public Show()
        {
            Name = "";
            Path = "";
            Cues = new();
        }
        public static Show Load(string filepath)
        {
            Show? show = null;
            if (!File.Exists(filepath) || !filepath.EndsWith("json"))
                Console.WriteLine("Could not find show at " + filepath);
            else
                show = JsonSerializer.Deserialize<Show>(filepath);

            if(show == null)
                Console.WriteLine("Could not load show from " + filepath);
            return show ?? LoadTestShow();
        }
        public static void Save(Show show, string? filepath = null)
        {
            string json = JsonSerializer.Serialize(show);
            File.WriteAllText(filepath ?? show.Path, json);
        }

        public static Show LoadTestShow()
        {
            Show show = new("Test Show", "testshow.json", new List<Cue>());
            show.Cues.Add(new Cue()
            {
                Number = "1",
                Note = "This was a triumph",
                Type = CueType.VidLight
            });
            show.Cues.Add(new Cue()
            {
                Number = "2",
                Note = "I'm leaving a note here:",
                Type = CueType.ImgLight
            });
            show.Cues.Add(new Cue()
            {
                Number = "3",
                Note = "Huge success!",
                Type = CueType.VidLight
            });
            show.Cues.Add(new Cue()
            {
                Number = "4",
                Note = "It's hard to overstate my satisfaction.",
                Type = CueType.LightOnly
            });
            show.Cues.Add(new Cue()
            {
                Number = "5",
                Note = "Aperture Science",
                Type = CueType.LightOnly
            });
            show.Cues.Add(new Cue()
            {
                Number = "6",
                Note = "We do what we must, because, we can.",
                Type = CueType.ImgOnly
            });
            show.Cues.Add(new Cue()
            {
                Number = "7",
                Note = "For the good of all of us,",
                Type = CueType.VidOnly
            });
            show.Cues.Add(new Cue()
            {
                Number = "8",
                Note = "Except the ones who are dead.",
                Type = CueType.VidLight
            });
            return show;
        }
    }
}

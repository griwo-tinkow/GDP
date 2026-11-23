using System;
using Godot;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private AudioStream[] _musicTracks = new AudioStream[]
    {
        GD.Load<AudioStream>("res://Main/Audio/bg-music/MyVeryOwnDeadShip-yd.ogg"),
        GD.Load<AudioStream>("res://Main/Audio/bg-music/One Man Symphony - UltiDigi⁄After The Disaster.ogg")
    };

    [Export] private AudioStreamPlayer _musicPlayer;
    private AudioStreamPlayer _buy;
    private AudioStreamPlayer _clickM;
    private AudioStreamPlayer _clickW;
    private AudioStreamPlayer _win;
    private AudioStreamPlayer _lose;
    private int _curSong;
    public int CurrentSong => _curSong;

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree();
            return;
        }

        // Find nodes using FindChild so it never breaks on path names
        _buy = FindChild("Buy", true, false) as AudioStreamPlayer;
        _clickM = FindChild("Click-M", true, false) as AudioStreamPlayer;
        _clickW = FindChild("Click-W", true, false) as AudioStreamPlayer;
        _win = FindChild("Win", true, false) as AudioStreamPlayer;
        _lose = FindChild("Lose", true, false) as AudioStreamPlayer;
    }

    // Switch and play a song dynamically using an AudioStream asset
    public void PlayMusic(int index)
    {
        if (_musicTracks == null || index < 0 || index >= _musicTracks.Length) return;

        _curSong = index;
        AudioStream targetTrack = _musicTracks[index];

        if (_musicPlayer.Stream == targetTrack && _musicPlayer.Playing) return;

        _musicPlayer.Stop();
        _musicPlayer.Stream = targetTrack;
        _musicPlayer.Play();
    }

    public void SpeedUpCurrent()
    {
        _musicPlayer.PitchScale = 1.15f;
    }

    // SFX Helpers
    public void PlayBuy() => _buy?.Play();
    public void PlayClickM() => _clickM?.Play();
    public void PlayClickW() => _clickW?.Play();
    public void PlayWin() => _win?.Play();
    public void PlayLose() => _lose?.Play();
}
